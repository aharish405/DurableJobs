using DurableJobs.Factory.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DurableJobs.Functions.Models
{
    public class SBQueueMessage
    {
        public ActionType action { get; set; }
    }
}
