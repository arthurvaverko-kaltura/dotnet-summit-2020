using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KLogMonitor
{
    public class Constants
    {
        // global keys
        public const string ACTION = "kmon_action";
        public const string REQUEST_ID_KEY = "kmon_req_id";
        public const string GROUP_ID = "kmon_group_id";
        public const string CLIENT_TAG = "kmon_client_tag";
        public const string HOST_IP = "kmon_host_ip";
        public const string USER_ID = "kmon_user_id";
        public const string TOPIC = "kmon_topic";
        public const string MULTIREQUEST = "kmon_multirequest";

        // same key as in REST solution  WebAPI.Filters.RequestParser 
        // in-case changing this  - you must change there  as well
        public const string KS = "global_ks";
        
        // key in which the value of muti-thread context data will be saved
        public const string MULTI_THREAD_DATA_KEY = "context_data";

        // event names (for monitor)
        public const string EVENT_API_START = "start";
        public const string EVENT_API_END = "end";
        public const string EVENT_CLIENT_API_START = "start_api";
        public const string EVENT_CLIENT_API_END = "end_api";
        public const string EVENT_DATABASE = "db";
        public const string EVENT_COUCHBASE = "cb";
        public const string EVENT_ELASTIC = "elastic";
        public const string EVENT_RABBITMQ = "rabbit";
        public const string EVENT_SPHINX = "sphinx";
        public const string EVENT_CONNTOOK = "conn";
        public const string EVENT_DUMPFILE = "file";
        public const string EVENT_WS = "ws";
    }
}