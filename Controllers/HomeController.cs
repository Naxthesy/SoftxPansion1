using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SoftxPansion1.Models;
using SoftxPansion1.Parse;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SoftxPansion1.Controllers
{
    public class HomeController : Controller
    {
        public IWebHostEnvironment appEnvironment;
        public HomeController(IWebHostEnvironment environment)
        {
            appEnvironment = environment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendFile(IFormFile file)
        {
            List<Unit> units = new List<Unit>();
            if (file != null)
            {
                // путь к папке Files
                string path = "/XmlFiles/" + file.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                units = XmlParse.Parse(appEnvironment.WebRootPath + path);
                //TODO: Удаление файла с сервера
            }
            List<string> Titles = new List<string>();
            foreach(var t in units)
            {
                Titles.Add(t.Title);
            }
            ViewBag.Units = units;
            ViewBag.Titles = units.Select(y => new SelectListItem
            {
                Value = y.Id.ToString(), Text = y.Title
            }).ToList();
            return View();
        }
    }
}