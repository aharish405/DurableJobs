using Castle.Core.Logging;
using DurableJobs.Factory.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DurableJobs.Factory
{
    public class Factory : IFactory
    {
        private readonly IEnumerable<IOrchestrator> _orcImplementations;
        private readonly ILogger<Factory> _logger;

        public Factory(IEnumerable<IOrchestrator> orcImplementations,ILogger<Factory> logger)
        {
            _orcImplementations = orcImplementations;
            _logger = logger;
        }

        public IOrchestrator Create(ActionType actionType)
        {
            var orchestrator=_orcImplementations.FirstOrDefault(x=>x.actionType==actionType);
            if (orchestrator==null)
            {
                throw new ApplicationException($" Action {actionType} is not allowed");
            }
            _logger.LogInformation($"Returning {actionType} orchestrator");
            return orchestrator;
        }
    }
}
