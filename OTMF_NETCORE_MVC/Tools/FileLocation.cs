namespace OTMF_NETCORE_MVC.Tools
{
    public interface IFileLocation
    {
        public string GetEtiquetasCajasImagePath(string imagePath);
    }
    public class FileLocation : IFileLocation
    {
        private IWebHostEnvironment webHostEnvironment;
        public FileLocation(IWebHostEnvironment Environment)
        {
            webHostEnvironment = Environment;
        }
        public string GetEtiquetasCajasImagePath(string FileName)
        {
            return Path.Combine(webHostEnvironment.WebRootPath + "\\Uploads\\Etiquetas\\Cajas\\", FileName);
        }
    }
}
