using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Runtime.InteropServices;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService
{
    private readonly IRebateDataStore rebateDataStore;
    private readonly IProductDataStore productDataStore;

    public RebateService(IRebateDataStore rebateDataStore, IProductDataStore productDataStore)
    {
        this.rebateDataStore = rebateDataStore;
        this.productDataStore = productDataStore;
    }

    public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {
        Rebate rebate = rebateDataStore.GetRebate(request.RebateIdentifier);
        Product product = productDataStore.GetProduct(request.ProductIdentifier);


        var incentive = GetIncentiveObject(rebate.Incentive);
        var result = incentive.GetIncentive(request, product, rebate);


        if (result.Success)
        {
            var storeRebateDataStore = new RebateDataStore();
            storeRebateDataStore.StoreCalculationResult(rebate, result.RebateAmount);
        }

        return result;
    }

    private IIncentive GetIncentiveObject(IncentiveType incentiveType)
    {
        Type t = Type.GetType("Smartwyre.DeveloperTest.Types." + incentiveType.ToString());
        var incentive = (IIncentive)Activator.CreateInstance(t);
        return incentive;
    }
}
