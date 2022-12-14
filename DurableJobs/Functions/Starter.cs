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
using System.Threading;
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

        //[FunctionName(nameof(Starter))]
        //public async Task RuAsync([ServiceBusTrigger("test-queue", Connection = "servicebus")] SBQueueMessage message,
        //    [DurableClient] IDurableOrchestrationClient client)
        //{
        //    _logger.LogInformation($"Started Orchestration with Message {JsonConvert.SerializeObject(message)}");
        //    var instanceId = await client.StartNewAsync(nameof(Orchestrator), message);
        //    _logger.LogInformation($"Completed orchestration Instance Id {instanceId}");
        //}

        [FunctionName(nameof(Starter))]
        public async Task TimerTriggerOperation([TimerTrigger("0 */5 * * * *", RunOnStartup = true)] TimerInfo timerInfo,
            CancellationToken cancellationToken, [DurableClient] IDurableOrchestrationClient client)
        {
            try
            {
                var message = new SBQueueMessage();
                await Task.Delay(100, cancellationToken);
                string scheduleStatus = string.Format("Status: Last='{0}', Next='{1}', IsPastDue={2}",
                timerInfo.ScheduleStatus.Last, timerInfo.ScheduleStatus.Next, timerInfo.IsPastDue);
                _logger.LogInformation(scheduleStatus);

                _logger.LogInformation($"Started Orchestration with Message {JsonConvert.SerializeObject(message)}");
                var instanceId = await client.StartNewAsync(nameof(Orchestrator), message);
                _logger.LogInformation($"Completed orchestration Instance Id {instanceId}");
            }
            catch (Exception exception)
            {
                _logger.LogError("Error with sync message{0} stack trace{1} ", exception.Message, exception.StackTrace);
            }
        }




    }
}
