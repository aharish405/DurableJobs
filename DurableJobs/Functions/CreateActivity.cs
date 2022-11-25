using DurableJobs.Functions.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DurableJobs.Functions
{
    public class CreateActivity
    {
        [FunctionName("CreateActivity")]
        public async Task Run([ActivityTrigger] DurableFunctionState state)
        {
            Console.WriteLine("Activity triggered");
        }
    }
}
