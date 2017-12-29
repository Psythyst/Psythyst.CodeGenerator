using System.Collections.Generic;
using System.Linq;

using Psythyst;
using Psythyst.Core.Data;

namespace Psythyst.CodeGenerator.CSharp
{
    /// <summary>
    /// CSharpPostConstructorAttributeGenerator Class.
    /// </summary>
    public class CSharpPostConstructorAttributeGenerator : IGenerator<ProjectModel, OutputModel>
    {
        const string ATTRIBUTE_TEMPLATE = 
@"[System.AttributeUsage(System.AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
public sealed class PostConstructorAttribute : System.Attribute
{
    public PostConstructorAttribute() { }
}
";
        public int Priority { get => 0; }
        
        public IEnumerable<OutputModel> Generate(ProjectModel ProjectModel)
        {
            return new List<OutputModel>()
            {
                new OutputModel("PostConstructorAttribute.cs", ATTRIBUTE_TEMPLATE)
            };
        }
    }
}