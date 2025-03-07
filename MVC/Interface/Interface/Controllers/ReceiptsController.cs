using DAL.Repository;
using Entities.InterfacesOfRepo;
using Entities.Models;
using Entities.Models.Receipt;
using ETA.eReceipt.IntegrationToolkit.Application.Models;
using Interface.Dto;
using Interface.Services;
using Interface.Services.ApiCall;
using Interface.ViewModels.ReceiptVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Common;
using System.Security.Claims;
using System.Text;

namespace Interface.Controllers
{
    [Authorize]
    public class ReceiptsController : BaseController
    {
        IWebHostEnvironment env;
        IApiCall api;
        public ReceiptsController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, IApiCall apiCall) : base(unitOfWork)
        {
            api = apiCall;
            env = webHostEnvironment;

        }

        [HttpGet]
        [ServiceFilter(typeof(TokenAttribute))]

        public async Task<IActionResult> Index(int pg = 1)
        {
            int id = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            IQueryable<receipt> receipts = IUnitOfWork.Receipts.FindAll(x => x.applicationUserId == id, new string[] { "header" , "documentType" , "buyer" })
                .OrderByDescending(x => x.receiptId);
            const int pageSize = 9;
            if (pg < 1) pg = 1;
            int recsCount = receipts.Count();
            Pager pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            receipts = receipts.Skip(recSkip).Take(pager.PageSize);
            ViewBag.Pager = pager;
            if (receipts != null)
            {
                foreach (receipt receipt in receipts.Where(x => x.status == null || x.status == "InProgress").ToList())
                {
                    string Token = HttpContext.Session.GetString("access_token");
                    ResponseDTO<ReceiptSubmissionsDTO, ErrorResponseDTO> temp = await api.ReceiptSubmissions<ReceiptSubmissionsDTO, ErrorResponseDTO>("api/v1/receiptsubmissions", receipt.submissionUuid, Token);
                    if (temp != null)
                    {
                        if (temp.SuccessResponse != null)
                        {
                            receipt.status = temp.SuccessResponse.Status;
                            IUnitOfWork.Receipts.Update(receipt);
                        }
                        else if (temp.ErrorResponse != null)
                        {
                            return RedirectToAction("Index", "Error", temp.ErrorResponse);
                        }
                    }
                }
                IUnitOfWork.Save();
                return View(receipts);
            }
            return View(new List<receipt>());
        }


        [HttpGet]
        public IActionResult New()
        {
            int id = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var temp = IUnitOfWork.Receipts.FindAll(x => x.applicationUserId == id, new[] { "header" }).OrderByDescending(x => x.receiptId).FirstOrDefault();
            if (temp == null)
            {
                return View(new ReceiptVM() { ReceiptNumber = "000001" });
            }
            int lastNum ;
            string lastNumber = temp.header.receiptNumber;
            int nextNumber = int.TryParse(lastNumber,out lastNum) ? lastNum+1 : 0 ;

            return View(new ReceiptVM() { ReceiptNumber = $"{nextNumber.ToString().PadLeft(6, '0')}" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(TokenAttribute))]
        public async Task<IActionResult> New(ReceiptVM t)
        {
            if (ModelState.IsValid)
            {
                receipt receipt = await CastingToModel(t);
                receipt.header.uuid = UuidService.GenerateUUID(receipt);

                string Token = HttpContext.Session.GetString("access_token");

                if (Token != null)
                {
                    ResponseDTO<ReceiptSubmitDTO, ErrorResponseDTO> subTemp = await api.ReceiptSubmit<ReceiptSubmitDTO, ErrorResponseDTO>("api/v1/receiptsubmissions", JsonConvert.SerializeObject(
                    new
                    {
                        receipts = new List<receipt>() {
                                        receipt }
                    }, new JsonSerializerSettings()
                    {
                        Formatting = Formatting.Indented,
                        NullValueHandling = NullValueHandling.Ignore
                    }), Token);


                    if (subTemp != null)
                    {
                        if (subTemp.SuccessResponse != null)
                        {
                            if (subTemp.SuccessResponse.acceptedDocuments?.Count > 0)
                            {
                                receipt.submissionUuid = subTemp.SuccessResponse.submissionId;
                                IUnitOfWork.Receipts.Insert(receipt);
                                IUnitOfWork.Save();
                                TempData["success"] = receipt.submissionUuid;
                                return RedirectToAction(nameof(New));
                            }
                            else if (subTemp.SuccessResponse.rejectedDocuments?.Count > 0)
                            {
                                foreach (ReceiptsRejectedDto dto in subTemp.SuccessResponse.rejectedDocuments)
                                {
                                    foreach (HttpCustomErrorResponseModel error in dto.error.Details)
                                    {
                                        ModelState.AddModelError("error.Target", error.Message);
                                    }
                                }
                            }
                        }
                        else if (subTemp.ErrorResponse != null)
                        {
                            return RedirectToAction("Index", "Error", subTemp.ErrorResponse);
                        }
                    }

                }
                else
                {
                    ModelState.AddModelError("", "خطأ في الاتصال مع منظومة الايصال الالكتروني");

                }

            }
            return View(t);
        }

        [HttpGet]
        public IActionResult GetReceiptJson(int id)
        {
            var receipt = IUnitOfWork.Receipts.FindAll(x => x.receiptId == id, new string[] { "header", "documentType", "seller", "buyer", "itemData", "taxTotals", "itemData.taxableItems", "itemData.commercialDiscountData" }).FirstOrDefault();
            if (receipt == null)
            {
                return NotFound();
            }
            var json = JsonConvert.SerializeObject(receipt,new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });
            return File(Encoding.UTF8.GetBytes(json), "application/json", $"{receipt.submissionUuid}.json");
        }

