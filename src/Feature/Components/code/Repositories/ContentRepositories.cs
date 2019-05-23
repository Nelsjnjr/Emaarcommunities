#region namespace
using System;
using System.Linq;
using EMAAR.Emaarcommunities.Feature.ContentComponents.Interfaces;
using EMAAR.Emaarcommunities.Feature.ContentComponents.Settings;
using EMAAR.Emaarcommunities.Foundation.DependencyInjection;
using EMAAR.Emaarcommunities.Foundation.ORM.Models.sitecore.templates.Project.Emaarcommunities.Content_Types.Amenity;
using EMAAR.Emaarcommunities.Foundation.ORM.Models.sitecore.templates.Project.Emaarcommunities.Content_Types.Faq;
using EMAAR.Emaarcommunities.Foundation.ORM.Models.sitecore.templates.Project.Emaarcommunities.Content_Types.Related_Pages;
using EMAAR.Emaarcommunities.Foundation.ORM.Models.sitecore.templates.Project.Emaarcommunities.Page_Types;
using Glass.Mapper.Sc.Web.Mvc;
using Sitecore.Data;
using Sitecore.Data.Fields;
#endregion
namespace EMAAR.Emaarcommunities.Feature.ContentComponents.Repositories
{
    [Service(typeof(IContentRepositories))]
    public class ContentRepositories : IContentRepositories
    {
        #region property
        private readonly Func<IMvcContext> _mvcContext;
        private readonly IAmenities _amenities;
        private readonly IGeneric_ContentPage _generic;
        private readonly IFaqs _faqs;
        private readonly IRelated_Pages _related_Pages;


        #endregion
        #region construtor
        public ContentRepositories(Func<IMvcContext> mvcContext, IGeneric_ContentPage generic, IAmenities amenities, IFaqs faqs, IRelated_Pages related_Pages)
        {
            _generic = generic;
            _mvcContext = mvcContext;
            _amenities = amenities;
            _faqs = faqs;
            _related_Pages = related_Pages;
        }
        #endregion
        #region method
        /// <summary>
        /// Getting Content Page details
        /// </summary>
        /// <returns>Content page </returns>
        public IGeneric_ContentPage GetGenericContentPage(out string date)
        {
            IMvcContext mvcContext = _mvcContext();
            date = String.Empty;
            if (mvcContext.ContextItem.TemplateID.Equals(ID.Parse(SitecoreSettings.NewsPageTemplateID))||
                mvcContext.ContextItem.TemplateID.Equals(ID.Parse(SitecoreSettings.EventsPageTemplateID)))
            {
                DateField dt = mvcContext.ContextItem.Fields["Date"];
                date =  Sitecore.DateUtil.FormatDateTime(dt.DateTime, EMAAR.Emaarcommunities.Foundation.SitecoreExtensions.Settings.SitecoreSettings.DateFormat, mvcContext.ContextItem.Language.CultureInfo); 
            }
            IGeneric_ContentPage generic = mvcContext.GetContextItem<IGeneric_ContentPage>();
            return generic ?? _generic;

        }
        /// <summary>
        /// Getting all amenities  when adding as rendering component                 
        /// </summary>
        /// <returns>Amenities</returns>
        public IAmenities GetAmenities()
        {  
            IMvcContext mvcContext = _mvcContext();
            IAmenities amenities = mvcContext.GetDataSourceItem<IAmenities>();
            _amenities.Page_Amenities = null;
            //First check the datasource item if empty then get the details from context template item
            if (amenities == null)
            {
                IGeneric_ContentPage generic_ContentPage = mvcContext.GetContextItem<IGeneric_ContentPage>();
                if (generic_ContentPage?.Amenities?.Count() > 0)
                {
                    _amenities.Page_Amenities = generic_ContentPage.Amenities;
                }
            }
            return amenities?.Page_Amenities != null ? amenities : _amenities;
   
        }
        /// <summary>
        /// Getting all Faqs  when adding as rendering component
        /// </summary>
        /// <returns>Faqs</returns>
        public IFaqs GetFaqs()
        {
            IMvcContext mvcContext = _mvcContext();
            IFaqs faqs = mvcContext.GetDataSourceItem<IFaqs>();
            _faqs.Page_Faqs = null;
            //First check the datasource item if empty then get the details from context template item
            if (faqs == null)
            {
                IGeneric_ContentPage generic_ContentPage = mvcContext.GetContextItem<IGeneric_ContentPage>();
                if (generic_ContentPage?.Faqs?.Count() > 0)
                {
                    _faqs.Page_Faqs = generic_ContentPage.Faqs;
                }
            }
            return faqs?.Page_Faqs != null ? faqs : _faqs;
        }
        ///// <summary>
        ///// Getting all Related pages component asigned in Sitecore   when adding as rendering component
        ///// </summary>
        ///// <returns>Related Pages details</returns>
        public IRelated_Pages RelatedPages()
        {
            IRelated_Pages related_Pages = null;
            IMvcContext mvcContext = _mvcContext();

            _related_Pages.Pages = null;
            //First check the datasource item if empty then get the details from context template item
            related_Pages = mvcContext.GetDataSourceItem<IRelated_Pages>();
            if (related_Pages == null)
            {
                IGeneric_ContentPage generic_ContentPage = mvcContext.GetContextItem<IGeneric_ContentPage>();
                if (generic_ContentPage?.Related_Pages?.Count() > 0)
                {
                    _related_Pages.Pages = generic_ContentPage.Related_Pages;
                }
            }
            return related_Pages?.Pages != null ? related_Pages : _related_Pages;
        }
        #endregion
    }
}