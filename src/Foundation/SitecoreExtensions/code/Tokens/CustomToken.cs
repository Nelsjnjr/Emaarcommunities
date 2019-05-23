using Sitecore.Pipelines.ExpandInitialFieldValue;

namespace EMAAR.Emaarcommunities.Foundation.SitecoreExtensions.Tokens
{
    /// <summary>
    /// Creating custom token handler for $parenttitle and $parentintro 
    /// </summary>
    public class CustomToken : ExpandInitialFieldValueProcessor
    {
        /// <summary>
        /// This custom token is used to retrieve replace the values in the standardvalues with respective values
        /// </summary>
        /// <param name="args"></param>
        public override void Process(ExpandInitialFieldValueArgs args)
        {
            switch (args.SourceField.Value)
            {
                case var token when token.Contains("$parenttitle"):
                    //Replacing the $parenttitle with its parent item's Title Field
                    args.Result = args.TargetItem.Parent != null ? args.Result.Replace("$parenttitle", args.TargetItem.Parent[CommonConstants.Title]) : string.Empty;
                    break;
                case var token when token.Contains("$parentintro"):
                    //Replacing the $parentintro with its parent item's Introduction Field
                    args.Result = args.TargetItem.Parent != null ? args.Result.Replace("$parentintro", args.TargetItem.Parent[CommonConstants.Introduction]) : string.Empty;
                    break;
                default:
                    break;
            }

        }
    }
}