#region namespace
using System.Web.Mvc;
using EMAAR.Emaarcommunities.Feature.Map.Interfaces;
using EMAAR.Emaarcommunities.Foundation.SitecoreExtensions.Interfaces;
using EMAAR.Emaarcommunities.Foundation.ORM.Models.sitecore.templates.Project.Emaarcommunities.Content_Types.Interactive_Map;
using static EMAAR.Emaarcommunities.Foundation.SitecoreExtensions.Settings.SitecoreSettings;
#endregion
namespace EMAAR.Emaarcommunities.Feature.Map.Controllers
{
    public class MapController : Controller
    {
        #region Private property
        private readonly IMapRepository _mapRepository;
        #endregion
        #region construtor     
        public MapController(IMapRepository mapRepository, ISitecoreHelper sitecoreHelper)
        {
           
               _mapRepository = mapRepository;
        }
        #endregion
        #region method     
        /// <summary>
        /// Getting Interactive map points from sitecore and place it on the background image
        /// </summary>
        /// <returns></returns>
        public ActionResult InteractiveMaps()
        {
            IInteractive_Map interactive_Map   = _mapRepository.InteractiveMaps();
            return View($"{ViewPath}Map/InteractiveMap/_InteractiveMap.cshtml", interactive_Map);
        }
        #endregion
    }
}