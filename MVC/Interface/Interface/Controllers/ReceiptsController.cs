using Entities.InterfacesOfRepo;
using Entities.Models;
using Entities.Models.Receipt;
using ETA.eReceipt.IntegrationToolkit.Application.Dtos;
using ETA.eReceipt.IntegrationToolkit.Application.Services;
using ETA.eReceipt.IntegrationToolkit.Domain.Enums;
using ETA.eReceipt.IntegrationToolkit.Infrastructure.Services;
using Interface.Dto;
using Interface.Services;
using Interface.Services.ApiCall;
using Interface.ViewModels;
using Interface.ViewModels.ReceiptVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Model.Tree;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Security.Claims;

namespace Interface.Controllers
{
    [Authorize]
    public class ReceiptsController : BaseController
    {
        IWebHostEnvironment env;
        IApiCall api;
        IToolkitHandler toolkit;
        public ReceiptsController(IUnitOfWork unitOfWork,IWebHostEnvironment webHostEnvironment,IApiCall apiCall,IToolkitHandler toolkitHandler) : base(unitOfWork)
        {
            api = apiCall;
            env = webHostEnvironment;
            toolkit = toolkitHandler;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(int ij)
        {
            int id = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            
            var query = IUnitOfWork.Receipts.FindAll(x => x.applicationUserId == id, new string[] { });
            int recordsTotals = query.Count();
            int skip = int.Parse(Request.Form["start"]);
            int take = int.Parse(Request.Form["length"]);
            string value = Request.Form["search[value]"];
            var receipts = query.Where(x => string.IsNullOrEmpty(value) ?
                true : x.header.receiptNumber.Contains(value) || x.header.uuid.Contains(value) || x.documentType.receiptType.Contains(value) ||
                x.buyer.name.Contains(value) || x.buyer.id.Contains(value)
            ).OrderByDescending(x => x.receiptId).Skip(skip).Take(take).Select(
                x => new { id = x.receiptId , receiptNumber = x.header.receiptNumber , dateTime = x.header.dateTimeIssued , type = x.documentType.receiptType , issuesName = x.buyer.name , issuesId = x.buyer.id ,total = x.totalAmount }
                ).ToList();

            return Json(new
            {
                recordsFiltered = recordsTotals,
                recordsTotals,
                data = receipts
            });
        }
        [HttpGet]
        public IActionResult New()
        {
            int id = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var t = IUnitOfWork.Receipts.FindAll(x => x.applicationUserId == id, new [] { "header" }).OrderByDescending(x => x.header.receiptNumber).FirstOrDefault();
            if (t == null)
            {
                return View(new ReceiptVM() { ReceiptNumber = "REC-0001" });
            }
            string lastNumber = t.header.receiptNumber.Split('-')[1];
            int nextNumber = int.Parse(lastNumber) + 1;

            return View(new ReceiptVM() { ReceiptNumber = $"REC-{nextNumber.ToString().PadLeft(4, '0')}" });
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> New(ReceiptVM t)
        {

            if(ModelState.IsValid)
            {
                applicationUser applicationUser = await IUnitOfWork.UserManager.FindByIdAsync(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);


                //var initializeRequestDto = new InitializeRequestDto();

                //initializeRequestDto.SaveCredential = false;
                //initializeRequestDto.ResumeWithInvalidCache = false;
                //initializeRequestDto.MaximumSubmissionDocumentCount = 500;
                //initializeRequestDto.CachLookupDurationInHours = 1;


                var authenticateRequestDto =
                 new AuthenticateRequestDto
                 {
                     ClientId = applicationUser.POSClientId,
                     ClientSecret = applicationUser.POSClientSecret,
                     PosSerial = applicationUser.POSSerial,
                     PosOsVersion = "Windows",
                     PosModelFramework = "",
                     PresharedKey = ""
                 };

                var respose = await toolkit.Authenticate(authenticateRequestDto);

                receipt receipt = await CastingToModel(t);

                var response = await toolkit.GenerateUuid(JsonConvert.SerializeObject(receipt,new JsonSerializerSettings() {
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore
                }));

                receipt.header.uuid = response.Uuid?.ToString();
                //IssueReceiptResponseDto temp = await toolkit.IssueReceipt(JsonConvert.SerializeObject(receipt, new JsonSerializerSettings()
                //{
                //    Formatting = Formatting.Indented,
                //    NullValueHandling = NullValueHandling.Ignore
                //}));
                //if (temp.Details == null)
                //{
                    


                    SubmissionDTO submissionDTO = await api.Submissions<SubmissionDTO>("api/v1/receiptsubmissions", JsonConvert.SerializeObject(
                        new
                        {
                            receipts = new List<receipt>() {
                            receipt
                            }
                        }, new JsonSerializerSettings()
                        {
                            Formatting = Formatting.Indented,
                            NullValueHandling = NullValueHandling.Ignore

                        }), respose.Token);
                    IUnitOfWork.Receipts.Insert(receipt);
                    IUnitOfWork.Save();
                    return RedirectToAction(nameof(Submitted),new { subId = submissionDTO.submissionId });

                //}

                //else
                //{
                //    foreach (dynamic error in (IEnumerable)temp.Details)
                //    {
                //        foreach (dynamic mess in error.Details)
                //        {
                //            ModelState.AddModelError("", mess.Message.ToString());
                //        }
                //    }
                //}
            }
            return View(t);
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
                };

                foreach(var discount in item.CommercialDiscountData)
                {
                    commercialDiscountData comm = new commercialDiscountData();
                    comm.description = discount.Description;
                    comm.amount = discount.Amount;
                    comm.rate = discount.Rate;
                    temp.commercialDiscountData.Add(comm);
                };

                receipt.itemData.Add(temp);
            }
            /////////////////////////////////

            /////////////////////////////////
            receipt.totalCommercialDiscount = receiptVM.TotalCommercialDiscount ;
            /////////////////////////////////

            /////////////////////////////////
            ///
            foreach(TaxTotalVM taxTotalVM in receiptVM.TaxTotals)
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
            return View(new { subId});
        }

        [HttpGet]
        public IActionResult gettaxtype()
        {
            var path = Path.Combine(env.WebRootPath,"json file","TaxType.json");
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
