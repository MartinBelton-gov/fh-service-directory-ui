using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FamilyHubs.ServiceDirectory.Ui.Pages.ProfessionalReferral;

public class ConsentModel : PageModel
{
    [BindProperty]
    public string IsConsentGiven { get; set; } = default!;

    [BindProperty]
    public string Id { get; set; } = default!;
    [BindProperty]
    public string Name { get; set; } = default!;
    public void OnGet(string id, string name)
    {
        Id = id;
        Name = name;
    }

    public IActionResult OnPost(string id, string name)
    {
        if (string.Compare(IsConsentGiven, "yes", StringComparison.OrdinalIgnoreCase) == 0)
        {
            return RedirectToPage("/ProfessionalReferral/PersonalDetails", new
            {
                id = id,
                name = name
            });
        }

        return RedirectToPage("/ProfessionalReferral/ConsentShutter", new
        {
        });

    }
    
}
