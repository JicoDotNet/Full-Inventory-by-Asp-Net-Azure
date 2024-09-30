namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IOrderDetails
    {
        string IBProductId { get; set; }
        bool IsPurchase { get; set; }
        string OrderNo { get; set; }
        int Users { get; set; }
        string PlanId { get; set; }
        string PlanDetails { get; set; }
        string PlanCostId { get; set; }
        string PlanCostDetails { get; set; }
        bool IsFreePlan { get; set; }
        double PlanCostUnitPrice { get; set; }
        string CouponCode { get; set; }
        double DiscountPercentage { get; set; }
        double OrderValue { get; set; }
        double TaxPercentage { get; set; }
        double TaxValue { get; set; }
        double Total { get; set; }
    }
}