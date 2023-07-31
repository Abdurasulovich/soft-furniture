namespace Soft_furniture.DataAccess.ViewModels.Orders;

public class OrderVM
{
    public long UserId { get; set; }

    public double ProductPrice { get; set; }

    public string DeliverName { get; set; } = String.Empty;

    public string DeliverPhoneNumber { get; set; } = String.Empty;

    public double DeliveryPrice { get; set; }

    public double TotalPrice { get; set; }

    public string Status { get; set; } = String.Empty;

    public string Description { get; set; } = String.Empty;

    public bool IsPaid { get; set; } = false;

    public DateTime ContractedDate { get; set; }
}
