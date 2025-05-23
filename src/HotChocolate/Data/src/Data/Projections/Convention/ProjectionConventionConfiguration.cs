using HotChocolate.Types;

namespace HotChocolate.Data.Projections;

public class ProjectionConventionConfiguration : IHasScope
{
    public string? Scope { get; set; }

    public Type? Provider { get; set; }

    public IProjectionProvider? ProviderInstance { get; set; }

    public List<IProjectionProviderExtension> ProviderExtensions { get; } = [];

    public List<Type> ProviderExtensionsTypes { get; } = [];
}
