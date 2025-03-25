using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using webapi_blazor.models.EbayDB;
public class CategoryService
{
    private readonly HttpClient _httpClient;
    public List<Category> categoryList = new List<Category>();
    public CategoryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task GetAllCategory()
    {
        var url = "http://localhost:9999/api/Category/getallcategory";
        var res = await _httpClient.GetFromJsonAsync<List<Category>>(url);
        categoryList = res;
        SetStateHasChange();
    }
    public event Action OnChange;

    public void SetStateHasChange() => OnChange?.Invoke();
}