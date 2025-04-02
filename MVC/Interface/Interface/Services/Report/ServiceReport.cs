using AspNetCore.Reporting;
using System.Data;
using System.Drawing.Imaging;
using System.Drawing;
using System.Text;
using QRCoder;
using System.Configuration;
using Interface.ApplicationConfiguration;
using Entities.Models.Document.BaseComponent;
using Entities.Models.Document.Receipt;

namespace Interface.Services.Report
{
    public class ServiceReport : IServiceReport
    {
        IAppConfig appConfig;
        public ServiceReport(IAppConfig appConfig)
        {
            this.appConfig = appConfig;
        }
        public byte[] CreateReportFile(string pathRDLC,IEnumerable<Receipt> data)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            LocalReport report = new(pathRDLC);

            data.FirstOrDefault().QRCode = GenerateQRCode(Path.Combine(appConfig.anonymousUrl,"receipts","search",data.FirstOrDefault().Header.Uuid, "share", data.FirstOrDefault().Header.DateTimeIssued.ToFileTimeUtc().ToString()));
            AddDataSource<Receipt>(report, nameof(Receipt), data.Where(x => true));
            AddDataSource<Header>(report, nameof(Header), data.Where(x => true).Select(x => x.Header));
            AddDataSource<Seller>(report, nameof(Seller), data.Where(x=> true).Select(x=>x.Seller));
            AddDataSource<DocumentType>(report, nameof(DocumentType), data.Where(x => true).Select(x => x.DocumentType));
            AddDataSource<Buyer>(report, nameof(Buyer), data.Where(x => true).Select(x => x.Buyer));
            AddDataSource<Item>(report, nameof(Item),data.FirstOrDefault().ItemData);
            AddDataSource<TaxTotal>(report, nameof(TaxTotal),data.FirstOrDefault().TaxTotals);
            AddDataSource<BranchAddress>(report, nameof(BranchAddress), data.Where(x=> true).Select(x=>x.Seller.BranchAddress));
            

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
