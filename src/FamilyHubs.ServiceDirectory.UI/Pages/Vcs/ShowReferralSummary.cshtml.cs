using FamilyHubs.ServiceDirectory.Ui.Services.Api;
using FamilyHubs.ServiceDirectoryCaseManagement.Common.Dto;
using FamilyHubs.SharedKernel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FamilyHubs.ServiceDirectory.Ui.Pages.Vcs;

public class ShowReferralSummaryModel : PageModel
{
    private readonly IReferralClientService _referralClientService;

    public PaginatedList<ReferralDto> ReferralList { get; set; } = default!;

    public ShowReferralSummaryModel(IReferralClientService referralClientService)
    {
        _referralClientService = referralClientService;
    }
    public async Task OnGet()
    {
        ReferralList = await _referralClientService.GetReferralsByOrganisationId("ba1cca90-b02a-4a0b-afa0-d8aed1083c0d", 1, 99);
    }
}
