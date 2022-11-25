using DurableJobs.Factory.Models;
using DurableJobs.Functions.Models;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DurableJobs.Factory
{
    internal class CreateOrchestrator : IOrchestrator
    {
        public ActionType actionType => ActionType.New;

        public Task Execute(DurableFunctionState state, IDurableOrchestrationContext context)
        {
            throw new NotImplementedException();
        }
    }
}
