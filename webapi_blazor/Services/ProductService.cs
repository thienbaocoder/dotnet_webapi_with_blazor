using webapi_blazor.models.EbayDB;

public class ProductService
{
    private readonly HttpClient _httpClient;
    public List<ProductListCategory> productList = new List<ProductListCategory>();
    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task GetAllProductListCategory(int pageIndex = 0, int pageSize = 10)
    {
        var url = $"http://localhost:9999/product/getProductListCategory?pageIndex={pageIndex}&pageSize={pageSize}";
        var res = await _httpClient.GetFromJsonAsync<List<ProductListCategory>>(url);
        productList = res;
        SetStateHasChange();
    }

    public event Action OnChange;

    public void SetStateHasChange() => OnChange?.Invoke();
}