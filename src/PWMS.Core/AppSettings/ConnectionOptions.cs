using PWMS.Core.SharedKernel;
using System.ComponentModel.DataAnnotations;

namespace PWMS.Core.AppSettings;

public sealed class ConnectionOptions : IAppOptions
{
    static string IAppOptions.ConfigSectionPath => "ConnectionStrings";

    [Required]
    public string Database { get; private init; }
}