using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.StaffControllers
{
    public class QRScannerController : Controller
    {
        // GET: QRScanner
        public ActionResult Index()
        {
            return View();
        }
    }
}