namespace EMAAR.Emaarcommunities.Foundation.Search.Models
{
    /// <summary>
    /// Facet class definition
    /// </summary>
    public class Facet
    {
        public string facetField { get; set; }   
        public string allLabel { get; set; }
        public int minCount { get; set; }
    }

}