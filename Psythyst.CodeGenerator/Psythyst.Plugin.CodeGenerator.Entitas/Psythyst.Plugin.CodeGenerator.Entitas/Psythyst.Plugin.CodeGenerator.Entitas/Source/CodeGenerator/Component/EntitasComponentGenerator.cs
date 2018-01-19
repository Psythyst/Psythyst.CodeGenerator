using System;
using System.Collections.Generic;
using System.Linq;

using Psythyst;
using Psythyst.Data.Entitas;

namespace Psythyst.Plugin.CodeGenerator.Entitas
{
    /// <summary>
    /// EntitasComponentGenerator Class.
    /// </summary>
    public class EntitasComponentGenerator : IGenerator<ProjectModel, OutputModel>
    {
        const string COMPONENT_TEMPLATE =
@"public sealed partial class ${ComponentName} : Entitas.IComponent {
${AttributeCollection}
}";
        const string MEMBER_DECLARATION_TEMPLATE = @"    public ${AttributeType} ${AttributeName};";

        public int Priority { get { return 0; } }

        public IEnumerable<OutputModel> Generate(ProjectModel ProjectModel)
        {
            return ProjectModel.Component.Select(Generate);
        }

        OutputModel Generate(ComponentModel ProjectComponentModel)
        {
            var FileContent = COMPONENT_TEMPLATE
                .Replace("${ComponentName}", $"{ProjectComponentModel.Name.ToUppercaseFirst()}Component")
                .Replace("${AttributeCollection}", GetMemberCollection(ProjectComponentModel.Member));

            return new OutputModel
            (
                $"Components/{ProjectComponentModel.Name}Component.cs",
                FileContent
            );
        }

        string GetMemberCollection(ComponentMemberModel[] Member) {
            var MemberCollection = Member
                .Select(MemberData => MEMBER_DECLARATION_TEMPLATE
                        .Replace("${AttributeType}", MemberData.Type)
                        .Replace("${AttributeName}", MemberData.Name)
                );

            return String.Join("\n", MemberCollection);
        }
    }
}