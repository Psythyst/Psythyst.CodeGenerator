using System;
using System.Collections.Generic;
using System.Linq;

using Psythyst;
using Psythyst.Core.Data;

namespace Psythyst.CodeGenerator.CSharp
{
    /// <summary>
    /// CSharpEntityIndexExtensionGenerator Class.
    /// </summary>
    public class CSharpEntityIndexExtensionGenerator : IGenerator<ProjectModel, OutputModel>
    {
        public int Priority { get => 0; }

        const string PRIMARY_ENTITY_INDEX_METHOD_TEMPLATE = 
@"    public static ${UpperContextName}Entity GetEntityWith${UpperComponentName}${UpperAttributeName}(this ${UpperContextName}Context Instance, ${AttributeType} ${LowerAttributeName}) => ((Entitas.PrimaryEntityIndex<${UpperContextName}Entity,${AttributeType}>)Instance.GetEntityIndex(${UpperContextName}Context.${UpperComponentName}${UpperAttributeName})).GetEntity(${LowerAttributeName});";

        const string ENTITY_INDEX_METHOD_TEMPLATE = 
@"    public static System.Collections.Generic.HashSet<${UpperContextName}Entity> GetEntitiesWith${UpperComponentName}${UpperAttributeName}(this ${UpperContextName}Context Instance, ${AttributeType} ${LowerAttributeName}) => ((Entitas.EntityIndex<${UpperContextName}Entity,${AttributeType}>)Instance.GetEntityIndex(${UpperContextName}Context.${UpperComponentName}${UpperAttributeName})).GetEntities(${LowerAttributeName});";


        const string ENTITY_INDEX_EXTENSION_TEMPLATE =
@"public static partial class ContextExtension 
{
${EntityIndexCollection}
}
";
        public IEnumerable<OutputModel> Generate(ProjectModel ProjectModel)
        {
            var EntityIndexMethodCollection = new Dictionary<String, List<String>>();
            var OutputModelCollection = new List<OutputModel>();

            foreach(var Component in ProjectModel.Component){
                foreach (var Context in Component.Context){
                    
                    var AttributeCollection = Component.Attribute.Where(x => x.EntityIndex != ProjectEntityIndexType.None);
                    var Method = GetEntityIndexMethodSet(Context, Component.Name, AttributeCollection);

                    if (EntityIndexMethodCollection != null){
                        if (!EntityIndexMethodCollection.ContainsKey(Context))
                            EntityIndexMethodCollection.Add(Context, new List<String>());

                        if (!String.IsNullOrEmpty(Method))
                        EntityIndexMethodCollection[Context].Add(Method);
                    }
                }
            }

            foreach (var Context in EntityIndexMethodCollection){
                if(Context.Value.Count > 0){
                    var EntityIndexExtension = GetEntityIndexExtension(Context.Value);
                    var OutputModel = new OutputModel
                    (
                        $"{Context.Key}/{Context.Key}EntityIndexExtension.cs",
                        EntityIndexExtension
                    );
                    OutputModelCollection.Add(OutputModel);
                }
            }

            return OutputModelCollection;
        }

        string GetEntityIndexMethod(String Context, String Component, ProjectComponentAttributeModel Attribute) {
            
            var TEMPLATE = Attribute.EntityIndex == 
                ProjectEntityIndexType.PrimaryEntityIndex ? 
                    PRIMARY_ENTITY_INDEX_METHOD_TEMPLATE :
                    ENTITY_INDEX_METHOD_TEMPLATE;
            
            return TEMPLATE
                    .Replace("${UpperContextName}", Context.ToUppercaseFirst())
                    .Replace("${UpperComponentName}", Component.ToUppercaseFirst())
                    .Replace("${UpperAttributeName}", Attribute.Name.ToUppercaseFirst())
                    .Replace("${LowerAttributeName}", Attribute.Name.ToLowercaseFirst())
                    .Replace("${AttributeType}", Attribute.Type);
        }

        string GetEntityIndexMethodSet(String Context, String Component, IEnumerable<ProjectComponentAttributeModel> Attribute) {
            var MethodCollection = Attribute
                .Select(x => GetEntityIndexMethod(Context, Component, x));

            return String.Join("\n", MethodCollection);
        }

        string GetEntityIndexExtension(IEnumerable<String> Collection) {
            var EntityIndexCollection = string.Join("\n", Collection);
            return ENTITY_INDEX_EXTENSION_TEMPLATE
                .Replace("${EntityIndexCollection}", EntityIndexCollection);
        }
    }
}