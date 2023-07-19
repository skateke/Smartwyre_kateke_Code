using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Types
{
    internal class FixedCashAmount : IIncentive
    {
        public CalculateRebateResult GetIncentive(CalculateRebateRequest request, Product product, Rebate rebate)
        {
            var result = new CalculateRebateResult();
            if (rebate == null)
            {
                result.Success = false;
            }
            else if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount))
            {
                result.Success = false;
            }
            else if (rebate.Amount == 0)
            {
                result.Success = false;
            }
            else
            {
                result.RebateAmount = rebate.Amount;
                result.Success = true;
            }
            return result;
        }
    }
}
