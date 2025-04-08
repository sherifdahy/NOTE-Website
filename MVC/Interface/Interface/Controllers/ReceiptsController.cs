using AutoMapper;
using Entities.InterfacesOfRepo;
using Entities.Models;
using Entities.Models.Document;
using Interface.Dto;
using Interface.Dto.ReceiptSubmissions;
using Interface.Dto.ReceiptSubmit;
using Interface.Services;
using Interface.Services.ApiCall;
using Interface.ViewModels;
using Interface.ViewModels.ReceiptVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Security.Claims;
using System.Text;

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
        public async Task<IActionResult> Index(int pg = 1,int pageSize = 10)
        {
            string token = HttpContext.Session.GetString("access_token");
            int recsCount = await IUnitOfWork.Receipts.CountAsync(x=>x.ApplicationUserId == userId);
            Pager pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            ViewBag.Pager = pager;
            IEnumerable<Receipt> receipts = await IUnitOfWork.Receipts.FindAllAsync(x => x.ApplicationUserId == userId  ,recSkip,pageSize,x=>x.ReceiptId,true);
            if (receipts.Any())
            {
                foreach (Receipt receipt in receipts.Where(x => x.Status == null || x.Status == "InProgress"))
                {
                    ResponseDTO<ReceiptSubmissionsDTO> temp = await api.ReceiptSubmissions<ReceiptSubmissionsDTO>("api/v1/receiptsubmissions", receipt.SubmissionUuid, token);
                    if (temp?.SuccessfulResponse != null)
                    {
                        receipt.Status = temp.SuccessfulResponse.Status;
                    }
                    else if (temp?.ErrorResponse != null)
                    {
                        return RedirectToAction("Index", "Error", temp.ErrorResponse);
                    }
                }
                IUnitOfWork.Receipts.UpdateRange(receipts);
                IUnitOfWork.Save();
            }
            return View(receipts);
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
                    ResponseDTO<SuccessfulResponseDTO> subTemp = await api.ReceiptSubmit<SuccessfulResponseDTO>("api/v1/receiptsubmissions", JsonConvert.SerializeObject(
                    new
                    {
                        receipts = new List<Receipt>() { receipt }
                    }, jsonSerializer), token);
                    if (subTemp != null)
                    {
                        if (subTemp.SuccessfulResponse != null)
                        {
                            if (subTemp.SuccessfulResponse.acceptedDocuments?.Count > 0)
                            {
                                receipt.SubmissionUuid = subTemp.SuccessfulResponse.submissionId;
                                await IUnitOfWork.Receipts.AddAsync(receipt);
                                IUnitOfWork.Save();
                                TempData["Message"] = JsonConvert.SerializeObject(new MessageVM() { Icon = "success", Title = "تم الارسال بنجاح.", Message = receipt.SubmissionUuid }) ;
                                return RedirectToAction(nameof(New));
                            }
                            else if (subTemp.SuccessfulResponse.rejectedDocuments?.Count > 0)
                            {
                                List<MessageVM> messageVMs = new List<MessageVM>();
                                foreach (DocumentRejectedDTO dto in subTemp.SuccessfulResponse.rejectedDocuments)
                                {
                                    foreach(StandardErrorDetailsDTO error in dto.error.Details)
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
                        else if (subTemp.ErrorResponse != null)
                        {
                            if (subTemp.ErrorResponse is StandardErrorResponseDTO errorResponseDTO)
                            {
                                string x;
                            }
                            else if (subTemp.ErrorResponse is SimpleErrorDTO simpleErrorDTO)
                            {
                                TempData["Message"] = JsonConvert.SerializeObject(new MessageVM { Icon = "error", Title = "خطأ", Message = simpleErrorDTO.Error });
                            }
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

            ResponseDTO<ReceiptSubmissionsDTO> temp = await api.ReceiptSubmissions<ReceiptSubmissionsDTO>("api/v1/receiptsubmissions", Id, token);
            if (temp != null)
            {
                if (temp.SuccessfulResponse != null && temp.SuccessfulResponse.Status == "Invalid")
                {
                    foreach (DocumentSummaryDTO documentSummaryDTO in temp.SuccessfulResponse.Receipts)
                    {
                        foreach (DocumentSummaryErrorsDTO documentSummaryErrorsDTO in documentSummaryDTO.Errors)
                        {
                            foreach(InnerErrorDTO innerErrorDTO in documentSummaryErrorsDTO.Error.InnerError)
                            {
                                ModelState.AddModelError("", innerErrorDTO.ErrorAr);
                            }
                        }
                    }
                }
                else if (temp.ErrorResponse != null)
                {
                    if (temp.ErrorResponse is StandardErrorResponseDTO errorResponseDTO)
                    {
                        string x;
                    }
                    else if (temp.ErrorResponse is SimpleErrorDTO simpleErrorDTO)
                    {
                        TempData["Message"] = JsonConvert.SerializeObject(new MessageVM { Icon = "error", Title = "خطأ", Message = simpleErrorDTO.Error });
                    }
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
            
            if (token != null && receipt.DocumentType.ReceiptType != "r" && receipt.Status != "Invalid")
            {
                receipt.DocumentType.ReceiptType = "r";
                receipt.Header.ReferenceUUID = receipt.Header.Uuid;
                receipt.Header.Uuid = "";
                receipt.Header.Uuid = UuidService.GenerateUUID(receipt);


                ResponseDTO<SuccessfulResponseDTO> subTemp = await api.ReceiptSubmit<SuccessfulResponseDTO>("api/v1/receiptsubmissions", JsonConvert.SerializeObject(
                new
                {
                    receipts = new List<Receipt>() { receipt }
                }, jsonSerializer), token);


                if (subTemp != null)
                {
                    if (subTemp.SuccessfulResponse != null)
                    {
                        if (subTemp.SuccessfulResponse.acceptedDocuments?.Count > 0)
                        {
                            receipt.SubmissionUuid = subTemp.SuccessfulResponse.submissionId;
                            IUnitOfWork.Receipts.Update(receipt);
                            IUnitOfWork.Save();

                            return RedirectToAction(nameof(Index), new { pg = pg });
                        }
                        else if (subTemp.SuccessfulResponse.rejectedDocuments?.Count > 0)
                        {
                            TempData["Message"] = JsonConvert.SerializeObject(new MessageVM()
                            {
                                Icon = "error",
                                Title = "Oops..",
                                Message = subTemp.SuccessfulResponse.rejectedDocuments.FirstOrDefault().error.Details.FirstOrDefault().Message
                            });
                            return RedirectToAction(nameof(Index), new { pg = pg });

                        }
                    }
                    else if (subTemp.ErrorResponse != null)
                    {
                        if (subTemp.ErrorResponse is StandardErrorResponseDTO errorResponseDTO)
                        {
                            string x;
                        }
                        else if (subTemp.ErrorResponse is SimpleErrorDTO simpleErrorDTO)
                        {
                            TempData["Message"] = JsonConvert.SerializeObject(new MessageVM { Icon = "error", Title = "خطأ", Message = simpleErrorDTO.Error });
                        }
                    }
                }
            }
            else
            {
                if (receipt.DocumentType.ReceiptType == "r")
                {
                    TempData["Message"] = JsonConvert.SerializeObject(new MessageVM()
                    {
                        Icon = "error",
                        Title = "خطأ في عملية الاسترجاع",
                        Message = "لا يمكن استرجاع هذا المستند، حيث تم استرجاعه مسبقًا."
                    });
                }
                else if (receipt.Status == "Invalid")
                {
                    TempData["Message"] = JsonConvert.SerializeObject(new MessageVM()
                    {
                        Icon = "error",
                        Title = "خطأ في عملية الاسترجاع",
                        Message = "لا يمكن استرجاع مستند غير صحيح."
                    });
                }
                else if (token == null)
                {
                    TempData["Message"] = JsonConvert.SerializeObject(new MessageVM()
                    {
                        Icon = "error",
                        Title = "خطأ في عملية الاتصال",
                        Message = "خطأ في الاتصال مع منظومة الايصال الالكتروني"
                    });
                }
                
            }
            return RedirectToAction(nameof(Index), new { pg = pg });
        }

        #endregion

    }
}
