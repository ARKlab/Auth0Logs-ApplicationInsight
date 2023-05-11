using Microsoft.Extensions.Logging;
using Auth0toAI.Common;
using Auth0toAI.Service;
using Microsoft.Azure.Functions.Worker;

namespace Auth0toAI
{
    public class EventTriggerFunction
    {
        private Auth0Service _auth0Service;

        public EventTriggerFunction(Auth0Service auth0Service) 
        {
            _auth0Service = auth0Service;
        }

        [Function("EventTriggerFunction")]
        public void Run([EventGridTrigger] MyEventType cloudEvent, FunctionContext context)
        {
            var _logger = context.GetLogger("EventTriggerFunction");

            _logger.LogDebug("Event Data : {data} for cliend {client}", cloudEvent.Data["type"].ToString(), cloudEvent.Data["client_name"].ToString());

            var logLevelType = MessageTypeHelper.LogConvertion(cloudEvent.Data["type"].ToString());

            var properties = new Dictionary<string, string>()
            {
                { "_id", cloudEvent.Data["log_id"].ToString() },
                { "client_id", cloudEvent.Data["client_id"].ToString() },
                { "client_name", cloudEvent.Data["client_name"].ToString() },
                { "date", cloudEvent.Data["date"].ToString() },
                { "ip", cloudEvent.Data["ip"].ToString() },
                { "log_id", cloudEvent.Data["log_id"].ToString() },
                { "type", logLevelType.nameLog },
                { "type_code", cloudEvent.Data["type"].ToString() },
            };

            if(cloudEvent.Data.ContainsKey("hostname"))
            {
                properties.Add("auth0_domain", cloudEvent.Data["hostname"].ToString());
            }

            if (cloudEvent.Data.ContainsKey("audience"))
            {
                properties.Add("audience", cloudEvent.Data["audience"].ToString());
            }

            if (cloudEvent.Data.ContainsKey("description"))
            {
                properties.Add("description", cloudEvent.Data["description"].ToString());
            }

            if (cloudEvent.Data.ContainsKey("user_agent"))
            {
                properties.Add("user_agent", cloudEvent.Data["user_agent"].ToString());
            }

            if(cloudEvent.Data.ContainsKey("scope") && cloudEvent.Data["scope"] != null)
            {
                properties.Add("scope", cloudEvent.Data["scope"].ToString());
            }

            if (cloudEvent.Data.ContainsKey("connection_id"))
            {
                properties.Add("connection_id", cloudEvent.Data["connection_id"].ToString());
            }

            if (cloudEvent.Data.ContainsKey("auth0_client"))
            {
                properties.Add("auth0_client", cloudEvent.Data["auth0_client"].ToString());
            }

            if (cloudEvent.Data.ContainsKey("details"))
            {
                properties.Add("details", cloudEvent.Data["details"].ToString());
            }

            if ((int)logLevelType.levelLog >= 3)
            {
                var error = new Exception(cloudEvent.Data["type"].ToString());
                _auth0Service.TrackExceptionToApplicationInsight(error, properties);
            }

            _auth0Service.TrackEventToApplicationInsight(logLevelType.nameLog, properties: properties);
        }
    }
    public class MyEventType
    {
        public string Id { get; set; }
        public string Topic { get; set; }
        public string Subject { get; set; }
        public string EventType { get; set; }
        public DateTime EventTime { get; set; }
        public IDictionary<string, object> Data { get; set; }
    }
}
