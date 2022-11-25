using DurableJobs.Factory.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DurableJobs.Factory
{
    public interface IFactory
    {
        IOrchestrator Create(ActionType actionType);
    }
}
