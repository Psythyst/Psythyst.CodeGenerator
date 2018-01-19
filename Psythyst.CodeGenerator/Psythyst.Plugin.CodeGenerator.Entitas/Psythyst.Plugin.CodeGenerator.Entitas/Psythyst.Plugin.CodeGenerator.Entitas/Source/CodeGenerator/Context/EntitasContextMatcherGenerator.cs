using System.Collections.Generic;
using System.Linq;

using Psythyst;
using Psythyst.Data.Entitas;

namespace Psythyst.Plugin.CodeGenerator.Entitas
{
    /// <summary>
    /// EntitasContextMatcherGenerator Class.
    /// </summary>
    public class EntitasContextMatcherGenerator : IGenerator<ProjectModel, OutputModel>
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
        public int Priority { get { return 0; } }

        public IEnumerable<OutputModel> Generate(ProjectModel ProjectModel)
        {
            return ProjectModel.Context.Select(Generate);
        }

        public OutputModel Generate(ContextModel ProjectContextModel)
        {
            return new OutputModel
            (
                $"{ProjectContextModel.Name}/{ProjectContextModel.Name}Matcher.cs",
                CONTEXT_MATCHER_TEMPLATE.Replace("${Context}", ProjectContextModel.Name)
            );
        }
    }
}