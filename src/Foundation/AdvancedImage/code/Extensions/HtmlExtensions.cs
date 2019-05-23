namespace Sitecore.Framework.Fields.Extensions
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Xml;
    using Data.Fields;
    using Data.Items;
    using Mvc.Helpers;
    using Resources.Media;

    public static class HtmlHelperExtensions
    {
        public static HtmlString AdvancedImageField(this SitecoreHelper helper, string fieldName, Item item, int width = 0, int height = 0, string alt = null, string cssClass = null, bool disableWebEditing = false)
        {
            /*
             Example usage in view @Html.Sitecore().AdvancedImageField("Cropped", Model.Item, 100, 400)

             * */
            if (item == null)
            {
                return new HtmlString("No datasource set");
            }

            ImageField imageField = new ImageField(item.Fields[fieldName]);
            if (String.IsNullOrEmpty(imageField.Value))
            {
                return new HtmlString(String.Empty);
            }
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(imageField.Value);

            if (xml.DocumentElement == null)
            {
                return new HtmlString("No cropping image parameters found.");
            }

            string cropx = xml.DocumentElement.HasAttribute("cropx") ? xml.DocumentElement.GetAttribute("cropx") : string.Empty;
            string cropy = xml.DocumentElement.HasAttribute("cropy") ? xml.DocumentElement.GetAttribute("cropy") : string.Empty;
            string focusx = xml.DocumentElement.HasAttribute("focusx") ? xml.DocumentElement.GetAttribute("focusx") : string.Empty;
            string focusy = xml.DocumentElement.HasAttribute("focusy") ? xml.DocumentElement.GetAttribute("focusy") : string.Empty;
            float.TryParse(cropx, out float cx);
            float.TryParse(cropy, out float cy);
            float.TryParse(focusx, out float fx);
            float.TryParse(focusy, out float fy);

            string imageSrc = MediaManager.GetMediaUrl(imageField.MediaItem);

            string src = $"{imageSrc}?cx={cx}&cy={cy}&cw={width}&ch={height}";

            string hash = HashingUtils.GetAssetUrlHash(src);

            TagBuilder builder = new TagBuilder("img");
            builder.Attributes.Add("src", $"{src}&hash={hash}");
            builder.Attributes.Add("alt", !String.IsNullOrEmpty(imageField.Alt) ? imageField.Alt : alt);

            if (!string.IsNullOrEmpty(cssClass))
            {
                builder.AddCssClass(cssClass);
            }

            return MvcHtmlString.Create(builder.ToString(TagRenderMode.Normal));

        }

    }
}
