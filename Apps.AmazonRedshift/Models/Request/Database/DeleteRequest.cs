using Blackbird.Applications.Sdk.Common;

namespace Apps.AmazonRedshift.Models.Request.Database;

public class DeleteRequest
{
    public string? Schema { get; set; }
    
    [Display("Where filter")]
    public string? Where { get; set; }
}