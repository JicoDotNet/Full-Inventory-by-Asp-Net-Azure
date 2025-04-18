namespace System.Web.Mvc
{
    using System.Text;

    public static class MvcExtension
    {
        public static string UrlIdEncrypt(this HtmlHelper htmlHelper, object id, bool isEscape = true)
        {
            if (id != null)
            {
                string _id = id.ToString();
                if (!string.IsNullOrEmpty(_id))
                {
                    var IdBytes = Encoding.UTF8.GetBytes(_id);
                    string rd = string.Empty;
                    if (isEscape)
                        rd = Uri.EscapeDataString(Convert.ToBase64String(IdBytes));
                    else
                        rd = Convert.ToBase64String(IdBytes);
                    return rd;
                }
            }
            return null;
        }

        //public static string Id(this HtmlHelper htmlHelper)
        //{
        //    var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;
        //    if (routeValues.ContainsKey("id"))
        //        return (string)routeValues["id"];
        //    else if (HttpContext.Current.Request.QueryString.AllKeys.Contains("id"))
        //        return HttpContext.Current.Request.QueryString["id"];
        //    return string.Empty;
        //}
        //public static string Controller(this HtmlHelper htmlHelper)
        //{
        //    var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;
        //    if (routeValues.ContainsKey("controller"))
        //        return (string)routeValues["controller"];
        //    return string.Empty;
        //}
        //public static string Action(this HtmlHelper htmlHelper)
        //{
        //    var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;
        //    if (routeValues.ContainsKey("action"))
        //        return (string)routeValues["action"];
        //    return string.Empty;
        //}

        public static string Area(this HtmlHelper htmlHelper)
        {
            var dataTokens = HttpContext.Current.Request.RequestContext.RouteData.DataTokens;

            if (dataTokens.ContainsKey("Area"))
                return (string)dataTokens["Area"];

            return string.Empty;
        }
    }
}