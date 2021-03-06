﻿using Nimbus.InfrastructureContracts;
using Pizza.Ordering.Messages;

namespace Pizza.WaitTimeService.Handlers
{
    public class HandleWaitTimeRequests : IHandleRequest<HowLongDoPizzasTakeRequest, HowLongDoPizzasTakeResponse>
    {
        private readonly IWaitTimeCounter _waitTimeCounter;

        public HandleWaitTimeRequests(IWaitTimeCounter waitTimeCounter)
        {
            _waitTimeCounter = waitTimeCounter;
        }

        public HowLongDoPizzasTakeResponse Handle(HowLongDoPizzasTakeRequest request)
        {

            var currentAverage = _waitTimeCounter.GetAveragePizzaTimes();

            return new HowLongDoPizzasTakeResponse {Minutes = currentAverage};
        }
    }
}