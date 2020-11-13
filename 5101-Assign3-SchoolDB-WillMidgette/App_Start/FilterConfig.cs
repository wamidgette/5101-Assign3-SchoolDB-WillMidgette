using System.Web;
using System.Web.Mvc;

namespace _5101_Assign3_SchoolDB_WillMidgette
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
