using Amazon;
using Amazon.RedshiftServerless;
using Amazon.Runtime;
using Apps.AmazonRedshift.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;

namespace Apps.AmazonRedshift.Api;

public class AmazonRedshiftServerlessApiClient : AmazonRedshiftServerlessClient
{
    public AmazonRedshiftServerlessApiClient(AuthenticationCredentialsProvider[] creds) : base(GetAwsCreds(creds), new AmazonRedshiftServerlessConfig()
    {
        RegionEndpoint = RegionEndpoint.GetBySystemName(creds.Get(CredsNames.Region).Value),
    })
    {
    }

    private static AWSCredentials GetAwsCreds(AuthenticationCredentialsProvider[] creds)
    {
        var key = creds.Get(CredsNames.AccessKey).Value;
        var secret = creds.Get(CredsNames.AccessSecret).Value;

        return new BasicAWSCredentials(key, secret);
    }
}