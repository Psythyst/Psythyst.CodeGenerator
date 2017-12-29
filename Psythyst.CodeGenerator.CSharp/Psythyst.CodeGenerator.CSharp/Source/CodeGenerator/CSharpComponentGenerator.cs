using System;
using System.Collections.Generic;
using System.Linq;

using Psythyst;
using Psythyst.Core.Data;

namespace Psythyst.CodeGenerator.CSharp
{
    /// <summary>
    /// CSharpComponentGenerator Class.
    /// </summary>
    public class CSharpComponentGenerator : IGenerator<ProjectModel, OutputModel>
    {
        const string COMPONENT_TEMPLATE =
@"public sealed partial class ${ComponentName} : Entitas.IComponent {
${AttributeCollection}
}";
        const string ATTRIBUTE_DECLARATION_TEMPLATE = @"    public ${AttributeType} ${AttributeName};";

        public int Priority { get => 0; }

        public IEnumerable<OutputModel> Generate(ProjectModel ProjectModel)
        {
            return ProjectModel.Component.Select(Generate);
        }

        OutputModel Generate(ProjectComponentModel ProjectComponentModel)
        {
            var FileContent = COMPONENT_TEMPLATE
                .Replace("${ComponentName}", $"{ProjectComponentModel.Name.ToUppercaseFirst()}Component")
                .Replace("${AttributeCollection}", GetAttributeCollection(ProjectComponentModel.Attribute));

            return new OutputModel
            (
                $"Components/{ProjectComponentModel.Name}Component.cs",
                FileContent
            );
        }

        string GetAttributeCollection(ProjectComponentAttributeModel[] Attribute) {
            var AttributeCollection = Attribute
                .Select(AttributeData => ATTRIBUTE_DECLARATION_TEMPLATE
                        .Replace("${AttributeType}", AttributeData.Type)
                        .Replace("${AttributeName}", AttributeData.Name)
                );

            return String.Join("\n", AttributeCollection);
        }
    }
}