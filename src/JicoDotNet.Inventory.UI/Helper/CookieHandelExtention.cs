using Newtonsoft.Json;
using System.Text;

namespace System.Web.Mvc
{
    public static class CookieHandelExtention
    {
        public static void SetCookie<T>(this HttpContextBase context, string key, T value)
        {
            if (value != null)
            {
                HttpCookie cookie = new HttpCookie(key,
                Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value))))
                {
                    Expires = DateTime.UtcNow.AddHours(29.5).AddSeconds(-1),
                };
                context.Response.Cookies.Add(cookie);
            }
        }

        public static T GetCookie<T>(this HttpContextBase context, string key)
        {
            HttpCookie cookie = context.Request.Cookies[key];
            if (cookie != null)
            {
                try
                {
                    cookie.Expires = DateTime.UtcNow.AddHours(29.5).AddSeconds(-1);
                    context.Response.Cookies.Add(cookie);

                    T cookieValue = default;
                    cookieValue = JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(Convert.FromBase64String(cookie.Value ?? "")));
                    if (cookieValue != null)
                        return cookieValue;
                }
                catch
                {
                    return default;
                }
            }
            return default;
        }

        public static void DeleteCookie(this HttpContextBase context, string key)
        {
            context.Response.Cookies[key].Value = string.Empty;
            context.Response.Cookies[key].Expires = DateTime.UtcNow.AddMonths(-20);
        }
    }
}