using AspNetCore.Reporting;
using Entities.Models.Receipt;
using System.Data;
using System.Drawing.Imaging;
using System.Drawing;
using System.Text;
using QRCoder;
using System.Configuration;
using Interface.ApplicationConfiguration;

namespace Interface.Services.Report
{
    public class ServiceReport : IServiceReport
    {
        IAppConfig appConfig;
        public ServiceReport(IAppConfig appConfig)
        {
            this.appConfig = appConfig;
        }
        public byte[] CreateReportFile(string pathRDLC,IEnumerable<receipt> data)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            LocalReport report = new(pathRDLC);

            data.FirstOrDefault().QRCode = GenerateQRCode(Path.Combine(appConfig.anonymousUrl,"receipts","search",data.FirstOrDefault().header.uuid, "share", data.FirstOrDefault().header.dateTimeIssued.ToFileTimeUtc().ToString()));
            AddDataSource<receipt>(report, nameof(receipt), data.Where(x => true));
            AddDataSource<header>(report, nameof(header), data.Where(x => true).Select(x => x.header));
            AddDataSource<seller>(report, nameof(seller), data.Where(x=> true).Select(x=>x.seller));
            AddDataSource<documentType>(report, nameof(documentType), data.Where(x => true).Select(x => x.documentType));
            AddDataSource<buyer>(report, nameof(buyer), data.Where(x => true).Select(x => x.buyer));
            AddDataSource<item>(report, nameof(item),data.FirstOrDefault().itemData);
            AddDataSource<taxTotal>(report, nameof(taxTotal),data.FirstOrDefault().taxTotals);
            AddDataSource<branchAddress>(report, nameof(branchAddress), data.Where(x=> true).Select(x=>x.seller.branchAddress));
            

            return report.Execute(RenderType.Pdf, 1).MainStream;
        }
        private void AddDataSource<T>(LocalReport report,string nameOfDataSet, IEnumerable<T> data)
        {
            report.AddDataSource(nameOfDataSet, data);
        }

        public byte[] GenerateQRCode(string url)
        {
            using (QRCodeGenerator qRCodeGenerator = new())
            {
                using (QRCodeData qrCodeData = qRCodeGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.M))
                using (QRCode qrCode = new QRCode(qrCodeData))
                {
                    Bitmap qrCodeImage = qrCode.GetGraphic(20);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        qrCodeImage.Save(ms, ImageFormat.Bmp);
                        return ms.ToArray();
                    }
                }
            }
        }
    }
}
