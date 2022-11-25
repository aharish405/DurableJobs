using DurableJobs.Factory.Models;
using DurableJobs.Functions.Models;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DurableJobs.Factory
{
    public interface IOrchestrator
    {
        ActionType actionType { get; }
        Task Execute(DurableFunctionState state, IDurableOrchestrationContext context);
    }
}
