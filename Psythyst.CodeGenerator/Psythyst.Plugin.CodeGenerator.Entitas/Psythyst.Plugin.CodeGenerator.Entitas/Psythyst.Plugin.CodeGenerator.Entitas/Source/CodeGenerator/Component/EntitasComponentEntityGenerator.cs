using System.Collections.Generic;
using System.Linq;

using Psythyst;
using Psythyst.Data.Entitas;

namespace Psythyst.Plugin.CodeGenerator.Entitas
{
    /// <summary>
    /// EntitasComponentEntityGenerator Class.
    /// </summary>
    public class EntitasComponentEntityGenerator : IGenerator<ProjectModel, OutputModel>
    {

    const string STANDARD_COMPONENT_TEMPLATE =
@"public partial class ${ContextName}Entity {

    public ${ComponentType} ${componentName} { get { return (${ComponentType})GetComponent(${Index}); } }
    public bool has${ComponentName} { get { return HasComponent(${Index}); } }

    public void Add${ComponentName}(${AttributeArgument}) {
        var index = ${Index};
        var component = CreateComponent<${ComponentType}>(index);
${AttributeAssignment}
        AddComponent(index, component);
    }

    public void Replace${ComponentName}(${AttributeArgument}) {
        var index = ${Index};
        var component = CreateComponent<${ComponentType}>(index);
${AttributeAssignment}
        ReplaceComponent(index, component);
    }

    public void Remove${ComponentName}() {
        RemoveComponent(${Index});
    }
}
";
        const string MEMBER_ARGS_TEMPLATE =
@"${AttributeType} new${AttributeName}";
        const string MEMBER_ASSIGNMENT_TEMPLATE =
@"        component.${attributeName} = new${AttributeName};";
        const string FLAG_COMPONENT_TEMPLATE =
@"public partial class ${ContextName}Entity {

    static readonly ${ComponentType} ${componentName}Component = new ${ComponentType}();

    public bool ${prefixedName} {
        get { return HasComponent(${Index}); }
        set {
            if (value != ${prefixedName}) {
                var index = ${Index};
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : ${componentName}Component;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
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

            var Index = $"{Context}ComponentsLookup.{ProjectComponentModel.Name}";

            var Template = ProjectComponentModel.Member.Length == 0
                                      ? FLAG_COMPONENT_TEMPLATE
                                      : STANDARD_COMPONENT_TEMPLATE;

            var TemplatePrefix = ProjectComponentModel.Member.Length == 0 ? "is" : "has";

            var Prefix = (string.IsNullOrEmpty(ProjectComponentModel.UniquePrefix)) ? TemplatePrefix : ProjectComponentModel.UniquePrefix;
            
            var FileContent = Template
                .Replace("${ContextName}", Context)
                .Replace("${ComponentType}", $"{ProjectComponentModel.Name}Component")
                .Replace("${ComponentName}", ProjectComponentModel.Name)
                .Replace("${componentName}", ProjectComponentModel.Name.ToLowercaseFirst())
                .Replace("${prefixedName}", $"{Prefix}{ProjectComponentModel.Name}")
                .Replace("${Index}", Index)
                .Replace("${AttributeArgument}", GetMemberArgument(ProjectComponentModel.Member))
                .Replace("${AttributeAssignment}", GetMemberAssignment(ProjectComponentModel.Member));

            return new OutputModel
            (
                $"{Context}/Components/{Context}{ProjectComponentModel.Name}Component.cs",
                FileContent
            );
        }

        string GetMemberArgument(ComponentMemberModel[] Member) {
            var Argument = Member
                .Select(MemberData => MEMBER_ARGS_TEMPLATE
                        .Replace("${AttributeType}", MemberData.Type)
                        .Replace("${AttributeName}", MemberData.Name.ToUppercaseFirst())
                ).ToArray();

            return string.Join(", ", Argument);
        }

        string GetMemberAssignment(ComponentMemberModel[] Member) {
            var MemberAssignment = Member
                .Select(MemberData => MEMBER_ASSIGNMENT_TEMPLATE
                        .Replace("${AttributeType}", MemberData.Type)
                        .Replace("${attributeName}", MemberData.Name)
                        .Replace("${AttributeName}", MemberData.Name.ToUppercaseFirst())
                ).ToArray();

            return string.Join("\n", MemberAssignment);
        }
    }
}