namespace Apps.AmazonRedshift.Models.Response;

public class RowResponse
{
    public IEnumerable<ValueResponse> Values { get; set; }
}