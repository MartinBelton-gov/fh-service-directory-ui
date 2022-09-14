using FamilyHubs.ServiceDirectory.Ui.Services.Api;
using FamilyHubs.ServiceDirectoryCaseManagement.Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FamilyHubs.ServiceDirectory.Ui.Pages.Vcs;

public class ShowReferralDetailsModel : PageModel
{
    private readonly IReferralClientService _referralClientService;

    public ReferralDto Referral { get; set; } = default!;

    public ShowReferralDetailsModel(IReferralClientService referralClientService)
    {
        _referralClientService = referralClientService;
    }
    public async Task OnGet(string id)
    {
        var referral = await _referralClientService.GetReferralById(id);
        if (referral != null)
            Referral = referral;
    }
    
}
