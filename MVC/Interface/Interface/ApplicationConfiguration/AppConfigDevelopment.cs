namespace Interface.ApplicationConfiguration
{
    public class AppConfigDevelopment : IAppConfig
    {
        public AppConfigDevelopment()
        {
            baseUrl      = "https://api.preprod.invoicing.eta.gov.eg";
            idSrvBaseUrl = "https://id.preprod.eta.gov.eg";
            anonymousUrl = "https://preprod.invoicing.eta.gov.eg";
        }
        public string baseUrl { get; set; }
        public string idSrvBaseUrl { get; set; }
        public string anonymousUrl { get; set; }
    }
}
