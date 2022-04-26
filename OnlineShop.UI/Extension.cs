using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace OnlineShop.UI
{
    public static class Extension
    {
        public static IWebHostEnvironment WebEnv()
        {
            var _accessor = new HttpContextAccessor();
            return _accessor.HttpContext.RequestServices.GetRequiredService<IWebHostEnvironment>();
        }
        public static string GetPath(string name)
        {
            string path = WebEnv().WebRootPath + "/images/" + name + ".png";
            if (System.IO.File.Exists(path))
            {
                return "/images/" + name + ".png";
            }
            return "/images/placeholder.png";
        }

    }
}
