using Azure.Storage.Queues.Models;
using DurableJobs.Factory;
using DurableJobs.Factory.Models;
using DurableJobs.Functions.Models;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DurableJobs.Functions
{
    public class Orchestrator
    {
        private readonly ILogger<Orchestrator> _logger;
        private readonly IFactory _factory;
        public Orchestrator(ILogger<Orchestrator> logger, IFactory factory)
        {
            _logger = logger;
            _factory = factory;
        }

        public async Task<bool> Run([OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var message = context.GetInput<SBQueueMessage>();
            var functionState = new DurableFunctionState
            {
                message= message,

            };

            await RunOrchestration(context, functionState);
            return true;
        }

        private async Task RunOrchestration(IDurableOrchestrationContext context,DurableFunctionState state)
        {
            var orchestrator = _factory.Create(state.message.action);
            await orchestrator.Execute(state, context);
        }
    }
}
