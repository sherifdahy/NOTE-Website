using Entities.Models.Receipt;

namespace Interface.Services.Report
{
    public interface IServiceReport
    {
        byte[] CreateReportFile(string pathRDLC, IEnumerable<receipt> data);

        byte[] GenerateQRCode(string url);

    }
}
