using System;
using EMAAR.Emaarcommunities.Foundation.SitecoreExtensions;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Data.Items;

namespace EMAAR.Emaarcommunities.Foundation.Search.ComputedFields
{
    public class CustomSiteNameField : IComputedIndexField
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
                Item Site = item.Database.SelectSingleItem(item.Paths.FullPath + "/ancestor-or-self::*[@@templateid='"+CommonConstants.SiteRootTemplateID+"']");
                if (Site != null)
                {
                    return Site.Name.ToLower();
                }
                else
                {
                    return string.Empty;
                }
            }

            catch (Exception ex)
            {
                string itemId = string.Empty;
                if (item != null)
                {
                    itemId = item.ID.ToString();
                }

                Sitecore.Diagnostics.Log.Error(GetType().Name + " - Item ID: " + itemId, ex, this);

            }

            return null;
        }

        #endregion

    }
}