#region namespace
using EMAAR.Emaarcommunities.Foundation.ORM.Models.sitecore.templates.Project.Emaarcommunities.Content_Types.Amenity;
using EMAAR.Emaarcommunities.Foundation.ORM.Models.sitecore.templates.Project.Emaarcommunities.Content_Types.Banner;
using EMAAR.Emaarcommunities.Foundation.ORM.Models.sitecore.templates.Project.Emaarcommunities.Content_Types.Hero;
using EMAAR.Emaarcommunities.Foundation.ORM.Models.sitecore.templates.Project.Emaarcommunities.Content_Types.Homepage_carousel;
#endregion
namespace EMAAR.Emaarcommunities.Feature.Banner.Interfaces
{
    /// <summary>
    /// Repositoy class for interacting with business objects
    /// </summary>
    public interface IBannerRepository
    {
        #region method
        /// <summary>
        /// Getting 3 variants of ImageText components(Left,Right and Background)
        /// <param name="alignment">Variant</param>
        /// <returns>ImageText component variation based on parameter selected from Sitecore</returns>
        IImageText GetImageText();
        IParallax GetParallax();
        /// <summary>
        /// Getting all related component asigned in Sitecore 
        /// </summary>
        /// <returns>Related content details</returns>
        IHomepage_Carousels HomePageCarousels();
       
        /// <summary>
        /// Getting all HeroBanner component asigned in Sitecore on field (Hero Community Metrics)
        /// </summary>
        /// <returns>HeroBannerList</returns>
        IHero GetHero();
      
        #endregion
    }
}