        [HttpGet]
        [ServiceFilter(typeof(TokenAttribute))]
        public async Task<IActionResult> Details(string Id)
        {
            string Token = HttpContext.Session.GetString("access_token");
            ResponseDTO<ReceiptSubmissionsDTO, ErrorResponseDTO> temp = await api.ReceiptSubmissions<ReceiptSubmissionsDTO, ErrorResponseDTO>("api/v1/receiptsubmissions", Id, Token);
            if (temp != null)
            {
                if (temp.SuccessResponse != null && temp.SuccessResponse.Status == "Invalid")
                {
                    foreach (ReceiptDTO receiptDTO in temp.SuccessResponse.Receipts)
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
                else if (temp.ErrorResponse != null)
                {
                    return RedirectToAction("Index", "Error", temp.ErrorResponse);
                }
            }
            receipt receipt = IUnitOfWork.Receipts.FindAll(x => x.submissionUuid == Id, new string[]
                {   nameof(header),
                    $"{nameof(seller)}.{nameof(branchAddress)}",
                    nameof(documentType),
                    nameof(buyer),
                    "itemData",
                })
                .FirstOrDefault();
            return View(receipt);
        }
        private async Task<receipt> CastingToModel(ReceiptVM receiptVM)
        {
            receipt receipt = new receipt();
            applicationUser user = await IUnitOfWork.UserManager.FindByIdAsync(User.Claims.FirstOrDefault(X => X.Type == ClaimTypes.NameIdentifier).Value);

            receipt.applicationUserId = user.Id;

            /////////////////////////////////
            receipt.header = new header()
            {
                dateTimeIssued = receiptVM.DateTime.AddHours(-2),
                receiptNumber = receiptVM.ReceiptNumber,
                uuid = "",
                currency = "EGP",
                referenceOldUUID = "",
                previousUUID = "",
                exchangeRate = 0,
                orderdeliveryMode = "",
                sOrderNameCode = ""


            };
            /////////////////////////////////
            /////////////////////////////////
            receipt.documentType = new documentType()
            {
                receiptType = receiptVM.Type,
                typeVersion = receiptVM.Version
            };
            /////////////////////////////////
            /////////////////////////////////
            receipt.seller = new seller()
            {
                rin = user.RegistrationNumber,
                activityCode = user.ActivityCodes,
                companyTradeName = user.Name,
                deviceSerialNumber = user.POSSerial,
                branchAddress = new branchAddress()
                {
                    buildingNumber = user.BuildingNumber,
                    country = user.Country,
                    regionCity = user.RegionCity,
                    governate = user.Governate,
                    street = user.Street
                },
                branchCode = user.BranchCode,
            };
            /////////////////////////////////
            /////////////////////////////////
            receipt.buyer = new buyer()
            {
                name = receiptVM.IssuedName,
                type = receiptVM.IssuedType,
                id = receiptVM.IssuedNumber,
                mobileNumber = receiptVM.IssuedPhone,
                paymentNumber = null,
            };

            /// /////////////////////////////
            /////////////////////////////////
            foreach (var item in receiptVM.itemData)
            {
                item temp = new item();
                temp.internalCode = item.InternalCode;
                temp.description = item.Description;
                temp.itemCode = item.ItemCode;
                temp.unitType = item.UnitType;
                temp.itemType = item.ItemType;
                temp.totalSale = item.TotalSale;
                temp.netSale = item.NetSale;
                temp.quantity = item.Quantity;
                temp.total = item.Total;
                temp.unitPrice = item.UnitPrice;

                foreach (var tax in item.TaxableItems)
                {
                    taxableItem taxableItem = new taxableItem();
                    taxableItem.taxType = tax.TaxType;
                    taxableItem.subType = tax.SubType;
                    taxableItem.amount = tax.Amount;
                    taxableItem.rate = tax.Rate;
                    temp.taxableItems.Add(taxableItem);
                }
                ;

                foreach (var discount in item.CommercialDiscountData)
                {
                    commercialDiscountData comm = new commercialDiscountData();
                    comm.description = discount.Description;
                    comm.amount = discount.Amount;
                    comm.rate = discount.Rate;
                    temp.commercialDiscountData.Add(comm);
                }
                ;

                receipt.itemData.Add(temp);
            }
            /////////////////////////////////

            /////////////////////////////////
            receipt.totalCommercialDiscount = receiptVM.TotalCommercialDiscount;
            /////////////////////////////////

            /////////////////////////////////
            ///
            foreach (TaxTotalVM taxTotalVM in receiptVM.TaxTotals)
            {
                receipt.taxTotals.Add(new taxTotal()
                {
                    taxType = taxTotalVM.TaxType,
                    amount = taxTotalVM.Amount,
                });

            }
            /////////////////////////////////
            receipt.totalSales = receiptVM.TotalSales;
            receipt.netAmount = receiptVM.NetAmount;
            receipt.totalAmount = receiptVM.TotalAmount;
            receipt.paymentMethod = receiptVM.PaymentMethod;
            return receipt;
        }
        [HttpGet]
        public IActionResult Submitted(string subId)
        {
            return View(new { subId });
        }
        [HttpGet]
        [ServiceFilter(typeof(TokenAttribute))]
        public async Task<IActionResult> Return(string Id,int pg)
        {
            string token = HttpContext.Session.GetString("access_token");
            receipt receipt = IUnitOfWork.Receipts.FindAll(x => x.submissionUuid == Id, new string[] {
                nameof(header),
                nameof(seller),
                $"{nameof(seller)}.{nameof(branchAddress)}",
                nameof(buyer),
                nameof(documentType),
                "itemData",
                "itemData.taxableItems",
                "itemData.commercialDiscountData",
                "taxTotals",
            }).FirstOrDefault();



            receipt.documentType.receiptType = "r";
            receipt.header.referenceUUID = receipt.header.uuid;
            receipt.header.uuid = "";
            receipt.header.uuid = UuidService.GenerateUUID(receipt);


            if (token != null)
            {
                ResponseDTO<ReceiptSubmitDTO, ErrorResponseDTO> subTemp = await api.ReceiptSubmit<ReceiptSubmitDTO, ErrorResponseDTO>("api/v1/receiptsubmissions", JsonConvert.SerializeObject(
                new
                {
                    receipts = new List<receipt>() {
                                        receipt }
                }, new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore
                }), token);


                if (subTemp != null)
                {
                    if (subTemp.SuccessResponse != null)
                    {
                        if (subTemp.SuccessResponse.acceptedDocuments?.Count > 0)
                        {
                            receipt.submissionUuid = subTemp.SuccessResponse.submissionId;
                            IUnitOfWork.Receipts.Update(receipt);
                            IUnitOfWork.Save();
                            
                            return RedirectToAction(nameof(Index), new { pg = pg });
                        }
                        else if (subTemp.SuccessResponse.rejectedDocuments?.Count > 0)
                        {
                            TempData["error"] = JsonConvert.SerializeObject(subTemp.SuccessResponse.rejectedDocuments);
                            return RedirectToAction(nameof(Index), new { pg = pg });

                        }
                    }
                    else if (subTemp.ErrorResponse != null)
                    {
                        return RedirectToAction("Index", "Error", subTemp.ErrorResponse);
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
        [HttpGet]
        public IActionResult gettaxtype()
        {
            var path = Path.Combine(env.WebRootPath, "json file", "TaxType.json");
            var TaxType = System.IO.File.ReadAllText(path);

            path = Path.Combine(env.WebRootPath, "json file", "SubTaxType.json");
            var SubTaxType = System.IO.File.ReadAllText(path);

            return Json(new
            {
                taxtype = TaxType,
                subtaxtype = SubTaxType,
            });
        }







    }
}
