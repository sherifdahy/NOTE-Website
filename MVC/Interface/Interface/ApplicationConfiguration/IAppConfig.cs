namespace Interface.ApplicationConfiguration
{
    public interface IAppConfig
    {
        public string baseUrl { get; set; }
        public string idSrvBaseUrl { get; set; }
        public string anonymousUrl { get; set; }

        
    }
}
