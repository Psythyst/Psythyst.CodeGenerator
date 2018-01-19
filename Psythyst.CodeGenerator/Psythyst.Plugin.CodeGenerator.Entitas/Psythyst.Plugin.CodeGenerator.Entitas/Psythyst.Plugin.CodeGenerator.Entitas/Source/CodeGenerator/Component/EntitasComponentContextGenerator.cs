using System.Collections.Generic;
using System.Linq;

using Psythyst;
using Psythyst.Data.Entitas;

namespace Psythyst.Plugin.CodeGenerator.Entitas
{
    /// <summary>
    /// EntitasComponentContextGenerator Class.
    /// </summary>
    public class EntitasComponentContextGenerator : IGenerator<ProjectModel, OutputModel>
    {
        const string STANDARD_COMPONENT_TEMPLATE =
@"public partial class ${ContextName}Context {

    public ${ContextName}Entity ${LowerComponentName}Entity { get { return GetGroup(${ContextName}Matcher.${UpperComponentName}).GetSingleEntity(); } }
    public ${ComponentType} ${LowerComponentName} { get { return ${LowerComponentName}Entity.${LowerComponentName}; } }
    public bool has${UpperComponentName} { get { return ${LowerComponentName}Entity != null; } }

    public ${ContextName}Entity Set${UpperComponentName}(${AttributeArgument}) {
        if (has${UpperComponentName}) {
            throw new Entitas.EntitasException(""Could not set ${UpperComponentName}!\n"" + this + "" already has an entity with ${ComponentType}!"",
                ""You should check if the context already has a ${LowerComponentName}Entity before setting it or use context.Replace${ComponentName}()."");
        }
        var entity = CreateEntity();
        entity.Add${UpperComponentName}(${MethodArgument});
        return entity;
    }

    public void Replace${UpperComponentName}(${AttributeArgument}) {
        var entity = ${LowerComponentName}Entity;
        if (entity == null) {
            entity = Set${UpperComponentName}(${MethodArgument});
        } else {
            entity.Replace${UpperComponentName}(${MethodArgument});
        }
    }

    public void Remove${UpperComponentName}() {
        ${LowerComponentName}Entity.Destroy();
    }
}
";
        const string ATTRIBUTE_ARGS_TEMPLATE =
@"${AttributeType} new${AttributeName}";
        const string METHOD_ARGS_TEMPLATE =
@"new${AttributeName}";
        const string FLAG_COMPONENT_TEMPLATE =
@"public partial class ${ContextName}Context {

    public ${ContextName}Entity ${LowerComponentName}Entity { get { return GetGroup(${ContextName}Matcher.${UpperComponentName}).GetSingleEntity(); } }

    public bool ${Prefix}${UpperComponentName} {
        get { return ${LowerComponentName}Entity != null; }
        set {
            var entity = ${LowerComponentName}Entity;
            if (value != (entity != null)) {
                if (value) {
                    CreateEntity().${Prefix}${UpperComponentName} = true;
                } else {
                    entity.Destroy();
                }
            }
        }
    }
}
";
        public int Priority { get { return 0; } }

        public IEnumerable<OutputModel> Generate(ProjectModel ProjectModel)
        {
            return ProjectModel.Component.SelectMany(x => x.Context.Select(z => Generate(z, x)));
        }

        OutputModel Generate(string Context, ComponentModel ProjectComponentModel) {
    
            var Template = ProjectComponentModel.Member.Length == 0
                                      ? FLAG_COMPONENT_TEMPLATE
                                      : STANDARD_COMPONENT_TEMPLATE;

            var TemplatePrefix = ProjectComponentModel.Member.Length == 0 ? "is" : "has";

            var Prefix = (string.IsNullOrEmpty(ProjectComponentModel.UniquePrefix)) ? TemplatePrefix : ProjectComponentModel.UniquePrefix;

            var FileContent = Template
                .Replace("${ContextName}", Context)
                .Replace("${ComponentType}", $"{ProjectComponentModel.Name}Component")
                .Replace("${UpperComponentName}", ProjectComponentModel.Name)
                .Replace("${LowerComponentName}", ProjectComponentModel.Name.ToLowercaseFirst())
                .Replace("${Prefix}", Prefix)
                .Replace("${AttributeArgument}", GetMemberArgument(ProjectComponentModel.Member))
                .Replace("${MethodArgument}", GetMethodArgument(ProjectComponentModel.Member));

            return new OutputModel(
                $"{Context}/Components/{Context}{ProjectComponentModel.Name}Component.cs",
                FileContent
            );
        }

        string GetMemberArgument(ComponentMemberModel[] Attribute) {
            var Argument = Attribute
                .Select(AttributeData => ATTRIBUTE_ARGS_TEMPLATE
                        .Replace("${AttributeType}", AttributeData.Type)
                        .Replace("${AttributeName}", AttributeData.Name.ToUppercaseFirst())
                ).ToArray();

            return string.Join(", ", Argument);
        }

        string GetMethodArgument(ComponentMemberModel[] Attribute) {
            var args = Attribute
                .Select(AttributeData => METHOD_ARGS_TEMPLATE
                    .Replace("${AttributeName}", AttributeData.Name.ToUppercaseFirst()))
                .ToArray();

            return string.Join(", ", args);
        }
    }
}