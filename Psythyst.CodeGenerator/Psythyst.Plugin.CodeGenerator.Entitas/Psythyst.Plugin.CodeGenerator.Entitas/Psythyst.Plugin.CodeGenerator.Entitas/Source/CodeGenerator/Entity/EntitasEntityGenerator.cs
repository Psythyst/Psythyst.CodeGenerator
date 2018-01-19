using System.Collections.Generic;
using System.Linq;

using Psythyst;
using Psythyst.Data.Entitas;

namespace Psythyst.Plugin.CodeGenerator.Entitas
{
    /// <summary>
    /// EntitasEntityGenerator Class.
    /// </summary>
    public class EntitasEntityGenerator : IGenerator<ProjectModel, OutputModel>
    {
        const string ENTITY_TEMPLATE = 
@"public sealed partial class ${Context}Entity : Entitas.Entity { }
";

        public int Priority { get { return 0; } }

        public IEnumerable<OutputModel> Generate(ProjectModel ProjectModel)
        {
            return ProjectModel.Context.Select(Generate);
        }

        OutputModel Generate(ContextModel ProjectContextModel)
        {
            return new OutputModel
            (
                $"{ProjectContextModel.Name}/{ProjectContextModel.Name}Entity.cs",
                ENTITY_TEMPLATE.Replace("${Context}", ProjectContextModel.Name)
            );
        }
    }
}