#region namespace
using EMAAR.Emaarcommunities.Feature.Banner.Interfaces;
using EMAAR.Emaarcommunities.Foundation.ORM.Models.sitecore.templates.Project.Emaarcommunities.Content_Types.Banner;
using EMAAR.Emaarcommunities.Foundation.ORM.Models.sitecore.templates.Project.Emaarcommunities.Content_Types.Hero;
using EMAAR.Emaarcommunities.Foundation.ORM.Models.sitecore.templates.Project.Emaarcommunities.Content_Types.Homepage_carousel;
using EMAAR.Emaarcommunities.Foundation.SitecoreExtensions.Interfaces;
using System.Web.Mvc;
using static EMAAR.Emaarcommunities.Foundation.SitecoreExtensions.Settings.SitecoreSettings;
#endregion
namespace EMAAR.Emaarcommunities.Feature.Banner.Controllers
{
    /// <summary>
    /// This controller is used for loading ImageText component
    /// </summary>
    public class BannerController : Controller
    {
        #region Private property
        private readonly IBannerRepository _bannerRepository;
      
        #endregion

        #region construtor
        public BannerController(IBannerRepository bannerRepository, ISitecoreHelper sitecoreHelper)
        {
            _bannerRepository = bannerRepository;
        
        }
        #endregion
        #region method
        /// <summary>
        /// Getting 2 variants of ImageText components(Left,Right )
        /// </summary>
        /// <returns>ImageText component variation based on parameter selected from Sitecore</returns>
        public ActionResult ImageText()
        {
            IImageText imageText = _bannerRepository.GetImageText();
            return View($"{ViewPath}Banner/ImageText/_ImageText.cshtml", imageText);
        }
        /// <summary>
        /// Getting 2 variants of Parallax components(background image )
        /// </summary>
        /// <returns>Parallax</returns>
        public ActionResult Parallax()
        {
            IParallax parallax = _bannerRepository.GetParallax();
            return View($"{ViewPath}Banner/Parallax/_Parallax.cshtml", parallax);
        }
        /// <summary>
        /// Getting all related component asigned in Sitecore with the Background CSS (eg:explore)
        /// </summary>  
        /// <returns>Relatedcontent list</returns>
        public ActionResult HomePageCarousels()
        {
            IHomepage_Carousels homepage_CarouselList = _bannerRepository.HomePageCarousels();
            return View($"{ViewPath}Banner/HomePageCarousel/_HomePageCarousel.cshtml", homepage_CarouselList);
        }
       
        /// <summary>
        ///  Getting all Hero Banner asigned in Sitecore 
        /// </summary>
        /// <returns>HeroBannerList</returns>
        public ActionResult GetHero()
        {
            IHero hero = _bannerRepository.GetHero();
            return View($"{ViewPath}Banner/Hero/_Hero.cshtml", hero);
        }
       
        #endregion
    }
}

