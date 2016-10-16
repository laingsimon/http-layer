using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace HttpLayer
{
    public class FormRequestData : PlainTextRequestData
    {
        private readonly string _contentType;
        private readonly NameValueCollection _form;

        public FormRequestData(
            NameValueCollection form,
            string contentType = "application/www-form-urlencoded")
            :base(_FormatForm(form), contentType: contentType)
        { }

        private static string _FormatForm(NameValueCollection form)
        {
            return string.Join(
                "&",
                form.AllKeys.Select(key => $"{HttpUtility.UrlEncode(key)}={HttpUtility.UrlEncode(form[key])}"));
        }
    }
}
