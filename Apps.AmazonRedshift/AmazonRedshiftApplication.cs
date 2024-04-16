using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Metadata;

namespace Apps.AmazonRedshift;

public class AmazonRedshiftApplication : IApplication, ICategoryProvider
{
    public IEnumerable<ApplicationCategory> Categories
    {
        get => [ApplicationCategory.AmazonApps, ApplicationCategory.DatabaseAndSpreadsheet];
        set { }
    }
    
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