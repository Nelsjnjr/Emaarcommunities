using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Shell.Applications.ContentEditor;

namespace EMAAR.Emaarcommunities.Foundation.SitecoreExtensions.Controls
{
    /// <summary>
    /// This class is used to create custom treelist for having inclusion and exclusion items with multisite and query feature 
    /// which is not supported by out of box
    /// </summary>
    public class QueryableTreelist : TreeList
    {
        private const string QueryKey = "query:";

        public new string Source
        {
            get { return base.Source; }
            set
            {
                if (!value.Contains(QueryKey))
                {
                    base.Source = value;
                }
                else
                {
                    string dataSource = StringUtil.ExtractParameter("DataSource", value).Trim();
                    if (string.IsNullOrEmpty(dataSource))
                    {
                        base.Source = value;
                        return;
                    }

                    string excludeTemplatesForSelection = StringUtil.ExtractParameter("ExcludeTemplatesForSelection", value).Trim();
                    string includeTemplatesForSelection = StringUtil.ExtractParameter("IncludeTemplatesForSelection", value).Trim();
                    string includeTemplatesForDisplay = StringUtil.ExtractParameter("IncludeTemplatesForDisplay", value).Trim();
                    string excludeTemplatesForDisplay = StringUtil.ExtractParameter("ExcludeTemplatesForDisplay", value).Trim();
                    string excludeItemsForDisplay = StringUtil.ExtractParameter("ExcludeItemsForDisplay", value).Trim();
                    string includeItemsForDisplay = StringUtil.ExtractParameter("IncludeItemsForDisplay", value).Trim();
                    string allowMultipleSelection = StringUtil.ExtractParameter("AllowMultipleSelection", value).Trim();
                    string databaseName = StringUtil.ExtractParameter("DatabaseName", value).Trim().ToLowerInvariant();

                    Item contextItem = Sitecore.Context.ContentDatabase.Items[ItemID];

                    if (contextItem == null)
                    {
                        base.Source = value;
                        return;
                    }

                    Item dataSourceItem = contextItem.Axes.SelectSingleItem(ExtractQuery(dataSource));

                    if (dataSourceItem == null)
                    {
                        base.Source = value;
                        return;
                    }

                    dataSource = dataSourceItem.Paths.FullPath;

                    string source = string.Empty;

                    if (!string.IsNullOrEmpty(dataSource))
                        source += $"DataSource={dataSource}&";

                    if (!string.IsNullOrEmpty(excludeTemplatesForSelection))
                        source += $"ExcludeTemplatesForSelection={excludeTemplatesForSelection}&";

                    if (!string.IsNullOrEmpty(includeTemplatesForSelection))
                        source += $"IncludeTemplatesForSelection={includeTemplatesForSelection}&";

                    if (!string.IsNullOrEmpty(includeTemplatesForDisplay))
                        source += $"IncludeTemplatesForDisplay={includeTemplatesForDisplay}&";

                    if (!string.IsNullOrEmpty(excludeTemplatesForDisplay))
                        source += $"ExcludeTemplatesForDisplay={excludeTemplatesForDisplay}&";

                    if (!string.IsNullOrEmpty(excludeItemsForDisplay))
                        source += $"ExcludeItemsForDisplay={excludeItemsForDisplay}&";

                    if (!string.IsNullOrEmpty(includeItemsForDisplay))
                        source += $"IncludeItemsForDisplay={includeItemsForDisplay}&";

                    if (!string.IsNullOrEmpty(allowMultipleSelection))
                        source += $"AllowMultipleSelection={allowMultipleSelection}&";

                    if (!string.IsNullOrEmpty(databaseName))
                        source += $"DatabaseName={databaseName}&";

                    base.Source = source.TrimEnd('&');
                }
            }
        }

        private string ExtractQuery(string value)
        {
            return value.Substring(QueryKey.Length);
        }
    }
}