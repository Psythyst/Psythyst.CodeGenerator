using System.Collections.Generic;
using System.Linq;

using Psythyst;
using Psythyst.Core.Data;

namespace Psythyst.CodeGenerator.CSharp
{
    /// <summary>
    /// CSharpComponentMatcherGenerator Class.
    /// </summary>
    public class CSharpComponentMatcherGenerator : IGenerator<ProjectModel, OutputModel>
    {
        const string STANDARD_COMPONENT_TEMPLATE =
@"public sealed partial class ${ContextName}Matcher {

    static Entitas.IMatcher<${ContextName}Entity> _matcher${ComponentName};

    public static Entitas.IMatcher<${ContextName}Entity> ${ComponentName} {
        get {
            if (_matcher${ComponentName} == null) {
                var matcher = (Entitas.Matcher<${ContextName}Entity>)Entitas.Matcher<${ContextName}Entity>.AllOf(${Index});
                matcher.componentNames = ${ComponentNameCollection};
                _matcher${ComponentName} = matcher;
            }

            return _matcher${ComponentName};
        }
    }
}
";
        public int Priority { get => 0; }

        public IEnumerable<OutputModel> Generate(ProjectModel ProjectModel)
        {
            return ProjectModel.Component.SelectMany(x => x.Context.Select(z => Generate(z, x)));
        }

        OutputModel Generate(string Context, ProjectComponentModel ProjectComponentModel) {

            var Index = $"{Context}ComponentsLookup.{ProjectComponentModel.Name}";

            var ComponentNameCollection = $"{Context}ComponentsLookup.ComponentNameCollection";

            var FileContent = STANDARD_COMPONENT_TEMPLATE
                .Replace("${ContextName}", Context)
                .Replace("${ComponentName}", ProjectComponentModel.Name)
                .Replace("${Index}", Index)
                .Replace("${ComponentNameCollection}", ComponentNameCollection);

            return new OutputModel(
                $"{Context}/Components/{Context}{ProjectComponentModel.Name}Component.cs",
                FileContent
            );
        }
    }
}