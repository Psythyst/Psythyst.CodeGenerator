using System.Collections.Generic;
using System.Linq;

using Psythyst;
using Psythyst.Core.Data;

namespace Psythyst.CodeGenerator.CSharp
{
    /// <summary>
    /// CSharpContextMatcherGenerator Class.
    /// </summary>
    public class CSharpContextMatcherGenerator : IGenerator<ProjectModel, OutputModel>
    {
        const string CONTEXT_MATCHER_TEMPLATE =
@"public sealed partial class ${Context}Matcher 
{
    public static Entitas.IAllOfMatcher<${Context}Entity> AllOf(params int[] Index) => Entitas.Matcher<${Context}Entity>.AllOf(Index);
    public static Entitas.IAnyOfMatcher<${Context}Entity> AnyOf(params int[] Index) => Entitas.Matcher<${Context}Entity>.AnyOf(Index);

    public static Entitas.IAllOfMatcher<${Context}Entity> AllOf(params Entitas.IMatcher<${Context}Entity>[] Matcher) => Entitas.Matcher<${Context}Entity>.AllOf(Matcher);
    public static Entitas.IAnyOfMatcher<${Context}Entity> AnyOf(params Entitas.IMatcher<${Context}Entity>[] Matcher) => Entitas.Matcher<${Context}Entity>.AnyOf(Matcher);
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
                $"{ProjectContextModel.Name}/{ProjectContextModel.Name}Matcher.cs",
                CONTEXT_MATCHER_TEMPLATE.Replace("${Context}", ProjectContextModel.Name)
            );
        }
    }
}