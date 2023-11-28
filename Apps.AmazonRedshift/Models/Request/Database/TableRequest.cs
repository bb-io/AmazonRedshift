using Apps.AmazonRedshift.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.AmazonRedshift.Models.Request.Database;

public class TableRequest
{
    public string Database { get; set; }
    
    [DataSource(typeof(WorkGroupDataHandler))]
    public string? Workgroup { get; set; }    
    
    [DataSource(typeof(ClusterDataHandler))]
    public string? Cluster { get; set; }
    
    [DataSource(typeof(TableDataHandler))]
    public string Table { get; set; }
}