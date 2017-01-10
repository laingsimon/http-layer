using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace HttpLayer.Request
{
    public class FormRequestData : PlainTextRequestData
    {
        public FormRequestData(
            NameValueCollection form,
            string contentType = "application/x-www-form-urlencoded")
            :base(_FormatForm(form), contentType: contentType)
        { }

        private static string _FormatForm(NameValueCollection form)
        {
            if (form == null)
                throw new ArgumentNullException(nameof(form));

            return string.Join(
                "&",
                form.AllKeys.Select(key => $"{HttpUtility.UrlEncode(key)}={HttpUtility.UrlEncode(form[key])}"));
        }
    }
}
