using RestSharp;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace JicoDotNet.Inventory.UIControllers
{
    public class PDFController : Controller
    {
        private const string BaseUrlForPdf = "https://BBPdfGenerator.azurewebsites.net/";

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Download(PdfParam param)
        {
            var client = new RestClient(BaseUrlForPdf);
            var request = new RestRequest("api/PDF/Generator", Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddHeader("EnvironmentType", "Prod");
            request.AddJsonBody(param);
            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=" + param.fileName + ".pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(response.RawBytes);
                Response.End();
            }
            return RedirectToAction("Error", "Index");
        }
    }

    /// <summary>
    /// PDF Param 
    /// </summary>
    public class PdfParam
    {
        public string htmlBody { get; set; }
        public string fileName { get; set; }
    }
}