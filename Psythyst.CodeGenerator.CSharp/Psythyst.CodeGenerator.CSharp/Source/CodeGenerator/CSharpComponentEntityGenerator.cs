using System.Collections.Generic;
using System.Linq;

using Psythyst;
using Psythyst.Core.Data;

namespace Psythyst.CodeGenerator.CSharp
{
    /// <summary>
    /// CSharpComponentEntityGenerator Class.
    /// </summary>
    public class CSharpComponentEntityGenerator : IGenerator<ProjectModel, OutputModel>
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
        const string ATTRIBUTE_ARGS_TEMPLATE =
@"${AttributeType} new${AttributeName}";
        const string ATTRIBUTE_ASSIGNMENT_TEMPLATE =
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
        public int Priority { get => 0; }

        public IEnumerable<OutputModel> Generate(ProjectModel ProjectModel)
        {
            return ProjectModel.Component.SelectMany(x => x.Context.Select(z => Generate(z, x)));
        }

        OutputModel Generate(string Context, ProjectComponentModel ProjectComponentModel) {

            var Index = $"{Context}ComponentsLookup.{ProjectComponentModel.Name}";

            var Template = ProjectComponentModel.Attribute.Length == 0
                                      ? FLAG_COMPONENT_TEMPLATE
                                      : STANDARD_COMPONENT_TEMPLATE;

            var TemplatePrefix = ProjectComponentModel.Attribute.Length == 0 ? "is" : "has";

            var Prefix = (string.IsNullOrEmpty(ProjectComponentModel.UniquePrefix)) ? TemplatePrefix : ProjectComponentModel.UniquePrefix;
            
            var FileContent = Template
                .Replace("${ContextName}", Context)
                .Replace("${ComponentType}", $"{ProjectComponentModel.Name}Component")
                .Replace("${ComponentName}", ProjectComponentModel.Name)
                .Replace("${componentName}", ProjectComponentModel.Name.ToLowercaseFirst())
                .Replace("${prefixedName}", $"{Prefix}{ProjectComponentModel.Name}")
                .Replace("${Index}", Index)
                .Replace("${AttributeArgument}", GetAttributeArgument(ProjectComponentModel.Attribute))
                .Replace("${AttributeAssignment}", GetAttributeAssignment(ProjectComponentModel.Attribute));

            return new OutputModel
            (
                $"{Context}/Components/{Context}{ProjectComponentModel.Name}Component.cs",
                FileContent
            );
        }

        string GetAttributeArgument(ProjectComponentAttributeModel[] Attribute) {
            var Argument = Attribute
                .Select(AttributeData => ATTRIBUTE_ARGS_TEMPLATE
                        .Replace("${AttributeType}", AttributeData.Type)
                        .Replace("${AttributeName}", AttributeData.Name.ToUppercaseFirst())
                ).ToArray();

            return string.Join(", ", Argument);
        }

        string GetAttributeAssignment(ProjectComponentAttributeModel[] Attribute) {
            var AttributeAssignment = Attribute
                .Select(AttributeData => ATTRIBUTE_ASSIGNMENT_TEMPLATE
                        .Replace("${AttributeType}", AttributeData.Type)
                        .Replace("${attributeName}", AttributeData.Name)
                        .Replace("${AttributeName}", AttributeData.Name.ToUppercaseFirst())
                ).ToArray();

            return string.Join("\n", AttributeAssignment);
        }
    }
}