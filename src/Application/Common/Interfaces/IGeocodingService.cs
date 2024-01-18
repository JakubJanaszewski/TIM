namespace Blog.Application.Common.Interfaces;
public interface IGeocodingService
{
    Task<string> GetAddressAsync(double latitude, double longitude);
}
