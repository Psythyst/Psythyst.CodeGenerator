using System.Collections.Generic;
using System.Linq;

using Psythyst;
using Psythyst.Core.Data;

namespace Psythyst.CodeGenerator.CSharp
{
    /// <summary>
    /// CSharpEntityGenerator Class.
    /// </summary>
    public class CSharpEntityGenerator : IGenerator<ProjectModel, OutputModel>
    {
        const string ENTITY_TEMPLATE = 
@"public sealed partial class ${Context}Entity : Entitas.Entity { }
";
        
        public int Priority { get => 0; }
        
        public IEnumerable<OutputModel> Generate(ProjectModel ProjectModel)
        {
            return ProjectModel.Context.Select(Generate);
        }

        OutputModel Generate(ProjectContextModel ProjectContextModel)
        {
            return new OutputModel
            (
                $"{ProjectContextModel.Name}/{ProjectContextModel.Name}Entity.cs",
                ENTITY_TEMPLATE.Replace("${Context}", ProjectContextModel.Name)
            );
        }
    }
}