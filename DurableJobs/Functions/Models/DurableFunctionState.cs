using System;
using System.Collections.Generic;
using System.Text;

namespace DurableJobs.Functions.Models
{
    public class DurableFunctionState
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public SBQueueMessage message { get; set; }

    }
}
