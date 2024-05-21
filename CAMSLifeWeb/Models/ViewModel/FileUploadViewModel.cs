namespace CaliphWeb.Models.ViewModel
{
    public class FileUploadViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string SavePath { get; set; }
        public string FullPath => SavePath + Name;


    }
}