using FamilyHubs.ServiceDirectory.Shared.Models.Api.OpenReferralServices;
using FamilyHubs.ServiceDirectory.Ui.Services.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FamilyHubs.ServiceDirectory.Ui.Pages.ProfessionalReferral;

public class LocalOfferDetailModel : PageModel
{
    private readonly ILocalOfferClientService _localOfferClientService;

    public OpenReferralServiceDto LocalOffer { get; set; } = default!;

    public LocalOfferDetailModel(ILocalOfferClientService localOfferClientService)
    {
        _localOfferClientService = localOfferClientService;
    }

    public async Task OnGetAsync(string id)
    {
        LocalOffer = await _localOfferClientService.GetLocalOfferById(id);
    }

    public IActionResult OnPost(string id, string name)
    {
        return RedirectToPage("/ProfessionalReferral/ConnectFamilyToServiceStart", new
        {
            id = id,
            name = name
        });

    }
}
