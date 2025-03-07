using Newtonsoft.Json;
using QRCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.SymbolStore;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Receipt
{
    [JsonObject]
    public class receipt
    {
        [JsonIgnore]
        public int receiptId { get; set; }
        [JsonIgnore]
        [ForeignKey("applicationUser")]
        public int applicationUserId { get; set; }
        [JsonIgnore]
        [ForeignKey("header")]
        public int headerId { get; set; }
        public virtual header header {  get; set; }
        [JsonIgnore]
        [ForeignKey("documentType")]
        public int documentTypeId { get; set; }
        public virtual documentType documentType { get; set; }
        [JsonIgnore]
        [ForeignKey("seller")]
        public int sellerId { get; set; }
        public virtual seller seller { get; set; }
        [JsonIgnore]
        [ForeignKey("buyer")]
        public int buyerId { get; set; }
        public virtual buyer buyer { get; set; }
        public virtual ICollection<item> itemData { get; set; } = new HashSet<item>();
        public decimal totalSales { get; set; }
        public decimal totalCommercialDiscount { get; set; }
        public decimal netAmount { get; set; }
        public decimal totalAmount { get; set; }
        public virtual ICollection<taxTotal> taxTotals { get; set; } = new HashSet<taxTotal>();
        public string paymentMethod { get; set; }

        [JsonIgnore]
        public string status { get; set; }
        [JsonIgnore]
        public string submissionUuid {  get; set; }

        [JsonIgnore]
        public byte[] QRCode
        {
            get
            {
                using (QRCodeGenerator qRCodeGenerator = new QRCodeGenerator())
                {
                    using (QRCodeData qrCodeData = qRCodeGenerator.CreateQrCode(header.uuid, QRCodeGenerator.ECCLevel.M))
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
}
