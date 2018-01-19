using System;
using System.Collections.Generic;
using System.Linq;

using Psythyst;
using Psythyst.Data.Entitas;

namespace Psythyst.Plugin.CodeGenerator.Entitas
{
    /// <summary>
    /// EntitasComponentLookupGenerator Class.
    /// </summary>
    public class EntitasComponentLookupGenerator : IGenerator<ProjectModel, OutputModel>
    {
        
     const string COMPONENT_LOOKUP_TEMPLATE =
@"public static class ${Lookup} {

${TotalComponentCountConstant}

${ComponentConstantCollection}

    public static readonly string[] ComponentNameCollection = {
${ComponentNameCollection}
    };

    public static readonly System.Type[] ComponentTypeCollection = {
${ComponentTypeCollection}
    };
}
";
        const string COMPONENT_CONSTANT_TEMPLATE =
@"    public const int ${ComponentName} = ${Index};";
        const string TOTAL_COMPONENT_CONSTANT_TEMPLATE =
@"    public const int TotalComponents = ${TotalComponentCount};";
        const string COMPONENT_NAME_TEMPLATE =
@"        ""${ComponentName}""";
        const string COMPONENT_TYPE_TEMPLATE =
@"        typeof(${ComponentType})";

        public int Priority { get { return 0; } }

        public IEnumerable<OutputModel> Generate(ProjectModel ProjectModel)
        {
            var _Cache = new Dictionary<String, List<ComponentModel>>();

            foreach (var _Component in ProjectModel.Component){
                foreach(var _Context in _Component.Context) {
                    if (!_Cache.ContainsKey(_Context))
                        _Cache.Add(_Context, new List<ComponentModel>());
                        _Cache[_Context].Add(_Component);
                }
            }

            return _Cache.Select(x => Generate(x.Key, x.Value, x.Value.Count));
        }

        OutputModel Generate(string Context, IEnumerable<ComponentModel> ProjectComponentModel, int Length)
        {
            var ComponentConstant = string.Join("\n",
                ProjectComponentModel.Select ((Component, Index) => {
                    return COMPONENT_CONSTANT_TEMPLATE
                        .Replace("${ComponentName}", Component.Name)
                        .Replace("${Index}", Index.ToString());               
                }).ToArray()
            );

            var TotalComponentConstant = TOTAL_COMPONENT_CONSTANT_TEMPLATE
                    .Replace("${TotalComponentCount}", Length.ToString());

            var ComponentName = string.Join(",\n", 
                    ProjectComponentModel.Select(Component => COMPONENT_NAME_TEMPLATE
                        .Replace("${ComponentName}", Component.Name)).ToArray());

            var ComponentType = string.Join(",\n", 
                    ProjectComponentModel.Select(Component => COMPONENT_TYPE_TEMPLATE
                        .Replace("${ComponentType}", $"{Component.Name}Component")).ToArray());

            var FileContent = COMPONENT_LOOKUP_TEMPLATE
                    .Replace("${Lookup}", $"{Context}ComponentsLookup")
                    .Replace("${ComponentConstantCollection}", ComponentConstant)
                    .Replace("${TotalComponentCountConstant}", TotalComponentConstant)
                    .Replace("${ComponentNameCollection}", ComponentName)
                    .Replace("${ComponentTypeCollection}", ComponentType);

            return new OutputModel(
                $"{Context}/{Context}ComponentsLookup.cs",
                FileContent
            );
        }
    }
}