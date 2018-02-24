using System.Web.Mvc;

namespace ThaiSonBacDMS.Areas.PhanPhoi
{
    public class PhanPhoiAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PhanPhoi";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PhanPhoi_default",
                "PhanPhoi/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}