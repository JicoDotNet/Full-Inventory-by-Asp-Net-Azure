using System.Web.Mvc;

namespace JicoDotNet.Inventory.UIControllers
{
    public class PDFController : Controller
    {
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Download(PdfParam param)
        {
            return RedirectToAction("Error", "Index", new { ex = param.FileName });
        }
    }

    /// <summary>
    /// PDF Param 
    /// </summary>
    public class PdfParam
    {
        public string HtmlBody { get; set; }
        public string FileName { get; set; }
    }
}