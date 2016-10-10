using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EShopping.Common
{
    public static class CustomerHtml
    {
    

           /// <summary>
        /// 描述：输出View HTML 字符串
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="viewName">视图文件名</param>
        /// <param name="masterName">母板页文件名</param>
        /// <returns></returns>
        public static string RenderViewToString(Controller controller, string viewName, string masterName)
        {
            IView view = ViewEngines.Engines.FindView(controller.ControllerContext, viewName, masterName).View;
            using (StringWriter writer = new StringWriter())
            {
                ViewContext viewContext = new ViewContext(controller.ControllerContext, view, controller.ViewData, controller.TempData, writer);
                viewContext.View.Render(viewContext, writer);
                return writer.ToString();
            }
        }

 


        /// <summary>
        /// 描述：输出PartialView HTML 字符串
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="partialViewName">部分视图文件名</param>
        /// <returns></returns>
        public static string RenderPartialViewToString(Controller controller, string partialViewName)
        {
            IView view = ViewEngines.Engines.FindPartialView(controller.ControllerContext, partialViewName).View;
            using (StringWriter writer = new StringWriter())
            {
                ViewContext viewContext = new ViewContext(controller.ControllerContext, view, controller.ViewData, controller.TempData, writer);
                viewContext.View.Render(viewContext, writer);
                return writer.ToString();
            }
        }
    }
}
