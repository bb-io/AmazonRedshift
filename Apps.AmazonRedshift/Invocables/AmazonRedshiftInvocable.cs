using Apps.AmazonRedshift.Api;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.AmazonRedshift.Invocables;

public class AmazonRedshiftInvocable : BaseInvocable
{
    protected AuthenticationCredentialsProvider[] Creds =>
        InvocationContext.AuthenticationCredentialsProviders.ToArray();

    protected AmazonRedshiftApiClient Client { get; }
    protected AmazonRedshiftDataApiClient DataClient { get; }
    protected AmazonRedshiftServerlessApiClient ServerlessClient { get; }

    public AmazonRedshiftInvocable(InvocationContext invocationContext) : base(invocationContext)
    {
        Client = new(Creds);
        DataClient = new(Creds);
        ServerlessClient = new(Creds);
    }
}