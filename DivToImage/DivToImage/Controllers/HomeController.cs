using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DivToImage.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public class CustomJsonResult
        {
            public bool Code { get; set; }
            public string Description { get; set; }
        }

        [HttpPost]
        public ActionResult CreateImage(string imagedata)
        {
            CustomJsonResult result = new CustomJsonResult();
            result.Code = false;
            result.Description = "";
            try
            {
                imagedata = imagedata.Split(',')[1];
                string fileName = "MyUniqueImageFileName_"+DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss")+".jpg";
                string fileNameWitPath = Path.Combine(Server.MapPath("~/FolderToSave"), fileName);

                using (FileStream fs = new FileStream(fileNameWitPath, FileMode.Create))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        byte[] data = Convert.FromBase64String(imagedata);
                        bw.Write(data);
                        bw.Close();
                    }
                    fs.Close();
                }
                result.Code = true;

            }
            catch (Exception ex)
            {
                result.Description = ex.ToString();
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}