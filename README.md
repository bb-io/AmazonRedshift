
# Blackbird.io Amazon Redshift  
  
Blackbird is the new automation backbone for the language technology industry. Blackbird provides enterprise-scale automation and orchestration with a simple no-code/low-code platform. Blackbird enables ambitious organizations to identify, vet and automate as many processes as possible. Not just localization workflows, but any business and IT process. This repository represents an application that is deployable on Blackbird and usable inside the workflow editor.  
  
## Introduction  
  
<!-- begin docs -->  
  
Amazon Redshift is a fully managed, petabyte-scale data warehouse service in the cloud, designed for high-performance analysis using standard SQL queries. This Amazon Redshift application primarily centers around database management.  

## Before setting up

Before you can connect you need to make sure that:

- You have an [AWS root user account](https://docs.aws.amazon.com/IAM/latest/UserGuide/id_root-user.html) or [IAM user](https://docs.aws.amazon.com/IAM/latest/UserGuide/introduction_identity-management.html) created by root user for you.
- You have an access key and secret generated for your IAM user.
- You have access to models you want to use. You must request access to a model before you can use it. If you try to use the model before you have requested access to it, you receive an error.

### Create IAM user

- Go to [Identity and Access Management](https://console.aws.amazon.com/iamv2/home).
- Select _Users_ from the left sidebar.
- Click _Create user_.
- Enter _User name_ and click _Next_.
- Select _Attach policies directly_ option from _Permissions options_.
- Add _AdministratorAccess_ and _AmazonRedshiftFullAccess_ policies, then click _Next_.
- Review user details and permissions, then click _Create user_.

### Generate access key

- Go to [Identity and Access Management](https://console.aws.amazon.com/iamv2/home).
- Select _Users_ from the left sidebar.
- Select the user for whom you want to generate an access key.
- Go to the _Security credentials_ tab.
- Scroll down to the _Access keys_ section and click _Create access key_.
- Select _Use case_ and click _Next_.
- Enter _Description tag value_ and click _Create access key_.
- Save _Access key_ and _Secret access key_ values to be able to connect to Amazon Redshift via Blackbird.

## Connecting

1. Navigate to apps and search for Amazon Redshift. If you cannot find Amazon Redshift then click _Add App_ in the top right corner, select Amazon Redshift and add the app to your Blackbird environment.
2. Click _Add Connection_.
3. Name your connection for future reference e.g. 'My organization'.
4. Fill in the access key and secret obtained in the previous section.
5. Click _Connect_.
6. Confirm that the connection has appeared and the status is _Connected_.
  
## Actions  
 
### Database

- **Get entries** returns a list of table records based on the provided filters. You can optionally specify limit/offset or _Where_ statement. Where statement is specified as a string, for example `age > 18`.
- **Add entry** adds a new record to the table. Values are specified as an array of strings in the same order as the columns are inside of the table. You can specify less values if the rest of the columns are nullable or have a default value.
- **Update entries** updates table records based on the provided filters. To define the criteria for updating, you can use a _Where_ statement in a manner similar to the _Get Entries_ action. The _Columns_ and _Values_ inputs must be of the same size, aligning with each other based on their respective positions. 

	For instance, if the Columns input is: Name, Age, Gender; and the Values input is: Bob, 13, Non-binary; the table will update the record, setting Bob for Name, 13 for Age, and Non-binary for Gender. Ensure consistency in the size of the Columns and Values inputs for accurate and effective updates.
- **Delete entries** deletes records from the specified table. To define the criteria for updating, you can use a _Where_ statement in a manner similar to the _Get Entries_ action.
- **Query database** executes any raw SQL query and returns the results if there are any.
  
## Feedback  
  
Do you want to use this app or do you have feedback on our implementation? Reach out to us using the [established channels](https://www.blackbird.io/) or create an issue.
  
<!-- end docs -->
