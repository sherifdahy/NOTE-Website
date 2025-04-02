using AutoMapper;
using DAL.Repository;
using Entities.InterfacesOfRepo;
using Entities.Models;
using Entities.Models.Document.BaseComponent;
using Entities.Models.Document.Receipt;
using ETA.eReceipt.IntegrationToolkit.Application.Models;
using ETA.eReceipt.IntegrationToolkit.Infrastructure.Services;
using Interface.Dto;
using Interface.Services;
using Interface.Services.ApiCall;
using Interface.ViewModels;
using Interface.ViewModels.ReceiptVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NuGet.Common;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;

namespace Interface.Controllers
{
    [Authorize]
    public class ReceiptsController : BaseController
    {
        #region Constructor
        IWebHostEnvironment env;
        IApiCall api;
        private readonly int userId;
        private readonly IMapper mapper;
        private readonly JsonSerializerSettings jsonSerializer;
        public ReceiptsController(IUnitOfWork unitOfWork,
            IWebHostEnvironment webHostEnvironment,
            IApiCall apiCall,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor) : base(unitOfWork)
        {
            api = apiCall;
            env = webHostEnvironment;
            this.mapper = mapper;
            userId = int.Parse(httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            jsonSerializer = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }
        #endregion

        #region Index
        [HttpGet]
        [ServiceFilter(typeof(TokenAttribute))]
        public async Task<IActionResult> Index(int pg = 1)
        {
            string token = HttpContext.Session.GetString("access_token");

            IQueryable<Receipt> receipts = IUnitOfWork.Receipts.FindAll(x => x.ApplicationUserId == userId)
                .OrderByDescending(x => x.ReceiptId);
            const int pageSize = 9;
            if (pg < 1) pg = 1;
            int recsCount = receipts.Count();
            Pager pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            receipts = receipts.Skip(recSkip).Take(pager.PageSize);
            ViewBag.Pager = pager;
            if (receipts != null)
            {
                foreach (Receipt receipt in receipts.Where(x => x.Status == null || x.Status == "InProgress").ToList())
                {
                    ResponseDTO<ReceiptSubmissionsDTO, ErrorResponseDTO, UnprocessableEntityDTO> temp = await api.ReceiptSubmissions<ReceiptSubmissionsDTO, ErrorResponseDTO, UnprocessableEntityDTO>("api/v1/receiptsubmissions", receipt.SubmissionUuid, token);
                    if (temp != null)
                    {
                        if (temp.Accepted != null)
                        {
                            receipt.Status = temp.Accepted.Status;
                            IUnitOfWork.Receipts.Update(receipt);
                        }
                        else if (temp.BadRequest != null)
                        {
                            return RedirectToAction("Index", "Error", temp.BadRequest);
                        }
                    }
                }
                IUnitOfWork.Save();
                return View(receipts);
            }
            return View(new List<Receipt>());
        }

        #endregion

        #region New
        [HttpGet]
        public IActionResult New()
        {
            var temp = IUnitOfWork.Receipts.FindAll(x => x.ApplicationUserId == userId).OrderByDescending(x => x.ReceiptId).FirstOrDefault();
            if (temp == null)
            {
                return View(new ReceiptVM() { ReceiptNumber = "000001" });
            }
            int lastNum;
            string lastNumber = temp.Header.ReceiptNumber;
            int nextNumber = int.TryParse(lastNumber, out lastNum) ? lastNum + 1 : 0;

            return View(new ReceiptVM() { ReceiptNumber = $"{nextNumber.ToString().PadLeft(6, '0')}" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(TokenAttribute))]
        public async Task<IActionResult> New(ReceiptVM receiptVM)
        {
            if (ModelState.IsValid)
            {
                string token = HttpContext.Session.GetString("access_token");



                ApplicationUser user = await IUnitOfWork.UserManager.FindByIdAsync(userId.ToString());
                Receipt receipt = mapper.Map<Receipt>(user);
                mapper.Map(receiptVM,receipt);
                receipt.Header.Uuid = UuidService.GenerateUUID(receipt);
                if (token != null)
                {
                    ResponseDTO<ReceiptSubmitDTO, ErrorResponseDTO, UnprocessableEntityDTO> subTemp = await api.ReceiptSubmit<ReceiptSubmitDTO, ErrorResponseDTO, UnprocessableEntityDTO>("api/v1/receiptsubmissions", JsonConvert.SerializeObject(
                    new
                    {
                        receipts = new List<Receipt>() { receipt }
                    }, jsonSerializer), token);
                    if (subTemp != null)
                    {
                        if (subTemp.Accepted != null)
                        {
                            if (subTemp.Accepted.acceptedDocuments?.Count > 0)
                            {
                                receipt.SubmissionUuid = subTemp.Accepted.submissionId;
                                IUnitOfWork.Receipts.Insert(receipt);
                                IUnitOfWork.Save();
                                TempData["Message"] = JsonConvert.SerializeObject(new MessageVM() { Icon = "success", Title = "تم الارسال بنجاح.", Message = receipt.SubmissionUuid }) ;
                                return RedirectToAction(nameof(New));
                            }
                            else if (subTemp.Accepted.rejectedDocuments?.Count > 0)
                            {
                                List<MessageVM> messageVMs = new List<MessageVM>();
                                foreach (ReceiptsRejectedDto dto in subTemp.Accepted.rejectedDocuments)
                                {
                                    foreach (HttpCustomErrorResponseModel error in dto.error.Details)
                                    {
                                        messageVMs.Add(new MessageVM
                                        {
                                            Icon = "error",
                                            Title = error.Code,
                                            Message = error.Message
                                        });


                                    }
                                }
                                TempData["Message"] = JsonConvert.SerializeObject(messageVMs[0]);

                            }
                        }
                        else if (subTemp.BadRequest != null)
                        {
                            return RedirectToAction("Index", "Error", subTemp.BadRequest);
                        }
                        else if(subTemp.UnprocessableEntity != null)
                        {
                            TempData["Message"] = JsonConvert.SerializeObject(new MessageVM { Icon = "error", Title = "خطأ", Message = subTemp.UnprocessableEntity.Error });
                        }
                    }

                }
                else
                {
                    TempData["Message"] = JsonConvert.SerializeObject(new MessageVM { Icon = "error", Title = "TokenError", Message = "خطأ في الاتصال مع منظومة الايصال الالكتروني" });

                }

            }
            return View(receiptVM);
        }

        #endregion

        #region GetReceiptJson
        [HttpGet]
        public IActionResult GetReceiptJson(int id)
        {
            var receipt = IUnitOfWork.Receipts.FindAll(x => x.ReceiptId == id).FirstOrDefault();
            if (receipt == null)
            {
                return NotFound();
            }
            var json = JsonConvert.SerializeObject(receipt, jsonSerializer);
            return File(Encoding.UTF8.GetBytes(json), "application/json", $"{receipt.SubmissionUuid}.json");
        }
        #endregion

        #region Details
        [HttpGet]
        [ServiceFilter(typeof(TokenAttribute))]
        public async Task<IActionResult> Details(string Id)
        {
            string token = HttpContext.Session.GetString("access_token");

            ResponseDTO<ReceiptSubmissionsDTO, ErrorResponseDTO, UnprocessableEntityDTO> temp = await api.ReceiptSubmissions<ReceiptSubmissionsDTO, ErrorResponseDTO, UnprocessableEntityDTO>("api/v1/receiptsubmissions", Id, token);
            if (temp != null)
            {
                if (temp.Accepted != null && temp.Accepted.Status == "Invalid")
                {
                    foreach (ReceiptDTO receiptDTO in temp.Accepted.Receipts)
                    {
                        foreach (ErrorDTO errorDTO in receiptDTO.Errors)
                        {
                            foreach (InnerErrorDTO innerErrorDTO in errorDTO.Error.InnerError)
                            {
                                ModelState.AddModelError("", innerErrorDTO.ErrorAr);
                            }
                        }
                    }
                }
                else if (temp.BadRequest != null)
                {
                    return RedirectToAction("Index", "Error", temp.BadRequest);
                }
            }
            Receipt receipt = IUnitOfWork.Receipts.FindAll(x => x.SubmissionUuid == Id).FirstOrDefault();
            return View(receipt);
        }
        #endregion

        #region Return
        [HttpGet]
        [ServiceFilter(typeof(TokenAttribute))]
        public async Task<IActionResult> Return(string Id, int pg)
        {
            string token = HttpContext.Session.GetString("access_token");

            Receipt receipt = IUnitOfWork.Receipts.FindAll(x => x.SubmissionUuid == Id).FirstOrDefault();

            receipt.DocumentType.ReceiptType = "r";
            receipt.Header.ReferenceUUID = receipt.Header.Uuid;
            receipt.Header.Uuid = "";
            receipt.Header.Uuid = UuidService.GenerateUUID(receipt);


            if (token != null)
            {
                ResponseDTO<ReceiptSubmitDTO, ErrorResponseDTO, UnprocessableEntityDTO> subTemp =
                    await api.ReceiptSubmit<ReceiptSubmitDTO, ErrorResponseDTO, UnprocessableEntityDTO>("api/v1/receiptsubmissions", JsonConvert.SerializeObject(
                new
                {
                    receipts = new List<Receipt>() { receipt }
                }, jsonSerializer), token);


                if (subTemp != null)
                {
                    if (subTemp.Accepted != null)
                    {
                        if (subTemp.Accepted.acceptedDocuments?.Count > 0)
                        {
                            receipt.SubmissionUuid = subTemp.Accepted.submissionId;
                            IUnitOfWork.Receipts.Update(receipt);
                            IUnitOfWork.Save();

                            return RedirectToAction(nameof(Index), new { pg = pg });
                        }
                        else if (subTemp.Accepted.rejectedDocuments?.Count > 0)
                        {
                            TempData["error"] = JsonConvert.SerializeObject(subTemp.Accepted.rejectedDocuments);
                            return RedirectToAction(nameof(Index), new { pg = pg });

                        }
                    }
                    else if (subTemp.BadRequest != null)
                    {
                        return RedirectToAction("Index", "Error", subTemp.BadRequest);
                    }
                }
                TempData["errorTime"] = "من فضلك انتظر عشر دقائق حتي تتمكن من ارسال نفس الايصال";
                return RedirectToAction(nameof(Index), new { pg = pg });
            }
            else
            {
                TempData["errorConnection"] = "خطأ في الاتصال مع منظومة الايصال الالكتروني";
                return RedirectToAction(nameof(Index), new { pg = pg });

            }
        }

        #endregion

    }
}
