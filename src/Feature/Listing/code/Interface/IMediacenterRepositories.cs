#region namespace
using EMAAR.Emaarcommunities.Foundation.ORM.Models.sitecore.templates.Project.Emaarcommunities.Page_Types.Mediacenter;
#endregion

namespace EMAAR.Emaarcommunities.Feature.Listing.Interface
{
    /// <summary>
    /// This is used to get the Media page with all necessary components(Top 3 News,Events,Downloads,I,age Gallery and Video gallery)
    /// </summary>
    public interface IMediacenterRepositories
    {
        #region method
        /// <summary>
        /// This is used to get the Media page with all necessary components(Top 3 News,Events,Downloads,I,age Gallery and Video gallery)
        /// </summary>
        /// <returns>Mediacenter page</returns>
        IMediacenterViewmodel GetMediacenterPage();
        #endregion
    }
}
