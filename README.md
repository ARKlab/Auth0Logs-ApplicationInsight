# Auth0Logs-ApplicationInsight
This project is containing an azure function, written in .Net C# and the related pipeline.yml and deploy.json to create pipeline in DevOps and deploy the needed infrastructure in Azure environment. The azure function is usefull to receive **Event Grid Events** from **Auth0** (once the Event Grid it is created), and send the events to an **ApplicationInsight**.

# Create Event Grid resources on Azure:
To create **Event Grid Partner Configuration** and **Event Grid Partner Topic** with **Auth0** subscription, follow the steps in the Microsoft Guide link below:
https://learn.microsoft.com/en-us/azure/event-grid/auth0-how-to#authorize-partner-to-create-a-partner-topic
- Register the Event Grid resource provider
- Authorize partner to create a partner topic
- Set up an Auth0 partner topic
- Activate a partner topic

# Step to use the project:
Download this project where are included 3 main components:
- Infrastructure\Pipeline.yml: It is used to create Azure infrastructure pipeline in your DevOps environment. Need to be imported to the Azure DevOps and set the following variables in the Library with variable group name **Auth0ToApplicationInsight**:

  | Variable Name | Value |
  | ------------- | ----- |
  | subscriptionId | Subscription Id |
  | resourceGroup | Resource group name |
  | location | Location of app service |
  | auth0LogConnectionString | The ApplicacionInsight for Auth0 logs connection string |
  | uniquePrefixName | Unique Prefix Name |
  | eventGridSubscriptionName | The EventGridSubscriptonName in Azure |
  | eventGridTopicName | The EventGridTopicName in Azure |
  | functionName | The Azure Function name |
  | sharedLawId | The WorkspaceResourceId for the ApplicationInsight |
Set **azureResurceManagerConnection** variable in the pipeline variable (not in the variable resource group)
  | Variable Name | Value |
  | ------------- | ----- |
  | azureResurceManagerConnection | Service connection name |

- Infrastructure\ARM\Deploy.json: It is the ARM template used to deploy all the resources in the Azure Portal, Application Service Plan, Function App (for the Azure Function), Application Insight to deploy custom events.
- Auth0LogEventGridFunction project it is the Function to be deployed in the **Azure Function** (Function App) that will be the **Event Grid Event Handler** subscribed to the **Event Grid Events** that will manage the Events from Auth0 and send them to **Application Insight**.
- Services\Auth0toAI\Pipeline.yml: This pipeline is building the azure function and deploying it in the Azure function app created with Infrastructure\ARM\Deploy.json.
- Service\Auth0toAI\ARM\Deploy.json: This Deploy.json is creating the **Event Subscription** in the **Event Grid Partner Topic** and subscribing the Azure Function previously deployed, to it.