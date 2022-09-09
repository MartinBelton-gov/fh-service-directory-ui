using FamilyHubs.ServiceDirectory.Ui.Services;

namespace FamilyHubs.ServiceDirectory.Ui.Models;

public interface IFooterViewModel : ILinkCollection, ILinkHelper
{
    bool UseLegacyStyles { get; }
}
