using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Types
{
    internal interface IIncentive
    {
        CalculateRebateResult GetIncentive(CalculateRebateRequest request, Product product, Rebate rebate);
    }
}
