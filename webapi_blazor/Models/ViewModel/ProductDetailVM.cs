
public class ProductDetailVM
{
    public int Id { get; set; }

    public string Name { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public string ListImage { get; set; } = "[]";
    // public List<string> ImgList { get; set; } = new List<string>();
    public ProductDetailVM()
    {

    }
}

public class ProductDetailResultVM
{
    public int Id { get; set; }

    public string Name { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<ImageUrlVM> ListImage { get; set; } = new List<ImageUrlVM>();
}

public class ImageUrlVM
{
    public string ImageUrl { get; set; }    
}