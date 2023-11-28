using Blackbird.Applications.Sdk.Common;

namespace Apps.AmazonRedshift.Models.Request.Database;

public class UpdateRequest
{
    public IEnumerable<string> Columns { get; set; }
    
    public IEnumerable<string> Values { get; set; }
    
    public string? Schema { get; set; }
    
    [Display("Where filter")]
    public string? Where { get; set; }
}