using System.Collections.Generic;

namespace EMAAR.Emaarcommunities.Foundation.Search.Models
{
    /// <summary>
    /// Search results generic class definition.
    /// </summary>
    public class SearchResultsGeneric<T>
    {
        #region properties
        
        public List<Filters> filters { get; set; }
        public Results<T> results { get; set; }
        #endregion
    }

    /// <summary>
    /// Results class definition.
    /// </summary>
    public class Results<T>
    {
        #region properties
        public int Totalcount { get; set; }
        public List<T> results { get; set; }
        #endregion
    }

    /// <summary>
    /// Filters class definition.
    /// </summary>
    public class Filters
    {
        #region properties
        public string filterLabel { get; set; }
        public List<FilterValues> filterValues { get; set; }
        #endregion 
    }

    /// <summary>
    /// Filter Values class definition.
    /// </summary>
    public class FilterValues
    {
        #region properties
        public string label { get; set; }
        public string id { get; set; }
        #endregion
    }
}