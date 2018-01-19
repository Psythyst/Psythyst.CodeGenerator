using System;
using System.Collections.Generic;
using System.Linq;

using Psythyst;
using Psythyst.Data.Entitas;

namespace Psythyst.Plugin.CodeGenerator.Entitas
{
    /// <summary>
    /// EntitasContextPostConstructorGenerator Class.
    /// </summary>
    public class EntitasContextPostConstructorGenerator : IGenerator<ProjectModel, OutputModel>
    {
        public int Priority { get { return 0; } }

        const string CONTEXT_POSTCONSTRUCTOR_TEMPLATE =
@"public sealed partial class ${UpperContextName}Context {

    public void ExecutePostConstructor() { 
        var PostConstructorCollection = System.Linq.Enumerable.Where(
            GetType().GetMethods(),
            Method => System.Attribute.IsDefined(Method, typeof(PostConstructorAttribute))
        );

        foreach (var PostConstructor in PostConstructorCollection)
            PostConstructor.Invoke(this, null);
    }
}
";
        public IEnumerable<OutputModel> Generate(ProjectModel ProjectModel)
        {
            return ProjectModel.Context.Select(x => Generate(x));
        }

        OutputModel Generate(ContextModel ProjectContextModel)
        {
            return new OutputModel
            (
                $"{ProjectContextModel.Name}/{ProjectContextModel.Name}Context.cs",
                GetPostConstructor(ProjectContextModel.Name)
            );
        }

        string GetPostConstructor(String Context) {
            return CONTEXT_POSTCONSTRUCTOR_TEMPLATE
                .Replace("${UpperContextName}", Context.ToUppercaseFirst());
        }
    }
}