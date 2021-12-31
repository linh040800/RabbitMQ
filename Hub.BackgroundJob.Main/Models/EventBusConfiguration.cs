using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hub.BackgroundJob.Main.Models
{
    public class EventBusConfiguration
    {
        public string EventBusConnection { get; set; }
        public int EventBusRetryCount { get; set; }
        public string BrokerName { get; set; }
        public string SubscriptionClientName { get; set; }
        public string EventBusUserName { get; set; }
        public string EventBusPassword { get; set; }
    }
}
