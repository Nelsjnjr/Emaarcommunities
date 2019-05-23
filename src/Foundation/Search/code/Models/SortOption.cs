namespace EMAAR.Emaarcommunities.Foundation.Search.Models
{
    /// <summary>
    /// SortOption class definition.
    /// </summary>
    public class SortOption
    {
        #region properties
        public string SortFieldName { get; set; }
        public SortOrder SortOrder { get; set; }
        #endregion
    }

    /// <summary>
    /// SortOrder Enum definition.
    /// </summary>
    public enum SortOrder
    {
        Ascending,
        Descending
    }
}