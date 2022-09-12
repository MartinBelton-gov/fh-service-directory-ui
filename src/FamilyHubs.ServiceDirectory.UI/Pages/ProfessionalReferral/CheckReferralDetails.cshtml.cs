using FamilyHubs.ServiceDirectory.Shared.Models.Api.OpenReferralServices;
using FamilyHubs.ServiceDirectory.Ui.Services.Api;
using FamilyHubs.ServiceDirectoryCaseManagement.Common.Dto;
using FamilyHubs.ServiceDirectoryCaseManagement.Common.MassTransit;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FamilyHubs.ServiceDirectory.Ui.Pages.ProfessionalReferral;

public class CheckReferralDetailsModel : PageModel
{
    [BindProperty]
    public string FullName { get; set; } = default!;

    [BindProperty]
    public string HasSpecialNeeds { get; set; } = default!;

    [BindProperty]
    public string Email { get; set; } = default!;

    [BindProperty]
    public string Phone { get; set; } = default!;

    [BindProperty]
    public string ReasonForSupport { get; set; } = default!;

    [BindProperty]
    public string Id { get; set; } = default!;
    [BindProperty]
    public string Name { get; set; } = default!;

    private readonly IConfiguration _configuration;
    private readonly ILocalOfferClientService _localOfferClientService;
    private readonly IReferralClientService _referralClientService;
    private readonly IPublishEndpoint _publishEndPoint;
    public CheckReferralDetailsModel(IConfiguration configuration, ILocalOfferClientService localOfferClientService, IReferralClientService referralClientService, IPublishEndpoint publishEndPoint)
    {
        _referralClientService = referralClientService;
        _configuration = configuration;
        _localOfferClientService = localOfferClientService;
        _publishEndPoint = publishEndPoint;
    }

    public void OnGet(string id, string name, string fullName, string hasSpecialNeeds, string email, string phone, string reasonForSupport)
    {
        Id = id;
        Name = name;
        FullName = fullName;
        HasSpecialNeeds = hasSpecialNeeds;
        Email = email;
        Phone = phone;
        ReasonForSupport = reasonForSupport;
    }

    public async Task<IActionResult> OnPost()
    {
        // Save to API
        OpenReferralServiceDto openReferralServiceDto = await _localOfferClientService.GetLocalOfferById(Id);

        ReferralDto dto = new(Guid.NewGuid().ToString(),Id,Name, openReferralServiceDto.Description  ?? String.Empty, Newtonsoft.Json.JsonConvert.SerializeObject(openReferralServiceDto), "CurrentUser",FullName,HasSpecialNeeds,Email,Phone,ReasonForSupport, new List<ReferralStatusDto> { new ReferralStatusDto(Guid.NewGuid().ToString(), "Initial-Referral") });

        try
        {
            if (_configuration.GetValue<bool>("UseRabbitMQ"))
            {
                await _publishEndPoint.Publish(new CommandMessage(Guid.NewGuid().ToString(), Newtonsoft.Json.JsonConvert.SerializeObject(dto)));
            }
            else
            {
                await _referralClientService.CreateReferral(dto);
            }
        }
        catch
        {
            return Page();
        }
        

        return RedirectToPage("/ProfessionalReferral/ConfirmReferral", new
        {
            id = Id,
            name = Name,
            fullName = FullName,
            hasSpecialNeeds = HasSpecialNeeds,
            email = Email,
            phone = Phone,
            reasonForSupport = ReasonForSupport
        });
    }
}
