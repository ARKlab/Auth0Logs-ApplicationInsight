using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Azure.Messaging;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Auth0LogEventGridFunction.Common;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Auth0toAI.Service;

namespace Auth0EventGridFunction
{
    public class EventTriggerFunction
    {
        private Auth0Service auth0Service;

        public EventTriggerFunction(Auth0Service _auth0Service) 
        {
            auth0Service = _auth0Service;
        }

        [FunctionName("EventTriggerFunction")]
        public void Run(
            Microsoft.Extensions.Logging.ILogger logger,
            [EventGridTrigger] CloudEvent cloudEvent)
        {
            logger.LogInformation("Event received {type} {subject}", cloudEvent.Type, cloudEvent.Subject);

            logger.LogInformation("Event Data : {data}", cloudEvent.Data.ToString());

            var dynamicRecord = JsonConvert.DeserializeObject<JObject>(cloudEvent.Data.ToString())!;

            var logLevelType = MessageTypeHelper.LogConvertion(dynamicRecord["type"].ToString());

            var properties = new Dictionary<string, string>()
            {
                { "_id", dynamicRecord["log_id"].ToString() },
                { "client_id", dynamicRecord["client_id"].ToString() },
                { "client_name", dynamicRecord["client_name"].ToString() },
                { "date", dynamicRecord["date"].ToString() },
                { "ip", dynamicRecord["ip"].ToString() },
                { "log_id", dynamicRecord["log_id"].ToString() },
                { "type", logLevelType.nameLog },
                { "type_code", dynamicRecord["type"].ToString() },
            };

            if(dynamicRecord.ContainsKey("hostname"))
            {
                properties.Add("auth0_domain", dynamicRecord["hostname"].ToString());
            }

            if (dynamicRecord.ContainsKey("audience"))
            {
                properties.Add("audience", dynamicRecord["audience"].ToString());
            }

            if (dynamicRecord.ContainsKey("description"))
            {
                properties.Add("description", dynamicRecord["description"].ToString());
            }

            if (dynamicRecord.ContainsKey("user_agent"))
            {
                properties.Add("user_agent", dynamicRecord["user_agent"].ToString());
            }

            if(dynamicRecord.ContainsKey("scope"))
            {
                properties.Add("scope", dynamicRecord["scope"].ToString());
            }

            if (dynamicRecord.ContainsKey("connection_id"))
            {
                properties.Add("connection_id", dynamicRecord["connection_id"].ToString());
            }

            if (dynamicRecord.ContainsKey("auth0_client"))
            {
                properties.Add("auth0_client", dynamicRecord["auth0_client"].ToString());
            }

            if (dynamicRecord.ContainsKey("details"))
            {
                properties.Add("details", dynamicRecord["details"].ToString());
            }

            if ((int)logLevelType.levelLog >= 3)
            {
                var error = new Exception(dynamicRecord["type"].ToString());
                auth0Service.TrackExceptionToApplicationInsight(error, properties);
            }

            auth0Service.TrackEventToApplicationInsight(logLevelType.nameLog, properties: properties);
        }
    }
}
