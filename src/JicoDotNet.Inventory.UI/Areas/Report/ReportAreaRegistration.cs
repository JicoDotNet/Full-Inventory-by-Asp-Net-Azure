using System.Web.Mvc;

namespace JicoDotNet.Inventory.UI.Areas.Report
{
    public class ReportAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Report";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Report_default",
                this.AreaName + "/{controller}/{action}/{id}/{id2}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional, id2 = UrlParameter.Optional },
                new[] { "JicoDotNet.Inventory.UI.Areas.Report.Controllers" }
            );
        }
    }
}