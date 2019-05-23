#region namespace
using System.Collections.Generic;
using EMAAR.Emaarcommunities.Feature.ContentComponents.Settings;
using Sitecore.Data.Items;
using Sitecore.Shell.Controls.RichTextEditor;
using Sitecore.Web;
using Telerik.Web.UI;
#endregion

namespace EMAAR.Emaarcommunities.Feature.ContentComponents.Pipelines
{
    /// <summary>
    /// Customization of RTE control by setting CSS file dynamically for multisite solution
    /// </summary>
    public class RteCustomization : EditorConfiguration
    {
        #region constructor
        public RteCustomization(Item profile) : base(profile)
        {
        }
        #endregion
        #region method
        /// <summary>
        /// Setting Style sheet file dynamically
        /// </summary>
        protected override void SetupStylesheets()
        {
            string id = WebUtil.GetQueryString("id");
            string query = "/*/content//*[@@id='" + id + "']/ancestor::*[@@templateid='{"+ SitecoreSettings.SiteRootTemplateID.ToUpper() + "}']";

            IList<Item> stylesheets = Sitecore.Context.ContentDatabase.SelectItems(query);

            foreach (Item item in stylesheets)
            {
                Sitecore.Data.Fields.LinkField lnkField = item.Fields[SitecoreSettings.SiteRootCSSFieldName];
                if (lnkField != null && lnkField.LinkType == "media")
                {
                    Editor.CssFiles.Add(Sitecore.StringUtil.EnsurePrefix('/', Sitecore.Resources.Media.MediaManager.GetMediaUrl(lnkField.TargetItem)));
                }
                Editor.CssClasses.Add("image-left-section", ".image-left-section");                
                
                base.SetupStylesheets();
            }
        }
        /// <summary>
        /// Setting break tag instead of p tag
        /// </summary>
        protected override void SetupEditor()
        {
            base.SetupEditor();
            Editor.NewLineMode = EditorNewLineModes.P;


        }
        /// <summary>
        /// Setting filter for RTE
        /// </summary>
        protected override void SetupFilters()
        {
            base.SetupFilters();
            Editor.ContentFilters = EditorFilters.None;
            this.Editor.DisableFilter(EditorFilters.ConvertToXhtml);
            Editor.DisableFilter(EditorFilters.DefaultFilters);
            Editor.EnableFilter(EditorFilters.FixEnclosingP);
           
        }
        #endregion
    }
}