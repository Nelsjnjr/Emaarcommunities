using EMAAR.Emaarcommunities.Foundation.ORM.Models.sitecore.templates.Foundation.Emaarcommunities.Base;

namespace EMAAR.Emaarcommunities.Feature.SEO.Interfaces
{
    /// <summary>
    /// This is used to set SEO functionalities
    /// </summary>
    public interface ISeoRepository
    {
        #region method
        /// <summary>
        /// Getting page metadata
        /// </summary>
        /// <returns>Metadata/OG/Twitter,Canonical and hreflang</returns>
        ISeoViewModel GetSeo();
        #endregion
    }
}
