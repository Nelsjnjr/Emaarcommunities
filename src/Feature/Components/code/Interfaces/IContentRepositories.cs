#region namespace
using EMAAR.Emaarcommunities.Foundation.ORM.Models.sitecore.templates.Project.Emaarcommunities.Content_Types.Amenity;
using EMAAR.Emaarcommunities.Foundation.ORM.Models.sitecore.templates.Project.Emaarcommunities.Content_Types.Faq;
using EMAAR.Emaarcommunities.Foundation.ORM.Models.sitecore.templates.Project.Emaarcommunities.Content_Types.Related_Pages;
using EMAAR.Emaarcommunities.Foundation.ORM.Models.sitecore.templates.Project.Emaarcommunities.Page_Types;
#endregion

namespace EMAAR.Emaarcommunities.Feature.ContentComponents.Interfaces
{
    public interface IContentRepositories
    {
        #region method
        /// <summary>
        /// Getting Content Page details
        /// </summary>
        /// <returns></returns>
        IGeneric_ContentPage GetGenericContentPage(out string date);
        /// <summary>
        /// Getting all Amenities
        /// </summary>
        /// <returns>Amenities</returns>
        IAmenities GetAmenities();
        /// <summary>
        /// Getting all Faqs
        /// </summary>
        /// <returns>Faqs</returns>
        IFaqs GetFaqs();
        ///// <summary>
        ///// Getting all related pages asigned in Sitecore 
        ///// </summary>
        ///// <returns>Related pages details</returns>
        IRelated_Pages RelatedPages();
        #endregion

    }
}
