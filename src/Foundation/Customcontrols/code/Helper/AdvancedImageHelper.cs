using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using EMAAR.ECM.Foundation.Customcontrols.Fields;
using EMAAR.ECM.Foundation.Customcontrols.Fields.Extensions;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Resources.Media;

namespace EMAAR.ECM.Foundation.Customcontrols.Helper
{
    public static class AdvancedImageHelper
    {
        /// <summary>
        /// Getting advanced image url with cropping (imagefield)
        /// </summary>
        /// <param name="imageField"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static string GetImageFieldUrl(ImageField imageField, int width = 0, int height = 0)
        {
            var img = new AdvanceImageField();
            var xml = new XmlDocument();
            xml.LoadXml(imageField.Value);
            var cropx = xml.DocumentElement.HasAttribute("cropx") ? xml.DocumentElement.GetAttribute("cropx") : string.Empty;
            var cropy = xml.DocumentElement.HasAttribute("cropy") ? xml.DocumentElement.GetAttribute("cropy") : string.Empty;
            var focusx = xml.DocumentElement.HasAttribute("focusx") ? xml.DocumentElement.GetAttribute("focusx") : string.Empty;
            var focusy = xml.DocumentElement.HasAttribute("focusy") ? xml.DocumentElement.GetAttribute("focusy") : string.Empty;

            float cx, cy, fx, fy;
            float.TryParse(cropx, out cx);
            float.TryParse(cropy, out cy);
            float.TryParse(focusx, out fx);
            float.TryParse(focusy, out fy);

            img.CropX = cx;
            img.CropY = cy;
            img.FocusX = fx;
            img.FocusY = fy;
            img.Alt = imageField.Alt;
            img.Border = imageField.Border;
            img.Class = imageField.Class;
            img.Width = System.Convert.ToInt32(string.IsNullOrEmpty(imageField.Width) ? "0" : imageField.Width);
            img.Height = System.Convert.ToInt32(string.IsNullOrEmpty(imageField.Height) ? "0" : imageField.Height);
            img.HSpace = System.Convert.ToInt32(string.IsNullOrEmpty(imageField.HSpace) ? "0" : imageField.HSpace);
            img.Language = imageField.MediaLanguage;
            img.MediaId = imageField.MediaID.ToGuid();
            img.Src = MediaManager.GetMediaUrl(imageField.MediaItem);
            img.VSpace = System.Convert.ToInt32(string.IsNullOrEmpty(imageField.VSpace) ? "0" : imageField.VSpace);
            return img.GetUrl(width, height);
        }
        /// <summary>
        ///  Getting advanced image url with cropping (Item)
        /// </summary>
        /// <param name="item"></param>
        /// <param name="fieldName"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static string GetImageFieldUrl(Item item, string fieldName, int width = 0, int height = 0)
        {
            Field field = item.Fields[fieldName];
            var scImg = new ImageField(field);
            var img = new AdvanceImageField();

            var xml = new XmlDocument();
            xml.LoadXml(scImg.Value);
            var id = xml.DocumentElement.GetAttribute("mediaid");
            var cropx = xml.DocumentElement.HasAttribute("cropx") ? xml.DocumentElement.GetAttribute("cropx") : string.Empty;
            var cropy = xml.DocumentElement.HasAttribute("cropy") ? xml.DocumentElement.GetAttribute("cropy") : string.Empty;
            var focusx = xml.DocumentElement.HasAttribute("focusx") ? xml.DocumentElement.GetAttribute("focusx") : string.Empty;
            var focusy = xml.DocumentElement.HasAttribute("focusy") ? xml.DocumentElement.GetAttribute("focusy") : string.Empty;

            float cx, cy, fx, fy;
            float.TryParse(cropx, out cx);
            float.TryParse(cropy, out cy);
            float.TryParse(focusx, out fx);
            float.TryParse(focusy, out fy);

            img.CropX = cx;
            img.CropY = cy;
            img.FocusX = fx;
            img.FocusY = fy;
            img.Alt = scImg.Alt;
            img.Border = scImg.Border;
            img.Class = scImg.Class;
            img.Width = System.Convert.ToInt32(string.IsNullOrEmpty(scImg.Width) ? "0" : scImg.Width);
            img.Height = System.Convert.ToInt32(string.IsNullOrEmpty(scImg.Height) ? "0" : scImg.Height);
            img.HSpace = System.Convert.ToInt32(string.IsNullOrEmpty(scImg.HSpace) ? "0" : scImg.HSpace);
            img.Language = scImg.MediaLanguage;
            img.MediaId = scImg.MediaID.ToGuid();
            img.Src = MediaManager.GetMediaUrl(scImg.MediaItem);
            img.VSpace = System.Convert.ToInt32(string.IsNullOrEmpty(scImg.VSpace) ? "0" : scImg.VSpace);

            return img.GetUrl(width, height);
        }
    }
}