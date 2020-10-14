using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace MoyaPrelest.Controllers
{
    public class DiagramController : Controller
    {
        public IActionResult BreedDiagramSelection()
        {
            return View(Startup.dm.BreedType());
        }

        [HttpPost]
        public RedirectToActionResult BreedDiagramSelection(string SelectedBreed)
        {
            return RedirectToAction("BreedDiagram", new { PassSelectedBreed = SelectedBreed });
        }

        public IActionResult BreedDiagram(string PassSelectedBreed)
        {

            //Encoding utf8 = Encoding.UTF8;
            //Encoding unicode = Encoding.Unicode;


            //byte[] unicodeBytes = unicode.GetBytes(PassSelectedBreed);
            //byte[] utf8Bytes = Encoding.Convert(unicode, utf8, unicodeBytes);


            //char[] utf8Chars = new char[utf8.GetCharCount(utf8Bytes, 0, utf8Bytes.Length)];
            //utf8.GetChars(utf8Bytes, 0, utf8Bytes.Length, utf8Chars, 0);
            //string utf8String = new string(utf8Chars);


            ViewBag.breed = PassSelectedBreed;
            return View();
        }

        public JsonResult AgeGroupQuantityDiagram(string input) => Json(Startup.dm.AgeGroupQuantity(input));
    }
}
