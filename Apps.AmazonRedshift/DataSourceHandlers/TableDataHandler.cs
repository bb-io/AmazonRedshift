using Apps.AmazonRedshift.Invocables;
using Apps.AmazonRedshift.Models.Request.Database;
using Apps.AmazonRedshift.Utils;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.AmazonRedshift.DataSourceHandlers;

public class TableDataHandler : AmazonRedshiftInvocable, IAsyncDataSourceHandler
{
    private TableRequest TableRequest { get; }

    public TableDataHandler(InvocationContext invocationContext, [ActionParameter] TableRequest request) : base(
        invocationContext)
    {
        TableRequest = request;
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(TableRequest.Database))
            throw new("You should input database first");

        if (string.IsNullOrWhiteSpace(TableRequest.Workgroup) && string.IsNullOrWhiteSpace(TableRequest.Cluster))
            throw new("You should input workgroup or cluster first");

        var clusterEnumerator =
            DataClient.Paginators.ListTables(new()
            {
                Database = TableRequest.Database,
                WorkgroupName = TableRequest.Workgroup,
                ClusterIdentifier = TableRequest.Cluster
            }).Tables.GetAsyncEnumerator(cancellationToken);
        var clusters = await Paginator.Paginate(clusterEnumerator);

        return clusters
            .Where(x => context.SearchString is null ||
                        x.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .ToDictionary(x => x.Name, x => x.Name);
    }
}