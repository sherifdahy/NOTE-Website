using Interface.ViewModels.ReceiptVM;

namespace Interface.ViewModels
{
    public class TaxpayerProfileVM
    {
        public string Name { get; set; }
        public string RegistrationNumber { get; set; }
        public BranchAddressVM BranchAddress { get; set; }
        public string ActivityCodes { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string POSSerial { get; set; }
        public string POSClientId { get; set; }
        public string POSClientSecret { get; set; }
        public string BranchCode { get; set; }
    }
}
