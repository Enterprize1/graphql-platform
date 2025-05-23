using HotChocolate.Language;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using HotChocolate.Types.Descriptors.Definitions;
using DirectiveLocation = HotChocolate.Types.DirectiveLocation;

namespace HotChocolate.Authorization;

internal sealed class AllowAnonymousDirectiveType
    : DirectiveType
    , ISchemaDirective
{
    public AllowAnonymousDirectiveType()
    {
        Name = Names.AllowAnonymous;
    }

    protected override void Configure(IDirectiveTypeDescriptor descriptor)
    {
        descriptor
            .Name(Names.AllowAnonymous)
            .Location(DirectiveLocation.FieldDefinition)
            .Repeatable()
            .Internal();
    }

    public void ApplyConfiguration(
        IDescriptorContext context,
        DirectiveNode directiveNode,
        ITypeSystemConfiguration definition,
        Stack<ITypeSystemConfiguration> path)
    {
        ((IDirectiveConfigurationProvider)definition).Directives.Add(new(directiveNode));

        if (definition is ObjectFieldConfiguration fieldDef)
        {
            fieldDef.ContextData[WellKnownContextData.AllowAnonymous] = true;
        }
    }

    public static class Names
    {
        public const string AllowAnonymous = "allowAnonymous";
    }
}
