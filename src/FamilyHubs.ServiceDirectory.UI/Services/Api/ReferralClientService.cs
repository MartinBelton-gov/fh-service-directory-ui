using FamilyHubs.ServiceDirectoryCaseManagement.Common.Dto;
using FamilyHubs.SharedKernel;
using System.Text;
using System.Text.Json;

namespace FamilyHubs.ServiceDirectory.Ui.Services.Api;

public interface IReferralClientService
{
    Task<PaginatedList<ReferralDto>> GetReferralsByReferrer(string referrer, int pageNumber, int pageSize);
    Task<string> CreateReferral(ReferralDto referralDto);
}

public class ReferralClientService : ApiService, IReferralClientService
{
    public ReferralClientService(HttpClient client)
        : base(client)
    {
        client.BaseAddress = new Uri("http://localhost:7282");
    }

    public async Task<PaginatedList<ReferralDto>> GetReferralsByReferrer(string referrer, int pageNumber, int pageSize)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(_client.BaseAddress + $"api/referrals/{referrer}?pageNumber={pageNumber}&pageSize={pageSize}"),
        };

        using var response = await _client.SendAsync(request);

        response.EnsureSuccessStatusCode();

        return await JsonSerializer.DeserializeAsync<PaginatedList<ReferralDto>>(await response.Content.ReadAsStreamAsync(), options: new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new PaginatedList<ReferralDto>();
    }

    public async Task<string> CreateReferral(ReferralDto referralDto)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(_client.BaseAddress + "api/referrals"),
            Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(referralDto), Encoding.UTF8, "application/json"),
        };

        using var response = await _client.SendAsync(request);

        response.EnsureSuccessStatusCode();

        var stringResult = await response.Content.ReadAsStringAsync();
        return stringResult;
    }
}

