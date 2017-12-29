using System.Collections.Generic;
using System.Linq;

using Psythyst;
using Psythyst.Core.Data;

namespace Psythyst.CodeGenerator.CSharp
{
    /// <summary>
    /// CSharpContextGenerator Class.
    /// </summary>
    public class CSharpContextGenerator : IGenerator<ProjectModel, OutputModel>
    {
        const string CONTEXT_TEMPLATE =
@"public sealed partial class ${Context}Context : Entitas.Context<${Context}Entity> {
    
    public ${Context}Context(IAERCFactory Factory) : base(${Context}ComponentsLookup.TotalComponents, 0,
            new Entitas.ContextInfo(
                ""${Context}"",
                ${Context}ComponentsLookup.ComponentNameCollection,
                ${Context}ComponentsLookup.ComponentTypeCollection
            ),
            (Entity) => Factory.Create(Entity)) {}
}
";
        public int Priority { get => 0; }

        public IEnumerable<OutputModel> Generate(ProjectModel ProjectModel)
        {
            return ProjectModel.Context.Select(Generate);
        }

        public OutputModel Generate(ProjectContextModel ProjectContextModel)
        {
            return new OutputModel
            (
                $"{ProjectContextModel.Name}/{ProjectContextModel.Name}Context.cs",
                CONTEXT_TEMPLATE.Replace("${Context}", ProjectContextModel.Name)
            );
        }
    }
}