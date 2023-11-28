using Blackbird.Applications.Sdk.Common;

namespace Apps.AmazonRedshift.Models.Request.Database;

public class SelectRequest
{
    public string? Schema { get; set; }   
    
    [Display("Where filter")]
    public string? Where { get; set; }
    
    public int? Limit { get; set; }
    
    public int? Offset { get; set; }
}