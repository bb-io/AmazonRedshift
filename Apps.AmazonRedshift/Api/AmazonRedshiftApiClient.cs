using Amazon;
using Amazon.Redshift;
using Amazon.Runtime;
using Apps.AmazonRedshift.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;

namespace Apps.AmazonRedshift.Api;

public class AmazonRedshiftApiClient : AmazonRedshiftClient
{
    public AmazonRedshiftApiClient(AuthenticationCredentialsProvider[] creds) : base(GetAwsCreds(creds), new AmazonRedshiftConfig()
    {
        RegionEndpoint = RegionEndpoint.USWest1,
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