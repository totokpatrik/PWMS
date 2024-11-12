namespace PWMS.Web.Blazor.Services.Inbound;

public class OrdersService : IOrdersService
{
    private readonly HttpClient _http;

    public OrdersService(HttpClient http)
    {
        _http = http;
    }


}
