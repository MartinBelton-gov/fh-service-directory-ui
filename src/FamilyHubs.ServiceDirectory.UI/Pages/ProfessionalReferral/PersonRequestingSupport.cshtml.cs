using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FamilyHubs.ServiceDirectory.Ui.Pages.ProfessionalReferral;

public class PersonRequestingSupportModel : PageModel
{
    [BindProperty]
    public string IsTypeOfPerson { get; set; } = default!;

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (string.Compare(IsTypeOfPerson, "professional", StringComparison.OrdinalIgnoreCase) == 0)
        {
            return RedirectToPage("/ProfessionalReferral/SignIn", new
            {

            });
        }

        return RedirectToPage("/ProfessionalReferral/Search", new
        {
        });

        //return RedirectToPage("FindServiceFromPostcode", new
        //{
        //});

    }
}
