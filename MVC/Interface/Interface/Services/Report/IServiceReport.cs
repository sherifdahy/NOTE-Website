using Entities.Models.Document;
using Entities.Models.Document.BaseComponent;

namespace Interface.Services.Report
{
    public interface IServiceReport
    {
        byte[] CreateReportFile(string pathRDLC, IEnumerable<Receipt> data);

        byte[] GenerateQRCode(string url);

    }
}
