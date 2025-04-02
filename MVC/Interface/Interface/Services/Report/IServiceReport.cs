using Entities.Models.Document.BaseComponent;
using Entities.Models.Document.Receipt;

namespace Interface.Services.Report
{
    public interface IServiceReport
    {
        byte[] CreateReportFile(string pathRDLC, IEnumerable<Receipt> data);

        byte[] GenerateQRCode(string url);

    }
}
