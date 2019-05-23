#region namespace
using EMAAR.Emaarcommunities.Foundation.ORM.Models.sitecore.templates.Project.Emaarcommunities.Content_Types.Interactive_Map;
#endregion
namespace EMAAR.Emaarcommunities.Feature.Map.Interfaces
{
    public interface IMapRepository
    {

        #region method
        /// <summary>
        /// Getting all Interactive Map Location points component asigned in Sitecore on field (Interactive Maps)
        /// </summary>
        /// <returns>InteractiveMapList</returns>
        IInteractive_Map InteractiveMaps();
        #endregion
    }
}
