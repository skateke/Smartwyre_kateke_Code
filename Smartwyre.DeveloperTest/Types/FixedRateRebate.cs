using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Types
{
    internal class FixedRateRebate : IIncentive
    {
        public CalculateRebateResult GetIncentive(CalculateRebateRequest request, Product product, Rebate rebate)
        {
            var result = new CalculateRebateResult();
            if (rebate == null)
            {
                result.Success = false;
            }
            else if (product == null)
            {
                result.Success = false;
            }
            else if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedRateRebate))
            {
                result.Success = false;
            }
            else if (rebate.Percentage == 0 || product.Price == 0 || request.Volume == 0)
            {
                result.Success = false;
            }
            else
            {
                result.RebateAmount += product.Price * rebate.Percentage * request.Volume;
                result.Success = true;
            }
            return result;
        }
    }
}
