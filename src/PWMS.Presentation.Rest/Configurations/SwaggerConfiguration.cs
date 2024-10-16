namespace PWMS.Presentation.Rest.Configurations;

internal sealed record SwaggerConfigurationSection
{
    public const string SectionName = "Swagger";

    public bool? Enabled { get; set; }

    public bool? AuthorizationEnabled { get; set; }
}
