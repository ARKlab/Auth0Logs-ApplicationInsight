using System;
using static Auth0toAI.Common.MessageTypeHelper;

namespace Auth0toAI.Common
{
    public static class MessageTypeHelper
    {
        public enum LogLevel
        {
            Debug_messages,
            Info_messages,
            Errors,
            Critical_errors
        }

        public static string[] LogLevelStr = new string[]
        {
            "Debug messages",
            "Info messages",
            "Errors",
            "Critical errors"
        };

        public static LogType LogConvertion(string logType)
        {
            switch (logType)
            {
                case "s":
                    return new LogType("Success Login", LogLevel.Debug_messages);
                case "slo":
                    return new LogType("Success Logout", LogLevel.Debug_messages);
                case "seccft":
                    return new LogType("Success Exchange", LogLevel.Debug_messages);
                case "seacft":
                    return new LogType("Success Exchange", LogLevel.Debug_messages);
                case "sertft":
                    return new LogType("Success Exchange", LogLevel.Debug_messages);
                case "ssa":
                    return new LogType("Success Silent Auth", LogLevel.Debug_messages);
                case "mgmt_api_read":
                    return new LogType("Management API read Operation", LogLevel.Debug_messages);
                case "feacft":
                    return new LogType("Failed Exchange", LogLevel.Errors);
                case "fercft":
                    return new LogType("Failed Exchange", LogLevel.Errors);
                case "fertft":
                    return new LogType("Failed Exchange", LogLevel.Errors);
                case "f":
                    return new LogType("Failed Login", LogLevel.Errors);
                case "fsa":
                    return new LogType("Failed Silent Auth", LogLevel.Errors);
                case "w":
                    return new LogType("Warnings During Login", LogLevel.Info_messages);
                case "du":
                    return new LogType("Deleted User", LogLevel.Debug_messages);
                case "fu":
                    return new LogType("Failed Login (invalid email/username)", LogLevel.Errors);
                case "fp":
                    return new LogType("Failed Login (wrong password)", LogLevel.Errors);
                case "fc":
                    return new LogType("Failed by Connector", LogLevel.Errors);
                case "fco":
                    return new LogType("Failed by CORS", LogLevel.Errors);
                case "con":
                    return new LogType("Connector Online", LogLevel.Debug_messages);
                case "coff":
                    return new LogType("Connector Offline", LogLevel.Errors);
                case "fcpro":
                    return new LogType("Failed Connector Provisioning", LogLevel.Critical_errors);
                case "ss":
                    return new LogType("Success Signup", LogLevel.Debug_messages);
                case "fs":
                    return new LogType("Failed Signup", LogLevel.Errors);
                case "cs":
                    return new LogType("Code Sent", LogLevel.Debug_messages); // 0
                case "cls":
                    return new LogType("Code/Link Sent", LogLevel.Debug_messages); // 0
                case "sv":
                    return new LogType("Success Verification Email", LogLevel.Debug_messages); // 0
                case "fv":
                    return new LogType("Failed Verification Email", LogLevel.Debug_messages); // 0
                case "scp":
                    return new LogType("Success Change Password", LogLevel.Debug_messages);
                case "fcp":
                    return new LogType("Failed Change Password", LogLevel.Errors);
                case "sce":
                    return new LogType("Success Change Email", LogLevel.Info_messages);
                case "fce":
                    return new LogType("Failed Change Email", LogLevel.Errors);
                case "scu":
                    return new LogType("Success Change Username", LogLevel.Info_messages);
                case "fcu":
                    return new LogType("Failed Change Username", LogLevel.Errors);
                case "scpn":
                    return new LogType("Success Change Phone Number", LogLevel.Info_messages);
                case "fcpn":
                    return new LogType("Failed Change Phone Number", LogLevel.Errors);
                case "svr":
                    return new LogType("Success Verification Email Request", LogLevel.Debug_messages); // 0
                case "fvr":
                    return new LogType("Failed Verification Email Request", LogLevel.Errors);
                case "scpr":
                    return new LogType("Success Change Password Request", LogLevel.Debug_messages); // 0
                case "fcpr":
                    return new LogType("Failed Change Password Request", LogLevel.Errors);
                case "fn":
                    return new LogType("Failed Sending Notification", LogLevel.Errors);
                case "limit_wc":
                    return new LogType("Blocked Account", LogLevel.Critical_errors);
                case "limit_ui":
                    return new LogType("Too Many Calls to /userinfo", LogLevel.Critical_errors);
                case "api_limit":
                    return new LogType("Rate Limit On API", LogLevel.Critical_errors);
                case "sdu":
                    return new LogType("Successful User Deletion", LogLevel.Info_messages);
                case "fdu":
                    return new LogType("Failed User Deletion", LogLevel.Errors);
                default:
                    throw new NotImplementedException("case not implemented.");
            }
        }
    }

    public class LogType
    {
        public LogType(string name, LogLevel level)
        {
            nameLog = name;
            levelLog = level;
        }

        public string nameLog { get; set; }
        public LogLevel levelLog { get; set; }
    }
}
