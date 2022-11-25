using Azure.Storage.Queues.Models;
using Castle.Core.Logging;
using DurableJobs.Functions.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DurableJobs.Functions
{
    public class Starter
    {
        private readonly ILogger<Starter> _logger;
        public Starter(ILogger<Starter> logger)
        {
            _logger = logger;
        }

        [FunctionName(nameof(Starter))]
        public async Task RuAsync([ServiceBusTrigger("%queuename%", Connection = "servicebus")] SBQueueMessage message,
            [DurableClient] IDurableOrchestrationClient client)
        {
            _logger.LogInformation($"Started Orchestration with Message {JsonConvert.SerializeObject(message)}");
            var instanceId = await client.StartNewAsync(nameof(Orchestrator), message);
            _logger.LogInformation($"Completed orchestration Instance Id {instanceId}");
        }




    }
}
