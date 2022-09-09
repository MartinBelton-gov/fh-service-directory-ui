using FamilyHubs.ServiceDirectory.Ui.Models;
using FamilyHubs.ServiceDirectory.Ui.Services.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FamilyHubs.ServiceDirectory.Ui.Pages;

public class FindServiceFromPostcodeModel : PageModel
{
    private readonly IPostcodeLocationClientService _postcodeLocationClientService;
    public string Postcode { get; set; } = default!;

    public FindServiceFromPostcodeModel(IPostcodeLocationClientService postcodeLocationClientService)
    {
        _postcodeLocationClientService = postcodeLocationClientService;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        var postCode = Request.Form["Postcode"];

        if (string.IsNullOrEmpty(postCode))
        {
            return new RedirectToPageResult("/FindServiceFromPostcode");
        }

        PostcodeApiModel postcodeApiModel = await _postcodeLocationClientService.LookupPostcode(postCode);


        return RedirectToPage("LocalOfferResults", new
        {
            postcodeApiModel.result.latitude,
            postcodeApiModel.result.longitude,
            distance = 32186.9 //212892.0
        });
    }
}
