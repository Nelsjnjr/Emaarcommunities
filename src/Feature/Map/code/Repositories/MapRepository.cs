#region namespace
using System;
using EMAAR.Emaarcommunities.Feature.Map.Interfaces;
using EMAAR.Emaarcommunities.Foundation.DependencyInjection;
using EMAAR.Emaarcommunities.Foundation.ORM.Models.sitecore.templates.Project.Emaarcommunities.Content_Types.Interactive_Map;
using Glass.Mapper.Sc.Web.Mvc;
#endregion
namespace EMAAR.Emaarcommunities.Feature.Map.Repositories
{
    /// <summary>
    /// Repositoy class for interacting with business objects
    /// </summary>
    [Service(typeof(IMapRepository))]
    public class MapRepository: IMapRepository
    {
        private readonly Func<IMvcContext> _mvcContext;
        private readonly IInteractive_Map _interactive_Map ;
        public MapRepository(Func<IMvcContext> mvcContext, IInteractive_Map interactive_Map  )
        {
            _interactive_Map = interactive_Map;
            _mvcContext = mvcContext;
        }
        /// <summary>
        /// Getting all Interactive Map component asigned in Sitecore on field (Interactive Maps)
        /// </summary>
        /// <returns>InteractiveMapList</returns>
        public IInteractive_Map InteractiveMaps()
        {
            IMvcContext mvcContext = _mvcContext();
            IInteractive_Map interactive_Map   = mvcContext.GetDataSourceItem<IInteractive_Map>();
            return interactive_Map ?? _interactive_Map;
        }
    }
}