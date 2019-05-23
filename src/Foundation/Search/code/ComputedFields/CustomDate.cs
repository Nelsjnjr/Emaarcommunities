using EMAAR.Emaarcommunities.Foundation.SitecoreExtensions;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Resources.Media;
using System;
using System.Linq;
using System.Collections;
using EMAAR.Emaarcommunities.Foundation.Search.Helpers;
using EMAAR.Emaarcommunities.Foundation.SitecoreExtensions.Settings;

namespace EMAAR.Emaarcommunities.Foundation.Search.ComputedFields
{
    public class CustomDate : IComputedIndexField
    {

        #region Properties
        public string FieldName
        {
            get; set;
        }

        public string ReturnType
        {
            get; set;
        }
        #endregion

        #region Public Method

        /// <summary>
        /// Computed field for date
        /// </summary>
        /// <param name="indexable">IIndexable</param>
        /// <returns>object.</returns>    
        public object ComputeFieldValue(IIndexable indexable)
        {
            Item item = null;

            try
            {
                item = indexable as SitecoreIndexableItem;
                if (item == null)
                {
                    return null;
                }

                string formattedTemplateId = SearchHelper.FormatGuid(item.TemplateID.ToString());

                if (item.Fields["Date"] != null && !string.IsNullOrEmpty(item.Fields["Date"].Value))
                {                   
                   
                    DateField dt = item.Fields["Date"];

                    if (formattedTemplateId.Equals(SearchHelper.FormatGuid(CommonConstants.NewsTemplateID)))
                    {
                        return Sitecore.DateUtil.FormatDateTime(dt.DateTime, SitecoreSettings.DateFormat, item.Language.CultureInfo);

                    }
                    else if (formattedTemplateId.Equals(SearchHelper.FormatGuid(CommonConstants.EventsTemplateID)))
                    {
                        return dt.DateTime.Day.ToString();
                    }



                }
            }

            catch (Exception ex)
            {
                string itemId = string.Empty;
                if (item != null)
                    itemId = item.ID.ToString();
                Sitecore.Diagnostics.Log.Error(this.GetType().Name + " - Item ID: " + itemId, ex,this);
             
            }

            return null;
        }

        #endregion

      
    }
}