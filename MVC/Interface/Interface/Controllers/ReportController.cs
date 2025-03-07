using Entities.InterfacesOfRepo;
using Entities.Models.Receipt;
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
            IEnumerable<receipt> receipts = IUnitOfWork.Receipts.FindAll(x => x.submissionUuid == Id, new string[] {
                nameof(header),
                nameof(seller),
                nameof(documentType),
                nameof(buyer),
                "itemData",
                "taxTotals",
                "seller.branchAddress"
                 });
            var byteRes = ServiceReport.CreateReportFile(path,receipts);
            return File(byteRes, System.Net.Mime.MediaTypeNames.Application.Octet, $"{receipts.FirstOrDefault().header.receiptNumber} - {receipts.FirstOrDefault().buyer.name}.pdf");
        }
        
        

    }
}
