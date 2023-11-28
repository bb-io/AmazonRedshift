using Blackbird.Applications.Sdk.Common;

namespace Apps.AmazonRedshift.Models.Request.Database;

public class QueryDatabaseRequest
{
    [Display("SQL query")]
    public string Sql { get; set; }
}