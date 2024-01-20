namespace eStore_API;

public class CreateProductDto
{
    public int ProductId { get; set; }
    public int CategoryId { get; set; }
    public string ProductName { get; set; } = null!;
    public float? Weight { get; set; }

    public int UnitPrice { get; set; }
    public int UnitInStock { get; set; }

}
