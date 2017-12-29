using System;
using System.Collections.Generic;
using System.Linq;

using Psythyst;
using Psythyst.Core.Data;

namespace Psythyst.CodeGenerator.CSharp
{
    /// <summary>
    /// CSharpEntityIndexGenerator Class.
    /// </summary>
    public class CSharpEntityIndexGenerator : IGenerator<ProjectModel, OutputModel>
    {
        public int Priority { get => 0; }
        const string ENTITY_INDEX_TEMPLATE = @"this.AddEntityIndex(new ${EntityIndexType}<${UpperContextName}Entity,${AttributeType}>(${UpperComponentName}${UpperAttributeName}, this.GetGroup(${UpperContextName}Matcher.${UpperComponentName}), (e, c) => ((${UpperComponentName}Component)c).${LowerAttributeName}));";

        const string ENTITY_INDEX_METHOD_TEMPLATE = @"    [PostConstructor] public void Add${UpperComponentName}${UpperAttributeName}${EntityIndexType}() => ${EntityIndex}";

        const string ENTITY_INDEX_CONSTANT_TEMPLATE = @"    public const string ${UpperComponentName}${UpperAttributeName} = ""${UpperComponentName}${UpperAttributeName}"";";

        const string ENTITY_INDEX_CONTEXT_TEMPLATE =
@"public sealed partial class ${UpperContextName}Context {

${EntityIndexConstantCollection}
    
${EntityIndexMethodCollection}
}
";

        public IEnumerable<OutputModel> Generate(ProjectModel ProjectModel)
        {
            var EntityIndexConstantCollection = new Dictionary<String, List<String>>();
            var EntityIndexMethodCollection = new Dictionary<String, List<String>>();

            var OutputModelCollection = new List<OutputModel>();

            foreach(var Component in ProjectModel.Component){
                foreach (var Context in Component.Context){

                    var AttributeCollection = Component.Attribute
                        .Where(x => x.EntityIndex != ProjectEntityIndexType.None);

                    var MethodSet = GetEntityIndexMethodSet(Context, Component.Name, AttributeCollection);
                    var ConstantSet = GetEntityIndexConstantSet(Component.Name, AttributeCollection);

                    if (EntityIndexMethodCollection != null && EntityIndexConstantCollection != null){
                        if (!EntityIndexMethodCollection.ContainsKey(Context)) 
                            EntityIndexMethodCollection.Add(Context, new List<String>());
                        if (!EntityIndexConstantCollection.ContainsKey(Context)) 
                            EntityIndexConstantCollection.Add(Context, new List<String>());
                    }
                    
                    if (!String.IsNullOrEmpty(MethodSet) && !String.IsNullOrEmpty(ConstantSet)) {
                        EntityIndexMethodCollection[Context].Add(MethodSet);
                        EntityIndexConstantCollection[Context].Add(ConstantSet);
                    }
                }
            }

            foreach (var Context in EntityIndexMethodCollection){
                if(Context.Value.Count > 0){

                    var EntityIndexExtension = GetEntityIndexContext(
                        Context.Key,
                        EntityIndexConstantCollection[Context.Key],
                        Context.Value
                    );
                    
                    var OutputModel = new OutputModel
                    (
                        $"{Context.Key}/{Context.Key}EntityIndex.cs",
                        EntityIndexExtension
                    );

                    OutputModelCollection.Add(OutputModel);
                }
            }

            return OutputModelCollection;
        }

        string GetEntityIndexConstant(String Component, ProjectComponentAttributeModel Attribute) {
            return ENTITY_INDEX_CONSTANT_TEMPLATE
                .Replace("${UpperComponentName}", Component.ToUppercaseFirst())
                .Replace("${UpperAttributeName}", Attribute.Name.ToUppercaseFirst());
        }

        string GetEntityIndexConstantSet(String Component, IEnumerable<ProjectComponentAttributeModel> Attribute) {
            var ConstantCollection = Attribute
                .Select(x => GetEntityIndexConstant(Component, x));

            return String.Join("\n", ConstantCollection);
        }

        string GetEntityIndexMethod(String Context, String Component, ProjectComponentAttributeModel Attribute) {
            return ENTITY_INDEX_METHOD_TEMPLATE
                .Replace("${UpperComponentName}", Component.ToUppercaseFirst())
                .Replace("${UpperAttributeName}", Attribute.Name.ToUppercaseFirst())
                .Replace("${EntityIndexType}", Attribute.EntityIndex.ToString())
                .Replace("${EntityIndex}", GetEntityIndex(Context, Component, Attribute));
        }

        string GetEntityIndexMethodSet(String Context, String Component, IEnumerable<ProjectComponentAttributeModel> Attribute) {
            var MethodCollection = Attribute
                .Select(x => GetEntityIndexMethod(Context, Component, x));

            return String.Join("\n", MethodCollection);
        }

        string GetEntityIndex(String Context, String Component, ProjectComponentAttributeModel Attribute)
        {
            return ENTITY_INDEX_TEMPLATE
                .Replace("${UpperContextName}", Context.ToUppercaseFirst())
                .Replace("${UpperComponentName}", Component.ToUppercaseFirst())
                .Replace("${UpperAttributeName}", Attribute.Name.ToUppercaseFirst())
                .Replace("${LowerAttributeName}", Attribute.Name.ToLowercaseFirst())
                .Replace("${EntityIndexType}", $"Entitas.{Attribute.EntityIndex.ToString()}")
                .Replace("${AttributeType}", Attribute.Type);
        }

        string GetEntityIndexContext(String Context, IEnumerable<String> ConstantCollection, IEnumerable<String> MethodCollection)
        {
            var Method = String.Join("\n", MethodCollection);
            var Constant = String.Join("\n", ConstantCollection);

            return ENTITY_INDEX_CONTEXT_TEMPLATE
                .Replace("${UpperContextName}", Context)
                .Replace("${EntityIndexConstantCollection}", Constant)
                .Replace("${EntityIndexMethodCollection}", Method);
        }
    }
}