using Newtonsoft.Json;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using System;
using System.Collections.Generic;

namespace EMAAR.Emaarcommunities.Foundation.Search.Models
{
    /// <summary>
    /// ListingSearchResultItem class definition
    /// </summary>
    [Serializable]
    public class ListingSearchResultItem : SearchResultItem
    {
        [JsonProperty]
        [IndexField("title")]
        public string title { get; set; }

        [JsonProperty]
        [IndexField("introduction")]
        public string description { get; set; }

        [JsonProperty]
        [IndexField("customdate")]
        public string date { get; set; }

        [JsonProperty]
        [IndexField("custommonth")]
        public string month { get; set; }

        [JsonProperty]
        [IndexField("pageurl")]
        public string pageUrl { get; set; }

        [JsonProperty]
        [IndexField("imageurl")]
        public string imageUrl { get; set; }

        [JsonProperty]
        [IndexField("thumbnailurl")]
        public string thumbnailurl { get; set; }

        [JsonProperty]
        [IndexField("imagealttext")]
        public string imageAlttext { get; set; }

        [JsonProperty]
        [IndexField("externalurl")]
        public string externalURL { get; set; }

 
        [IndexField("Value")]
        public string value { get; set; }

        
        [IndexField("images_sm")]
        public List<string> images { get; set; }

        [IndexField("videos_sm")]
        public List<string> videos { get; set; }
        [IndexField("date_dt")]
        public DateTime date_dt { get; set; }

        [JsonProperty]
        [IndexField("content")]
        public string content { get; set; }

        [JsonProperty]
        [IndexField("renderingscontent")]
        public string renderingsContent { get; set; }
        [JsonProperty]
        [IndexField("customsitefieldname")]
        public string customsitefieldname { get; set; }
    }
}