using DataAccess.AzureStorage;
using DataAccess.Sql;
using JicoDotNet.Inventory.BusinessLayer.Common;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using JicoDotNet.Inventory.BusinessLayer.DTO.SP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    //public class SMSManageLogic : ConnectionString
    //{
        //public SMSManageLogic(sCommonDto CommonObj) : base(CommonObj) { }

        //public async Task SendAsync(SMSSendTrack sMSSend, string CompanyName, string Identity)
        //{
        //    #pragma warning disable CS4014 
        //    Task.Run(() =>
        //    {
        //        Send(sMSSend, CompanyName, Identity);
        //    });
        //    #pragma warning restore CS4014
        //}

        //public void Send(SMSSendTrack sMSSend, string CompanyName, string Identity)
        //{
        //    sMSSend.SendTime = GenericLogic.IstNow;
        //    sMSSend.ComponentIdentity = Identity;
        //    sMSSend.UrlLink = "http://d.itemblob.in/i/" + GenericLogic.EncodeHex(Convert.ToInt64(Identity));
        //    sMSSend.SmsFor = ESMSFor.INV.ToString();
        //    sMSSend.SmsContent = "Dear Customer " + Environment.NewLine
        //                    + "Thank you for shopping with " + CompanyName.Truncate(15)
        //                + ". Please click the link to view e-invoice "
        //                + sMSSend.UrlLink
        //                + Environment.NewLine
        //                + "Service provided by ITEM Blob";

        //    SMSBankLogic bankLogic = new SMSBankLogic(CommonObj);
        //    if (bankLogic.Deduction())
        //    {
        //        SmSLogic.SMSInvoiceLink(sMSSend.MobileNo, CompanyName.Truncate(15), sMSSend.UrlLink);
        //        _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
        //        nameValuePairs nvp = new nameValuePairs()
        //        {
                     
                     
        //            new nameValuePair("@UserId", CommonObj.UserId),
        //            new nameValuePair("@UserName", CommonObj.UserName),

        //            new nameValuePair("@ComponentIdentity", sMSSend.ComponentIdentity),
        //            new nameValuePair("@UrlLink", sMSSend.UrlLink),
        //            new nameValuePair("@SmsContent", sMSSend.SmsContent),
        //            new nameValuePair("@MobileNo", sMSSend.MobileNo),
        //            new nameValuePair("@IsMobileNoChanged", sMSSend.IsMobileNoChanged),
        //            new nameValuePair("@IsResend", sMSSend.IsResend),
        //            new nameValuePair("@SmsFor", sMSSend.SmsFor),

        //            new nameValuePair("@RequestId", CommonObj.RequestId),
        //            new nameValuePair("@QueryType", "INSERT")
        //        };
        //        _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetSmsTrack]", nvp, "@OutParam").ToString();
        //    }
        //}

        //public SMSSendTrack GetLinkWise(string ComponentIdentity, ESMSFor sMSFor)
        //{
        //    _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
        //    nameValuePairs nvp = new nameValuePairs()
        //    {
        //        new nameValuePair("@ComponentIdentity", ComponentIdentity),
        //        new nameValuePair("@SmsFor", sMSFor.ToString()),
        //        new nameValuePair("@QueryType", "LINKWISE")
        //    };
        //    return _sqlDBAccess.GetFirstOrDefaultRow("[dbo].[spGetSmsTrack]", nvp).FirstOrDefault<SMSSendTrack>();
        //}

        //public SMSSendTrack Get(string ComponentIdentity, ESMSFor sMSFor)
        //{
        //    _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
        //    nameValuePairs nvp = new nameValuePairs()
        //    {
        //        new nameValuePair("@ComponentIdentity", ComponentIdentity),
        //        new nameValuePair("@SmsFor", sMSFor.ToString()),
                 
                 
        //        new nameValuePair("@QueryType", "SINGLE")
        //    };
        //    return _sqlDBAccess.GetFirstOrDefaultRow("[dbo].[spGetSmsTrack]", nvp).FirstOrDefault<SMSSendTrack>();
        //}

        //public List<SMSSendTrack> Gets()
        //{
        //    _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
        //    nameValuePairs nvp = new nameValuePairs()
        //    {                
                 
        //        new nameValuePair("@QueryType", "ALL")
        //    };
        //    return _sqlDBAccess.GetData("[dbo].[spGetSmsTrack]", nvp).ToList<SMSSendTrack>();
        //}
    //}    
}
