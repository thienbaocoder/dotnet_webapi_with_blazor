using webapi_blazor.Models.EbayDB;

public class ProductService
{
    private readonly HttpClient _httpClient;
    public List<ProductListCategory> productList = new List<ProductListCategory>();
    public List<EbayProduct> ebayProductList = new List<EbayProduct>();
    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task GetAllProductListCategory(int pageIndex = 0, int pageSize = 10, string selectedSortOption = "", string category = "")
    {
        var url = $"http://localhost:9999/product/getProductListCategory?pageIndex={pageIndex}&pageSize={pageSize}";

        if (!string.IsNullOrEmpty(selectedSortOption))
            url += $"&sortOption={selectedSortOption}";

        if (!string.IsNullOrEmpty(category))
            url += $"&category={category}";

        productList = await _httpClient.GetFromJsonAsync<List<ProductListCategory>>(url);
        SetStateHasChange();
    }
     public async Task GetAllEbayProducts(int pageIndex = 0, int pageSize = 10, string selectedSortOption = "", string category = "")
    {
        var url = $"http://localhost:9999/product/getProductEbayDB?pageIndex={pageIndex}&pageSize={pageSize}";

        if (!string.IsNullOrEmpty(selectedSortOption))
            url += $"&sortOption={selectedSortOption}";

        if (!string.IsNullOrEmpty(category))
            url += $"&category={category}";

        ebayProductList = await _httpClient.GetFromJsonAsync<List<EbayProduct>>(url);
        SetStateHasChange();
    }


    public event Action OnChange;

    public void SetStateHasChange() => OnChange?.Invoke();
}