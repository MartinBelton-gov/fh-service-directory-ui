using FamilyHubs.ServiceDirectory.Ui.Models;
using FamilyHubs.ServiceDirectory.Ui.Services.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FamilyHubs.ServiceDirectory.Ui.Pages.ProfessionalReferral;

public class SearchModel : PageModel
{
    [BindProperty]
    public string SearchOption { get; set; } = default!;

    [BindProperty]
    public string ServiceName { get; set; } = default!;

    [BindProperty]
    public string Postcode { get; set; } = default!;

    private readonly IPostcodeLocationClientService _postcodeLocationClientService;

    public SearchModel(IPostcodeLocationClientService postcodeLocationClientService)
    {
        _postcodeLocationClientService = postcodeLocationClientService;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        if (SearchOption == null)
            return Page();

        if (string.Compare(SearchOption, "Name", StringComparison.OrdinalIgnoreCase) == 0)
        {
            return RedirectToPage("LocalOfferResults", new
            {
                latitude = 0.0D,
                longitude = 0.0D,
                distance = 0.0D,
                minimumAge = 0,
                maximumAge = 99,
                searchText = ServiceName
            });

        }

        try
        {
            PostcodeApiModel postcodeApiModel = await _postcodeLocationClientService.LookupPostcode(Postcode);

            return RedirectToPage("LocalOfferResults", new
            {
                postcodeApiModel.result.latitude,
                postcodeApiModel.result.longitude,
                distance = 32186.9 //212892.0
            });
        }
        catch
        {
            return Page();
        }

    }
}
