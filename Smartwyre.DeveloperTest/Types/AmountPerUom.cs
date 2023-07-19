using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Types
{
    internal class AmountPerUom : IIncentive
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
            else if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.AmountPerUom))
            {
                result.Success = false;
            }
            else if (rebate.Amount == 0 || request.Volume == 0)
            {
                result.Success = false;
            }
            else
            {
                result.RebateAmount += rebate.Amount * request.Volume;
                result.Success = true;
            }
            return result;
        }
    }
}
