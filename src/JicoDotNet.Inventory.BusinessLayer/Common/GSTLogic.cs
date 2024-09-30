using JicoDotNet.Inventory.Core.Enumeration;
using System;

namespace JicoDotNet.Inventory.BusinessLayer.Common
{
    public class GSTLogic
    {
        /// <summary>
        /// <para>
        ///     Bill - Company will pay GST to Vendor if Vendor is GST registred.
        /// </para> 
        /// <para>
        ///     Invoice - Customer will pay GST to Company if Company is GST registred
        /// </para> 
        /// </summary>
        /// <param name="GstPayerStateCode">
        /// <para>
        ///     Bill : Company IsGSTRegistred -> Then GstStateCode Else StateCode, 
        /// </para> 
        /// <para>
        ///     Invoice : Customer IsGSTRegistred -> Then GstStateCode Else StateCode
        /// </para> 
        /// </param>
        /// <param name="GstReceiverStateCode">
        /// <para>
        ///     Bill : Vendor IsGSTRegistred -> Then GstStateCode Else StateCode, 
        /// </para> 
        /// <para>
        ///     Invoice : Company IsGSTRegistred -> Then GstStateCode Else StateCode
        /// </para> 
        /// </param>
        /// <returns>EGSTType object. if GST unregistred then EGSTType.None</returns>
        public static EGSTType GetType(object GstPayerStateCode, object GstReceiverStateCode)
        {
            EGSTType eGSTType = EGSTType.None;
            try
            {
                int GstF = Convert.ToInt32(GstPayerStateCode);
                int GstT = Convert.ToInt32(GstReceiverStateCode);

                if (GstF == GstT)
                {
                    eGSTType = EGSTType.CGSTSGST;
                }
                else
                {
                    eGSTType = EGSTType.IGST;
                }
                return eGSTType;
            }
            catch
            {
                return eGSTType;
            }
        }
    }
}
