#region namespace
using System.Web.Mvc;
using EMAAR.Emaarcommunities.Feature.Listing.Interface;
using EMAAR.Emaarcommunities.Foundation.ORM.Models.sitecore.templates.Project.Emaarcommunities.Page_Types.Mediacenter;
using static EMAAR.Emaarcommunities.Foundation.SitecoreExtensions.Settings.SitecoreSettings;
#endregion
namespace EMAAR.Emaarcommunities.Feature.Listing.Controllers
{
    public class MediacenterController : Controller
    {
        #region Private property
        private readonly IMediacenterRepositories _mediacenterRepositories;
        #endregion
        #region constructor
        public MediacenterController(IMediacenterRepositories mediacenterRepositories )
        {
            _mediacenterRepositories = mediacenterRepositories;
        }
        #endregion
        #region method
        /// <summary>
        /// This is used to get the Media page with all necessary components(Top 3 News,Events,Downloads,I,age Gallery and Video gallery)
        /// </summary>
        /// <returns>Mediacenter page</returns>
        public ActionResult GetMediacenterPage()
        {
            IMediacenterViewmodel mediacenter  = _mediacenterRepositories.GetMediacenterPage();
            return View($"{ViewPath}Mediacenter/MediacenterPage.cshtml", mediacenter);
        }
        #endregion
    }
}