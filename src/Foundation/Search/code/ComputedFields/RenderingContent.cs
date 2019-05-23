using EMAAR.Emaarcommunities.Foundation.Search.Settings;
using Sitecore;
using Sitecore.ContentSearch;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Layouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace EMAAR.Emaarcommunities.Foundation.Search.ComputedFields
{
    /// <summary>
    /// RenderingsContent class definition
    /// </summary>
    public class RenderingsContent : Sitecore.ContentSearch.ComputedFields.IComputedIndexField
    {
        public string FieldName { get; set; }

        public string ReturnType { get; set; }

        /// <summary>
        /// Method to compute RenderingsContent
        /// </summary>
        /// <param name="indexable">indexable object.</param>
        /// <returns>object.</returns>    
        public object ComputeFieldValue(Sitecore.ContentSearch.IIndexable indexable)
        {
            string renderingsContent = string.Empty;
            SitecoreIndexableItem sitecoreIndexable = null;
            try
            {
                sitecoreIndexable = indexable as SitecoreIndexableItem;

                if (sitecoreIndexable == null)
                        return null;

                TemplateItem[] baseTemplates = sitecoreIndexable.Item.Template.BaseTemplates;
                if (baseTemplates.Length > 0)
                {
                    string basePageTemplateId = SitecoreSettings.BasePageTemplateId.ToUpper().Replace("{", string.Empty).Replace("}", string.Empty);
                    foreach (TemplateItem baseTemplate in baseTemplates)
                    {
                        if (baseTemplate.ID.Guid.ToString().ToUpper().Equals(basePageTemplateId))
                        {
                            // find renderings with datasources set
                            var customDataSources = ExtractRenderingDataSourceItems(sitecoreIndexable.Item);

                            // extract text from data sources
                            var contentToAdd = customDataSources.SelectMany(GetItemContent).ToList();

                            if (contentToAdd.Count == 0) return renderingsContent;

                            renderingsContent = string.Join(" ", contentToAdd);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                string itemId = string.Empty;
                if (sitecoreIndexable.Item != null)
                    itemId = sitecoreIndexable.Item.ID.ToString();
                Log.Error(this.GetType().Name + " - Item ID: " + itemId, ex, this);
            }
            return renderingsContent;

        }
        /// <summary>
		/// Finds all renderings on an item's layout details with valid custom data sources set and returns the data source items.
		/// </summary>
		protected virtual IEnumerable<Item> ExtractRenderingDataSourceItems(Item baseItem)
        {
            string currentLayoutXml = LayoutField.GetFieldValue(baseItem.Fields[FieldIDs.LayoutField]);
            if (string.IsNullOrEmpty(currentLayoutXml)) yield break;

            LayoutDefinition layout = LayoutDefinition.Parse(currentLayoutXml);

            // loop over devices in the rendering
            for (int deviceIndex = layout.Devices.Count - 1; deviceIndex >= 0; deviceIndex--)
            {
                var device = layout.Devices[deviceIndex] as DeviceDefinition;

                if (device == null) continue;

                // loop over renderings within the device
                for (int renderingIndex = device.Renderings.Count - 1; renderingIndex >= 0; renderingIndex--)
                {
                    var rendering = device.Renderings[renderingIndex] as RenderingDefinition;

                    if (rendering == null) continue;

                    // Only renderings on specific placeholders will be considered
                    if (!SitecoreSettings.PlaceholdersToSearch.ToLower().Contains(rendering.Placeholder.ToLower()))
                        continue;

                    // if the rendering has a custom data source, we resolve the data source item and place its text fields into the content to add
                    if (!string.IsNullOrWhiteSpace(rendering.Datasource))
                    {
                        var dataSource = baseItem.Database.GetItem(rendering.Datasource, baseItem.Language);


                        if (dataSource != null && dataSource != baseItem)
                        {
                            yield return dataSource;
                        }
                    }
                }
            }

        }

        /// <summary>
        /// Extracts textual content from an item's fields
        /// </summary>
        protected virtual IEnumerable<string> GetItemContent(Item dataSource)
        {
            foreach (Field field in dataSource.Fields)
            {
                // this check is what Sitecore uses to determine if a field belongs in _content (see LuceneDocumentBuilder.AddField())
                if (IndexOperationsHelper.IsTextField(new SitecoreItemDataField(field)))
                {

                    string fieldValue = StripHtml(field.Value ?? string.Empty);

                    if (!string.IsNullOrWhiteSpace(fieldValue)) yield return fieldValue;
                }
                else if ((field.Type == "Multilist" || field.Type == "Treelist") && !string.IsNullOrWhiteSpace(field.Value))
                {
                    string[] itemGuids = field.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    if (itemGuids.Any())
                    {
                        foreach (var itemGuid in itemGuids)
                        {
                            var innerItem = dataSource.Database.GetItem(itemGuid, dataSource.Language);
                            foreach (Field innerItemfeild in innerItem.Fields)
                            {
                                if (IndexOperationsHelper.IsTextField(new SitecoreItemDataField(innerItemfeild)))
                                {

                                    string fieldValue = StripHtml(innerItemfeild.Value ?? string.Empty);

                                    if (!string.IsNullOrWhiteSpace(fieldValue)) yield return fieldValue;
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Strips html tags
        /// </summary>
        protected string StripHtml(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;
            string noHTML = Regex.Replace(input, @"<[^>]+>|&nbsp;", "").Trim();
            return Regex.Replace(noHTML, @"\s{2,}", " ");
        }

    }
}