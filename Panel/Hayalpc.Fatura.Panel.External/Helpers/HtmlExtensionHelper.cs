using Hayalpc.Fatura.Panel.External.Resources;
using Hayalpc.Library.Common.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Hayalpc.Fatura.Panel.External.Helpers
{
    public static class HtmlExtensionHelper
    {

        public static List<SelectListItem> GetClassSelectList(this IHtmlHelper helper, Type classType, string selectedValue = null)
        {
            if (helper is null)
            {
                throw new ArgumentNullException(nameof(helper));
            }
            var tr = RequestHelper.LocService as LocService;
            var list = classType.GetFields().
              Select(f => new SelectListItem
              {
                  Selected = selectedValue == f.GetValue(null).ToString(),
                  Value = f.GetValue(null).ToString(),
                  Text = f.GetCustomAttribute(typeof(DisplayAttribute)) != null ?
                  (
                  tr != null ?
                  tr.Get(((DisplayAttribute)f.GetCustomAttribute(typeof(DisplayAttribute))).Name)
                  : ((DisplayAttribute)f.GetCustomAttribute(typeof(DisplayAttribute))).Name
                  )
                  :
                  (
                  tr != null ?
                  tr.Get(f.GetValue(null).ToString())
                  :
                  f.GetValue(null).ToString()
                  )
              }).ToList();

            return list;
        }
    }
}
