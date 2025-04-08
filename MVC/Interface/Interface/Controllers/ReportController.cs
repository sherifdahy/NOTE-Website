using AutoMapper;
using Entities.InterfacesOfRepo;
using Entities.Models.Document;
using Entities.Models.Document.BaseComponent;
using Interface.Services.Report;
using Microsoft.AspNetCore.Mvc;

namespace Interface.Controllers
{
    public class ReportController : BaseController
    {
        IServiceReport ServiceReport;
        IWebHostEnvironment WebHostEnvironment;
        public ReportController(IUnitOfWork unitOfWork,IServiceReport serviceReport,IWebHostEnvironment webHostEnvironment):base(unitOfWork)
        {
            WebHostEnvironment = webHostEnvironment;
            ServiceReport = serviceReport;

        }
        [HttpGet]
        public IActionResult exportData(string Id)
        {
            string path = Path.Combine(WebHostEnvironment.WebRootPath, "reports", "receipt.rdlc");
            IEnumerable<Receipt> receipts = IUnitOfWork.Receipts.FindAll(x => x.SubmissionUuid == Id);
            var byteRes = ServiceReport.CreateReportFile(path,receipts);
            return File(byteRes, System.Net.Mime.MediaTypeNames.Application.Octet, $"{receipts.FirstOrDefault().Header.ReceiptNumber} - {receipts.FirstOrDefault().Buyer.Name}.pdf");
        }
        
        

    }
}
