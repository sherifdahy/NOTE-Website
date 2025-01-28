namespace Interface.Services
{
    public static class uploadFile
    {
        
        public static void uploadImage(IWebHostEnvironment hostEnvironment,IFormFile Image)
        {
            string folder_path = Path.Combine(hostEnvironment.WebRootPath, "imgs");
            string uniqueImageName = Guid.NewGuid().ToString() + "_" + Image.FileName;
            string filePath = Path.Combine(folder_path, uniqueImageName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                Image.CopyTo(fileStream);
                fileStream.Close();

            }
        }



        public static void uploadPDF(IWebHostEnvironment hostEnvironment, IFormFile PDF)
        {
            string folder_path = Path.Combine(hostEnvironment.WebRootPath, "imgs");
            string uniqueImageName = Guid.NewGuid().ToString() + "_" + PDF.FileName;
            string filePath = Path.Combine(folder_path, uniqueImageName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                PDF.CopyTo(fileStream);
                fileStream.Close();

            }
        }
    }
}
