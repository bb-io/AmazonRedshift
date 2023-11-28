namespace Apps.AmazonRedshift.Models.Request.Database;

public class InsertRequest
{
    public string? Schema { get; set; }
    
    public IEnumerable<string> Values { get; set; }
}