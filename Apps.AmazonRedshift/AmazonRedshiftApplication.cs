using Blackbird.Applications.Sdk.Common;

namespace Apps.AmazonRedshift;

public class AmazonRedshiftApplication : IApplication
{
    public string Name
    {
        get => "Amazon Redshift";
        set { }
    }

    public T GetInstance<T>()
    {
        throw new NotImplementedException();
    }
}