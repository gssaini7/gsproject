using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace ussmain.Models.HtmlHelpers
{
    public static class HtmlHelperExtensions
    {
        public static string ActivePage(this HtmlHelper helper, string controller, string action, string id)
        {
            string classValue = "";

            string currentController = helper.ViewContext.Controller.ValueProvider.GetValue("controller").RawValue.ToString();
            string currentAction = helper.ViewContext.Controller.ValueProvider.GetValue("action").RawValue.ToString();
            string currentID = helper.ViewContext.Controller.ValueProvider.GetValue("id").RawValue.ToString();


            if (currentController == controller && currentAction == action && currentID == id)
            {
                classValue = "active";
            }

            return classValue;
        }

        //public static string ActivePageLink(this HtmlHelper helper, string link)
        //{
        //    string classValue = "";

        //    string currentController = helper.ViewContext.Controller.ValueProvider.GetValue("controller").RawValue.ToString();
        //    string currentAction = helper.ViewContext.Controller.ValueProvider.GetValue("action").RawValue.ToString();

        //    if (currentController == controller && currentAction == action)
        //    {
        //        classValue = "active";
        //    }

        //    return classValue;
        //}
    }
}