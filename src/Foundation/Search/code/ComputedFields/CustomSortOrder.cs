using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Resources.Media;
using System;

namespace EMAAR.Emaarcommunities.Foundation.Search.ComputedFields
{
    public class CustomSortOrder : IComputedIndexField
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
        /// Computed field for image url
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

                 var sortOrderField = item.Fields["__Sortorder"];
                if (sortOrderField != null && !String.IsNullOrEmpty(sortOrderField.Value))
                {
                    return Convert.ToInt64(sortOrderField.Value);
                }
                else
                {
                    return 0;
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