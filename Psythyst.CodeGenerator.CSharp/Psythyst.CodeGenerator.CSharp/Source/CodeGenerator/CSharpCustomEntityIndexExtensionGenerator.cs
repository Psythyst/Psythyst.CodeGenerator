using System;
using System.Collections.Generic;
using System.Linq;

using Psythyst;
using Psythyst.Core.Data;

namespace Psythyst.CodeGenerator.CSharp
{
    /// <summary>
    /// CSharpCustomEntityIndexExtensionGenerator Class.
    /// </summary>
    public class CSharpCustomEntityIndexExtensionGenerator : IGenerator<ProjectModel, OutputModel>
    {
        public int Priority { get => 0; }

        const string PRIMARY_ENTITY_INDEX_METHOD_TEMPLATE = 
@"    public static ${UpperContextName}Entity ${EntityIndexMethodName}(this ${UpperContextName}Context Instance${AttributeParamCollection}) => ((${EntityIndexType})Instance.GetEntityIndex(${UpperContextName}Context.${EntityIndexType})).${EntityIndexMethodName}(${AttributeCollection});";
        const string ENTITY_INDEX_METHOD_TEMPLATE = 
@"    public static System.Collections.Generic.HashSet<${UpperContextName}Entity> ${EntityIndexMethodName}(this ${UpperContextName}Context Instance${AttributeParamCollection}) => ((${EntityIndexType})Instance.GetEntityIndex(${UpperContextName}Context.${EntityIndexType})).${EntityIndexMethodName}(${AttributeCollection});";

        const string ENTITY_INDEX_PARAMS_ATTRIBUTE_TEMPLATE = @"${AttributeType} ${AttributeName}";

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

            foreach(var CustomEntityIndex in ProjectModel.CustomEntityIndex){
                    
                var Method = GetEntityIndexMethod(CustomEntityIndex.Context, CustomEntityIndex);

                if (!EntityIndexMethodCollection.ContainsKey(CustomEntityIndex.Context))
                   EntityIndexMethodCollection.Add(CustomEntityIndex.Context, new List<String>());

                if (!String.IsNullOrEmpty(Method))
                    EntityIndexMethodCollection[CustomEntityIndex.Context].Add(Method);
            }

            
            foreach (var Context in EntityIndexMethodCollection){
                if(Context.Value.Count > 0){
                    var EntityIndexExtension = GetEntityIndexExtension(Context.Value);
                    var OutputModel = new OutputModel
                    (
                        $"{Context.Key}/{Context.Key}CustomEntityIndexExtension.cs",
                        EntityIndexExtension
                    );
                    OutputModelCollection.Add(OutputModel);
                }
            }

            return OutputModelCollection;
        }

        string GetEntityIndexParamAttribute(ProjectCustomEntityIndexAttributeModel Attribute) {
            return ENTITY_INDEX_PARAMS_ATTRIBUTE_TEMPLATE
                    .Replace("${AttributeType}", Attribute.Type)
                    .Replace("${AttributeName}", Attribute.Name);
        }

        string GetEntityIndexAttributeParamSet(IEnumerable<ProjectCustomEntityIndexAttributeModel> Attribute) {
            var AttributeCollection = Attribute
                .Select(x => GetEntityIndexParamAttribute(x));

            return String.Join(", ", AttributeCollection);
        }

        string GetEntityIndexAttributeSet(IEnumerable<ProjectCustomEntityIndexAttributeModel> Attribute) {
            var AttributeCollection = Attribute
                .Select(x => x.Name);

            return String.Join(", ", AttributeCollection);
        }

        string GetEntityIndexMethod(String Context, ProjectCustomEntityIndexModel EntityIndex) {
            
            var TEMPLATE = EntityIndex.EntityIndexParentType == 
                ProjectEntityIndexType.PrimaryEntityIndex ? 
                    PRIMARY_ENTITY_INDEX_METHOD_TEMPLATE :
                    ENTITY_INDEX_METHOD_TEMPLATE;
            
            var AttributeCollection = EntityIndex.Attribute == null ? String.Empty : GetEntityIndexAttributeSet(EntityIndex.Attribute);
            var AttributeParamCollection = EntityIndex.Attribute == null ? String.Empty : $", {GetEntityIndexAttributeParamSet(EntityIndex.Attribute)}";

            return TEMPLATE
                    .Replace("${EntityIndexMethodName}", EntityIndex.EntityIndexMethod)
                    .Replace("${UpperContextName}", Context.ToUppercaseFirst())
                    .Replace("${AttributeParamCollection}", AttributeParamCollection)
                    .Replace("${EntityIndexType}", EntityIndex.EntityIndexType)
                    .Replace("${AttributeCollection}", AttributeCollection);
        }

        string GetEntityIndexMethodSet(String Context, IEnumerable<ProjectCustomEntityIndexModel> Attribute) {
            var MethodCollection = Attribute
                .Select(x => GetEntityIndexMethod(Context, x));

            return String.Join("\n", MethodCollection);
        }

        string GetEntityIndexExtension(IEnumerable<String> Collection) {
            var EntityIndexCollection = string.Join("\n", Collection);
            return ENTITY_INDEX_EXTENSION_TEMPLATE
                .Replace("${EntityIndexCollection}", EntityIndexCollection);
        }
    }
}