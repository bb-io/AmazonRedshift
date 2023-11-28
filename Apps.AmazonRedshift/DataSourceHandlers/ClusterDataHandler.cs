using Apps.AmazonRedshift.Invocables;
using Apps.AmazonRedshift.Utils;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.AmazonRedshift.DataSourceHandlers;

public class ClusterDataHandler : AmazonRedshiftInvocable, IAsyncDataSourceHandler
{
    public ClusterDataHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var clusterEnumerator =
            Client.Paginators.DescribeClusters(new()).Clusters.GetAsyncEnumerator(cancellationToken);
        var clusters = await Paginator.Paginate(clusterEnumerator);

        return clusters
            .Where(x => context.SearchString is null ||
                        x.ClusterIdentifier.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .ToDictionary(x => x.ClusterIdentifier, x => x.ClusterIdentifier);
    }
}