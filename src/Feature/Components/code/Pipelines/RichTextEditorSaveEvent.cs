#region namespace
using System;
using System.Linq;
using EMAAR.Emaarcommunities.Feature.ContentComponents.Settings;
using EMAAR.Emaarcommunities.Foundation.SitecoreExtensions;
using HtmlAgilityPack;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Events;
using Sitecore.SecurityModel;
#endregion
namespace EMAAR.Emaarcommunities.Feature.ContentComponents.Pipelines
{
    /// <summary>
    /// This class is used to Format the Richtext html content on item saving event, that useful for our snippets
    /// </summary>
    public class RichTextEditorSaveEvent
    {
        #region property
        public string Database
        {
            get;
            set;
        }
        #endregion
        #region method
        /// <summary>
        /// This method is used when RTE is save to do formatting HTML
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnItemSaving(object sender, EventArgs args)
        {

            Item item = Event.ExtractParameter(args, 0) as Item;
            if (item == null)
            {
                return;
            }
            if (item.TemplateID != ID.Parse(SitecoreSettings.GenericTemplateID))
            {
                return;
            }
            if ((item.Database != null && string.Compare(item.Database.Name, Database) != 0))
            {
                return;
            }
            try
            {
                //Target only on richtext field
                foreach (Field field in item.Fields)
                {
                    //if (field.TypeKey.Equals("tree list", StringComparison.InvariantCultureIgnoreCase))
                    ////{
                    ////    MultilistField Tags = Sitecore.Context.Item.Fields[field.Name];

                    ////   bool isrestrictedTemplate= Tags.GetItems().Any(p => p.TemplateName == "");
                    ////    if(isrestrictedTemplate)
                    ////    {
                    ////        args.
                    ////    }
                    ////}

                    if (!field.TypeKey.Equals("rich text", StringComparison.InvariantCultureIgnoreCase))
                    {
                        continue;
                    }

                    string content = field.Value;

                    if (!string.IsNullOrEmpty(content))
                    {
                        content = content.Trim();

                        try
                        {
                            HtmlDocument htmlDocument = new HtmlDocument();
                            htmlDocument.LoadHtml(content);
                            RemoveEmptyDivTags(htmlDocument);
                            FormatProperHTML(htmlDocument);
                            RemoveEmptyPTags(htmlDocument);
                            content = htmlDocument.DocumentNode.InnerHtml;

                        }
                        catch (Exception ex)
                        {
                            Sitecore.Diagnostics.Log.Error("Error occured in RTE Saving :RichTextEditorSaveEvent.cs :Error  " + ex, this);
                        }

                        using (new SecurityDisabler())
                        {
                            item.Editing.BeginEdit();
                            field.Value = content;
                            item.Editing.EndEdit();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error("Error occured in RTE Saving :RichTextEditorSaveEvent.cs : Error " + ex, this);
            }

        }
        /// <summary>
        /// Format HTML properly by removing unwanted nodes 
        /// </summary>
        /// <param name="content"></param>
        private void FormatProperHTML(HtmlDocument content)
        {
            //Removes Span tag for Text content because it should be with P tag(this usually happens when copying content from browser and paste it RTE)
            HtmlNodeCollection spanNodes = content.DocumentNode.SelectNodes("//span");
            if (spanNodes != null)
            {
                foreach (HtmlNode node in spanNodes)
                {
                    HtmlNode text = HtmlNode.CreateNode(node.InnerHtml);
                    node.ParentNode.ReplaceChild(text, node);
                }
            }
            //Getting all Text node without P tag , and add wrapping P tag around it
            HtmlNodeCollection textNodes = content.DocumentNode.SelectNodes("//text()");
            if (textNodes != null && textNodes.Count > 0)
            {
                foreach (HtmlNode node in textNodes)
                {
                    if (node.ParentNode != null && node.ParentNode.Name == "div")
                    {
                        HtmlNode title = HtmlNode.CreateNode("<p>" + node.InnerHtml.Replace("<br />", "<p/><p>") + "</p>");
                        node.InnerHtml = title.OuterHtml;
                    }
                }

            }
            //Remove P tag wrapped on comments and remove class attribute
            HtmlNodeCollection pNodes = content.DocumentNode.SelectNodes("//p");
            if (pNodes != null)
            {

                foreach (HtmlNode node in pNodes)
                {
                    node.InnerHtml = node.InnerHtml.Trim();
                    if (node.InnerHtml.StartsWith("<!"))
                    {
                        HtmlNode title = HtmlNode.CreateNode(node.InnerHtml);
                        node.ParentNode.ReplaceChild(title, node);

                    }
                    node.Attributes.Remove("class");
                }
            }
            //Remove unneccessary BR tag
            HtmlNodeCollection brNodes = content.DocumentNode.SelectNodes("//br");
            if (brNodes != null)
            {

                foreach (HtmlNode node in brNodes)
                {
                    node.ParentNode.RemoveChild(node);
                }
            }
            //Getting all Class used in RTE(as some times it is appeared in different nodes apart from expected, so removing the assigned class
            var result = content.DocumentNode.Descendants()
             .Where(x => x.Attributes.Contains("class") && CommonConstants.RteClassNames.Contains(x.Attributes["class"].Value)).ToList();
            foreach (var node in result)
            {
                if(node.Name!="div")
                {
                    node.Attributes.Remove("class");
                }
            }      
        }
        /// <summary>
        /// Removes Empty Ptags
        /// </summary>
        /// <param name="content"></param>
        private void RemoveEmptyPTags(HtmlDocument content)
        {
            HtmlNodeCollection pNodes = content.DocumentNode.SelectNodes("//p");
            if (pNodes != null && pNodes.Count > 0)
            {
                foreach (HtmlNode pTag in pNodes)
                {
                    if (string.IsNullOrWhiteSpace(pTag.InnerHtml) || pTag.InnerHtml == "&nbsp;" || pTag.InnerHtml == "\n" || pTag.InnerHtml == "\n\n")
                    {
                        pTag.ParentNode.RemoveChild(pTag);
                    }
                }
            }
        }
        /// <summary>
        /// Removes Empty Div tags
        /// </summary>
        /// <param name="content"></param>
        private void RemoveEmptyDivTags(HtmlDocument content)
        {
            HtmlNodeCollection pNodes = content.DocumentNode.SelectNodes("//div");
            if (pNodes != null && pNodes.Count > 0)
            {
                foreach (HtmlNode divTag in pNodes)
                {
                    if (string.IsNullOrWhiteSpace(divTag.InnerHtml) || divTag.InnerHtml == "&nbsp;" || divTag.InnerHtml == "\n" || divTag.InnerHtml == "\n\n")
                    {
                        divTag.ParentNode.RemoveChild(divTag);
                    }
                }
            }
        }
        #endregion
    }
}


