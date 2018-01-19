using System.Collections.Generic;
using System.Linq;

using Psythyst;
using Psythyst.Data.Entitas;

namespace Psythyst.Plugin.CodeGenerator.Entitas
{
    /// <summary>
    /// EntitasPostConstructorAttributeGenerator Class.
    /// </summary>
    public class EntitasPostConstructorAttributeGenerator : IGenerator<ProjectModel, OutputModel>
    {
        const string ATTRIBUTE_TEMPLATE = 
@"[System.AttributeUsage(System.AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
public sealed class PostConstructorAttribute : System.Attribute
{
    public PostConstructorAttribute() { }
}
";
        public int Priority { get { return 0; } }

        public IEnumerable<OutputModel> Generate(ProjectModel ProjectModel)
        {
            return new List<OutputModel>()
            {
                new OutputModel("PostConstructorAttribute.cs", ATTRIBUTE_TEMPLATE)
            };
        }
    }
}