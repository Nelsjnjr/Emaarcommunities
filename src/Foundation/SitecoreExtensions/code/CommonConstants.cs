namespace EMAAR.Emaarcommunities.Foundation.SitecoreExtensions
{
    /// <summary>
    /// Common constants
    /// </summary>
    public static class CommonConstants
    {
        #region property
        /// <summary>
        /// Getting Alignment Variations
        /// </summary>
        public enum Alignment { Left, Right, Background };
        /// <summary>
        /// Getting Explore constants for CSS
        /// </summary>
        public static string Explore { get; } = "explore";
        /// <summary>
        /// Getting Home page header CSS class name
        /// </summary>
        public static string HomePageHeaderCss { get; } = "siteHeader";
        /// <summary>
        /// Getting Content Page Css Class name
        /// </summary>
        public static string ContentPageHeaderCss { get; } = "siteHeader innerHeader";

       
        /// <summary>
        /// Introduction Field Name
        /// </summary>
        public static string Introduction { get; } = "Introduction";
        /// <summary>
        /// Getting Contact Types
        /// </summary>
        public enum ContactTypes { Email, Phone, Map };
        /// <summary>
        /// Year Token for dynamically modify it
        /// </summary>
        public static string YearToken { get; } = "{year}";
        /// <summary>
        /// Registering the all javascripts to bundle and minify with the name
        /// </summary>
        public static string ScriptsBundleName { get; } = "~/bundles/scripts";
        /// <summary>
        /// All jquery /common javascript Application files path
        /// </summary>
        public static string[] AllScriptsPaths => new string[] {
            "~/Assets/Project/Emaarcommunities/js/vendor/jquery/*.js",
            "~/Assets/Project/Emaarcommunities/js/vendor/bootstrap/bootstrap.js",
            "~/Assets/Project/Emaarcommunities/js/vendor/common/wow.js",
            "~/Assets/Project/Emaarcommunities/js/vendor/common/map-popup.js",
            "~/Assets/Project/Emaarcommunities/js/vendor/common/swiper.js",
            "~/Assets/Project/Emaarcommunities/js/vendor/common/select2.js",
            "~/Assets/Project/Emaarcommunities/js/vendor/common/handlebars-v4.1.1.js",
            "~/Assets/Project/Emaarcommunities/js/CDN/jquery.fancybox.js",
            "~/Assets/Project/Emaarcommunities/js/application/app.js",
            "~/Assets/Project/Emaarcommunities/js/application/ajax.js",
            "~/Assets/Project/Emaarcommunities/js/application/main.js",

        };
        #region Dictionarykey
        /// <summary>
        /// Dictionary key for ViewAll
        /// </summary>
        public const string ViewAll = "viewall";
        /// <summary>
        /// Dictionary key for News
        /// </summary>
        public const string News = "news";
        /// <summary>
        /// Dictionary key for Events
        /// </summary>
        public const string Events = "events";
        /// <summary>
        /// Dictionary key for Downloads
        /// </summary>
        public const string Downloads = "downloads";
        /// <summary>
        /// Dictionary key for Download
        /// </summary>
        public const string Download = "download";
        /// <summary>
        /// Dictionary key for ReadOnline
        /// </summary>
        public const string ReadOnline = "ReadOnline";
        /// <summary>
        /// Dictionary key for ImageGallery
        /// </summary>
        public const string ImageGallery = "imagegallery";
        /// <summary>
        /// Dictionary key for VideoGallery
        /// </summary>
        public const string VideoGallery = "videogallery";
        /// <summary>
        /// Dictionary key for ScrollDown
        /// </summary>
        public const string ScrollDown = "scrolldown";
        /// <summary>
        /// Dictionary key for Amenities
        /// </summary>
        public const string Amenities = "amenities";
        /// <summary>
        /// Sitemap key for Amenities
        /// </summary>
        public const string Sitemap = "sitemap";

        #endregion Dictionarykey

        #region ItemFieldName
        /// <summary>
        /// FullBannerImage Field Name
        /// </summary>
        public const string FullBannerImage = "FullBannerImage";
        /// <summary>
        /// Banner Field Name
        /// </summary>
        public const string Banner = "Banner";
        /// <summary>
        /// Image Field Name
        /// </summary>
        public const string Image = "Image";
        /// <summary>
        /// ThumbnailImage Field Name
        /// </summary>
        public const string ThumbnailImage = "ThumbnailImage";
        /// <summary>
        /// Hero Image Field Name
        /// </summary>
        public const string HeroImage = "Hero Image";
        /// <summary>
        /// Title Field Name
        /// </summary>
        public const string Title = "Title";
        /// <summary>
        /// Is Events Page Field Name
        /// </summary>
        public const string IsEventsPage = "Is Events Page";
        /// <summary>
        /// Navigation URL Field Name
        /// </summary>
        public const string NavigationURL = "Navigation URL";
        /// <summary>
        /// Navigation Title Field Name
        /// </summary>
        public const string NavigationTitle = "Navigation Title";


        #endregion ItemFieldName
        /// <summary>
        /// Internal constants value
        /// </summary>
        public const string Internal = "Internal";
        /// <summary>
        /// Global constants value
        /// </summary>
        public const string Global = "Global";
        /// <summary>
        /// All events constants value
        /// </summary>
        public const string AllEvents = "AllEvents";
        /// <summary>
        /// Used as text in Experience editor for complex image fields
        /// </summary>
        public const string WebEditExperinceEditorImageEditingText = "Edit Background Image";
        /// <summary>
        /// RTE Class names
        /// </summary>
        public static string[] RteClassNames => new string[] { "image-right-section", "image-left-section", "table-responsive" };
        /// <summary>
        /// Updated date index field
        /// </summary>
        public static string Updated { get; } = "__updated";
        /// <summary>
        /// Template name for Image Album
        /// </summary>
        public const string ImageAlbumPageTemplateName = "Image Album";
        /// <summary>
        /// Template name for Image Item
        /// </summary>
        public const string ImageItemPageTemplateName = "Image Item";
        /// <summary>
        /// Template name for Video Album Without Filters
        /// </summary>
        public const string VideoAlbumWithoutFilterPageTemplateName = "Video Album Without Filters";
        /// <summary>
        /// Template name for Video Album With Filters
        /// </summary>
        public const string VideoAlbumWithFilterPageTemplateName = "Video Album With Filters";
        /// <summary>
        /// Template name for Image Gallery Page
        /// </summary>
        public const string ImageGalleryPageTemplateName = "Image Gallery Page";
        /// <summary>
        /// Template name for Video Gallery Page
        /// </summary>
        public const string VideoGalleryPageTemplateName = "Video Gallery Page";
        /// <summary>
        /// Template name for Video Item
        /// </summary>
        public const string VideoItemPageTemplateName = "Video Item";
        /// <summary>
        /// Template name for Events Listing Page
        /// </summary>
        public const string EventsPageTemplateName = "Events Listing Page";
        /// <summary>
        /// Template name for Downloads Page
        /// </summary>
        public const string DownloadsPageTemplateName = "Downloads Page";
        /// <summary>
        /// Template name for News Listing Page
        /// </summary>
        public const string NewsPageTemplateName = "News Listing Page";
        /// <summary>
        /// Include in Sitemap XML Field Name
        /// </summary>
        public const string IncludeinSitemap = "Include in Sitemap XML";

        #region Search

        /// <summary>
        /// Index Sortorder field
        /// </summary>
        public static string Sortorder { get; } = "__Sortorder";

        /// <summary>
        /// Index custom Sortorder field
        /// </summary>
        public static string CustomSortorder { get; } = "customsortorder";

        /// <summary>
        /// Index Language field
        /// </summary>
        public static string Language { get; } = "_language";

        /// <summary>
        /// Index Template ID field
        /// </summary>
        public static string TemplateID { get; } = "_template";

        /// <summary>
        /// Index Name field
        /// </summary>
        public static string IndexNameField { get; } = "_name";

        /// <summary>
        /// PageUrl field
        /// </summary>
        public static string PageUrl { get; } = "pageurl";

        /// <summary>
        /// RenderingContent field
        /// </summary>
        public static string RenderingsContentIndexField { get; } = "renderingscontent";

        /// <summary>
        /// Title Field in Index
        /// </summary>
        public static string TitleIndexField { get; } = "title";

        /// <summary>
        /// Introduction Field in Index
        /// </summary>
        public static string IntroductionIndexField { get; } = "introduction";

        /// <summary>
        /// Introduction Field in Index
        /// </summary>
        public static string ContentIndexField { get; } = "content";

        /// <summary>
        /// Index Latest version field
        /// </summary>
        public static string Latestversion { get; } = "_latestversion";
        /// <summary>
        /// Index CustomSiteFieldName field
        /// </summary>
        public static string CustomSiteFieldName { get; } = "customsitefieldname";
        
        /// <summary>
        /// Index ID field
        /// </summary>
        public const string IndexIdField = "_group";

        /// <summary>
        /// All filter value
        /// </summary>
        public const string AllValue = "all";


        /// <summary>
        /// Invalid ID
        /// </summary>
        public const string InvalidID = "00000000000000000000000000000000";

        /// <summary>
        /// None Value
        /// </summary>
        public const string NoneValue = "None";

        /// <summary>
        /// None Value
        /// </summary>
        public const string NameStandardValueToken = "$name";


        /// <summary>
        /// Batch Size
        /// </summary>
        public const int BatchSize = 50;

        /// <summary>
        /// Index Name Prefix
        /// </summary>
        public const string IndexNamePrefix = "communitieshi";

        /// <summary>
        /// DefaultIndex Name Prefix
        /// </summary>
        public const string DefaultIndexNamePrefix = "sitecore";

        /// <summary>
        /// Year Facet Field
        /// </summary>
        public const string YearFacetField = "customyear";

        /// <summary>
        /// Date Field
        /// </summary>
        public const string DateField = "date";

        /// <summary>
        /// Categories Facet Field
        /// </summary>
        public const string CategoriesFacetField = "categories";
        /// <summary>
        /// SiteRoot Template ID
        /// </summary>
        public const string SiteRootTemplateID = "{407F6EA5-A542-4D83-9D03-B598E06D3422}";
       
        /// <summary>
        /// No Image Item ID
        /// </summary>
        public const string NoImageItemID = "{F97AD3C4-CCCE-4C41-8C42-33D7335FEE58}";
        /// <summary>
        /// Home Template ID
        /// </summary>
        public const string HomePageTemplateID = "{337B3473-E10A-4490-88B5-8255FF80D0F2}";
        /// <summary>
        /// GenericContentPage Template ID
        /// </summary>
        public const string GenericContentPageTemplateID = "{4FB05EC1-985E-46D3-A95B-A851FF0BE873}";
        /// <summary>
        /// MediaCenterPageTemplateID Template ID
        /// </summary>
        public const string MediaCenterPageTemplateID = "{AB4F05B9-0538-4014-835A-545DED4920C1}";
        /// <summary>
        /// DownloadsPageTemplateID Template ID
        /// </summary>
        public const string DownloadsTemplateID = "{6D04D32E-59D9-4F3F-8748-3C89E5247366}";
        /// <summary>
        /// Image Album Page Template ID
        /// </summary>
        public const string ImageAlbumPageTemplateID = "{5E7F38ED-D38C-4EA8-8DBD-D3606F4E1E08}";
        /// <summary>
        /// Image Gallery Page Template ID
        /// </summary>
        public const string ImageGalleryPageTemplateID = "{70422DB2-25AF-483F-A3F1-AAF7CD751D5D}";

        /// <summary>
        /// Video Album with out Filter Template ID
        /// </summary>
        public const string VideoAlbumWithoutFiltersTemplateID = "{2C088140-3A80-4CB7-9E80-198AFCB01574}";
        /// <summary>
        /// Video Album with  Filter Template ID
        /// </summary>
        public const string VideoAlbumWithFiltersTemplateID = "{C14D3C3F-2CA3-4AC1-9FCB-F8BB6D27DD04}";

        /// <summary>
        /// Image Item Template ID
        /// </summary>
        public const string ImageItemTemplateID = "{E669E6DE-8064-4126-BDB7-1A12757A1A90}";
        /// <summary>
        /// Video Item Template ID
        /// </summary>
        public const string VideoItemTemplateID = "{AF5D11D7-F83A-4D16-8747-D92F6F873071}";
        /// <summary>
        /// Video gallery Template ID
        /// </summary>
        public const string VideoGalleryTemplateID = "{19BF6D5D-CAFC-4C5A-927F-0705A5CD4B61}";

        /// <summary>
        /// News Template ID
        /// </summary>
        public const string NewsTemplateID = "{835DCB81-BBC5-4CB7-970D-4BE80A2D39D8}";
        /// <summary>
        /// News Folder Template ID
        /// </summary>
        public const string NewsFolderTemplateID = "{C8761BF7-FA60-4FED-9CE7-55899C8A45D7}";
        /// <summary>
        /// Newslisting page Template id
        /// </summary>
        public const string NewsListingPageTemplateID = "{21FC4198-E485-4AA7-B60E-4CC9E8F33C7B}";

        /// <summary>
        /// Eventslisting page Template id
        /// </summary>
        public const string EventsListingPageTemplateID = "{967D5AE7-1742-4661-9696-3E8E9D9653C5}";

        /// <summary>
        /// Events Template ID
        /// </summary>
        public const string EventsTemplateID = "{AA1DC073-7B89-4945-A520-D5313AC3BC6C}";

        /// <summary>
        /// Download Item Template ID
        /// </summary>
        public const string DownloadItemTemplateID = "{C2001609-2857-447B-BA2A-DDE4F9DFCB9C}";

        /// <summary>
        /// Video Item Template ID
        /// </summary>
        public const string YearFolderTemplateID = "{75D66F86-B219-4070-9820-AB948FD107A4}";
        /// <summary>
        /// Video Album with Filters  Folder Template ID
        /// </summary>
        public const string VideoAlbumWithFiltersYearFolderTemplateID = "{F3F807F7-01D0-419F-B0FD-11A9C4E66A7B}";
        /// <summary>
        /// Image Album Folder Template ID
        /// </summary>
        public const string ImageAlbumYearFolderTemplateID = "{6D64A541-EA75-4422-BE92-02541C818115}";
        /// <summary>
        /// Video Gallery Folder Template ID
        /// </summary>
        public const string VideoGalleryYearFolderTemplateID = "{187707E8-702D-4C9D-A058-D39D62049416}";

        /// <summary>
        /// Video Album Folder Template ID
        /// </summary>
        public const string VideoAlbumYearFolderTemplateID = "{F3F807F7-01D0-419F-B0FD-11A9C4E66A7B}";
        /// <summary>
        /// News Folder Template ID
        /// </summary>
        public const string NewsYearFolderTemplateID = "{C8761BF7-FA60-4FED-9CE7-55899C8A45D7}";
        /// <summary>
        /// Downloads Folder Template ID
        /// </summary>
        public const string DownloadsYearFolderTemplateID = "{0BA80BCD-E9AA-4D18-B97F-B2EF67191A8B}";
        /// <summary>
        /// Events Folder Template ID
        /// </summary>
        public const string EventsYearFolderTemplateID = "{01E451AA-9122-4601-A53F-98557F85FE79}";
        /// <summary>
        /// Home Template ID
        /// </summary>
        public const string SitemapPageTemplateID = "{C814585C-A8DD-415A-BBAF-7412363CCF93}";
        /// <summary>
        /// All Years dictionary key
        /// </summary>
        public const string AllYearsKey = "AllYears";


        /// <summary>
        /// All Categories dictionary key
        /// </summary>
        public const string AllCategoriesKey = "AllCategories";

        /// <summary>
        /// Download dictionary key
        /// </summary>
        public const string DownloadKey = "Download";

        /// <summary>
        /// Download dictionary key
        /// </summary>
        public const string SearchWatermarkText = "SearchWatermarkText";

        /// <summary>
        /// Read Online dictionary key
        /// </summary>
        public const string ReadOnlineKey = "ReadOnline";
        /// <summary>
        /// PastEvents Dictionary Key
        /// </summary>
        public const string PastEvents = "PastEvents";
        /// <summary>
        /// UpcomingEvents dictionary key
        /// </summary>
        public const string UpcomingEvents = "UpcomingEvents";
        /// <summary>
        /// Past Events Key
        /// </summary>
        public const string Past = "Past";
        /// <summary>
        /// Upcoming  Events Key
        /// </summary>
        public const string Upcoming = "Upcoming";
        /// <summary>
        /// Event Type
        /// </summary>
        public const string EventType = "EventType";
        /// <summary>
        /// Sitecore date field for Events template
        /// </summary>
        public const string Date = "date_dt";
        #endregion

        #endregion;

    }

}
