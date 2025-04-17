using JicoDotNet.Inventory.UI.Models;
using System;
using System.Web.Mvc;
using JicoDotNet.Inventory.Controllers;

namespace JicoDotNet.Inventory.UI.Controllers
{
    public class PartialController : BaseController
    {
        public PartialViewResult NavBarCompany()
        {
            SessionCredentialCompanyModels sessionCredentialCompanyModels = new SessionCredentialCompanyModels()
            {
                _sessionCredential = SessionPerson
            };
            return PartialView("_PartialNavbarCompany", sessionCredentialCompanyModels);
        }

        public PartialViewResult AsideMain()
        {
            try
            {
                return this.PartialView("_PartialAsideMain");
            }
            catch (Exception ex)
            {
                return ErrorLoggingToPartial(ex);
            }
        }

        public PartialViewResult AsideReport()
        {
            try
            {
                return PartialView("_PartialAsideReport");
            }
            catch (Exception ex)
            {
                return ErrorLoggingToPartial(ex);
            }
        }

        public PartialViewResult AsideStore()
        {
            try
            {
                return PartialView("_PartialAsideStore");
            }
            catch (Exception ex)
            {
                return ErrorLoggingToPartial(ex);
            }
        }

        //public PartialViewResult BrandLogo()
        //{
        //    return PartialView("_PartialBrandLogo");
        //}

        //public PartialViewResult SidebarUser()
        //{
        //    return PartialView("_PartialSidebarUser", SessionCompany);
        //}

        //public PartialViewResult Footer()
        //{
        //    return PartialView("_PartialFooter");
        //}
        //public PartialViewResult SidebarRight()
        //{
        //    SessionCredentialCompanyModels sessionCredentialCompanyModels = new SessionCredentialCompanyModels()
        //    {
        //        _sessionCredential = SessionPerson,
        //        _company = SessionCompany
        //    };
        //    return PartialView("_PartialSidebar", sessionCredentialCompanyModels);
        //}
    }
}