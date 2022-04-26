using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Threading.Tasks;

namespace OnlineShop.UI.Pages.Admin
{
    public class IndexModel : PageModel
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        private readonly IWebHostEnvironment _env;

        public IndexModel(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task OnPost(IFormCollection form, IFormFile photo, string fileName)
        {
            if (photo == null || form == null || fileName == null)
            {
                return;
            }
            var newPath = Path.Combine(_env.WebRootPath, "images\\");

            var path = Path.Combine(_env.WebRootPath, "images", photo.FileName);

            var stream = new FileStream(path, FileMode.Create);
            await photo.CopyToAsync(stream);
            stream.Close();
            FileName = fileName;
            FilePath = path;
            SetFileName(newPath + Request.Form.Files[0].FileName, newPath + fileName + ".png");
        }
        public void SetFileName(string oldName, string newName)
        {
            if (System.IO.File.Exists(newName))
            {
                System.IO.File.Delete(newName);
            }
            System.IO.File.Move(oldName, newName);
        }


    }

}
