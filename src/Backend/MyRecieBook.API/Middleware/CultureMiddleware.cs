using System.Globalization;

namespace MyRecieBook.API.Middleware
{
    public class CultureMiddleware(RequestDelegate next)
    {

        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext context)
        {
            var suportedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures);

            var requestedCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();
            
            var cultureInfo =  new CultureInfo("en");

            if (!string.IsNullOrWhiteSpace(requestedCulture) && suportedLanguages.Any(e => e.Name.Equals(requestedCulture)))
            {
                cultureInfo = new CultureInfo(requestedCulture);
            }

            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;

            await _next(context);
        }
    }
}
