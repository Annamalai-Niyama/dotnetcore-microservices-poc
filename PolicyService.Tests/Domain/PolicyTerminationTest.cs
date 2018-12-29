using System;
using PolicyService.Domain;
using Xunit;
using static Xunit.Assert;

namespace PolicyService.Tests.Domain
{
    public class PolicyTerminationTest
    {
        [Fact]
        public void CanTerminateActivePolicyInTheMiddleOfCoverPeriod()
        {
            var offer = OfferFactory.NewOfferValidUntil(DateTime.Now.AddDays(5));

            var policy = offer.Buy(PolicyHolderFactory.Abc());

            var terminationResult = policy.Terminate(DateTime.Now.AddDays(3));
            
            Equal(PolicyStatus.Terminated, policy.Status);     
            Equal(180M, terminationResult.TerminalVersion.TotalPremiumAmount);
            Equal(120M, terminationResult.AmountToReturn);

        }
    }
}