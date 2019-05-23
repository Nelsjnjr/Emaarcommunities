using System;
using System.Collections.Generic;
using System.Linq;
using EMAAR.Emaarcommunities.Foundation.SitecoreExtensions.Settings;
using Sitecore.Configuration;
using Sitecore.Data.Items;
using Sitecore.Web;

namespace EMAAR.Emaarcommunities.Foundation.SitecoreExtensions
{
    public static class CommonUtility
    {
        public static string RemoveBreakTags(this string value)
        {
            return value.Replace("<br><br>", "<p></p>")
                .Replace("<br /><br />", "<p></p>")
                .Replace("<br>", "<p></p>")
                .Replace("<br />", "<p></p>")
                .Replace("{Add Here}", string.Empty);
        }
        /// <summary>
         ///  first Check Navigation title field Available if not Fall to Title Field again if not Fallback to Item name
         /// </summary>
         /// <param name="item"></param>
         /// <param name="defaultTitle">Navigation Item Name</param>
         /// <returns></returns>
        public static string GetTitle(Item Referreditem, Item NavigationItem)
        {
            //First look to Title in Navigation/Subnavigation Template
            if (!String.IsNullOrEmpty(NavigationItem.Fields[CommonConstants.NavigationTitle].Value))
            {
                return NavigationItem.Fields[CommonConstants.NavigationTitle].Value;
            }   
            //Then go the refered item and get its Navigation Title
            else if (Referreditem.Fields[CommonConstants.NavigationTitle] != null && !String.IsNullOrEmpty(Referreditem.Fields[CommonConstants.NavigationTitle].Value))
            {
                return Referreditem.Fields[CommonConstants.NavigationTitle].Value;
            }
            //Take Refered item Title
            else if (Referreditem.Fields[CommonConstants.Title] != null && !String.IsNullOrEmpty(Referreditem.Fields[CommonConstants.Title].Value))
            {
                return Referreditem.Fields[CommonConstants.Title].Value;
            }
            //Take Navigation/Subnavigation Item Name(This will be used incase of external link)
            else
            {
                return NavigationItem.Name;
            }
            
        }

    }
}