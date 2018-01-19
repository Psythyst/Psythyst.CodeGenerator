using System;
using System.Collections.Generic;
using System.Linq;

using Psythyst;
using Psythyst.Data.Entitas;

namespace Psythyst.Plugin.CodeGenerator.Entitas
{
    /// <summary>
    /// EntitasCustomEntityIndexExtensionGenerator Class.
    /// </summary>
    public class EntitasCustomEntityIndexExtensionGenerator : IGenerator<ProjectModel, OutputModel>
    {
        public int Priority { get { return 0; } }

        const string PRIMARY_ENTITY_INDEX_METHOD_TEMPLATE = 
@"    public static ${UpperContextName}Entity ${EntityIndexMethodName}(this ${UpperContextName}Context Instance${MemberParamCollection}) => ((${EntityIndexType})Instance.GetEntityIndex(${UpperContextName}Context.${EntityIndexType})).${EntityIndexMethodName}(${AttributeCollection});";
        const string ENTITY_INDEX_METHOD_TEMPLATE = 
@"    public static System.Collections.Generic.HashSet<${UpperContextName}Entity> ${EntityIndexMethodName}(this ${UpperContextName}Context Instance${MemberParamCollection}) => ((${EntityIndexType})Instance.GetEntityIndex(${UpperContextName}Context.${EntityIndexType})).${EntityIndexMethodName}(${AttributeCollection});";

        const string ENTITY_INDEX_PARAMS_MEMBER_TEMPLATE = @"${AttributeType} ${AttributeName}";

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

        string GetEntityIndexParamMember(CustomEntityIndexMemberModel Member) {
            return ENTITY_INDEX_PARAMS_MEMBER_TEMPLATE
                    .Replace("${AttributeType}", Member.Type)
                    .Replace("${AttributeName}", Member.Name);
        }

        string GetEntityIndexMemberParamSet(IEnumerable<CustomEntityIndexMemberModel> Attribute) {
            var AttributeCollection = Attribute
                .Select(x => GetEntityIndexParamMember(x));

            return String.Join(", ", AttributeCollection);
        }

        string GetEntityIndexMemberSet(IEnumerable<CustomEntityIndexMemberModel> Member) {
            var MemberCollection = Member
                .Select(x => x.Name);

            return String.Join(", ", MemberCollection);
        }

        string GetEntityIndexMethod(String Context, CustomEntityIndexModel EntityIndex) {
            
            var TEMPLATE = EntityIndex.EntityIndexParentType == 
                EntityIndexType.PrimaryIndex ? 
                    PRIMARY_ENTITY_INDEX_METHOD_TEMPLATE :
                    ENTITY_INDEX_METHOD_TEMPLATE;
            
            var AttributeCollection = EntityIndex.Member == null ? String.Empty : GetEntityIndexMemberSet(EntityIndex.Member);
            var AttributeParamCollection = EntityIndex.Member == null ? String.Empty : $", {GetEntityIndexMemberParamSet(EntityIndex.Member)}";

            return TEMPLATE
                    .Replace("${EntityIndexMethodName}", EntityIndex.EntityIndexMethod)
                    .Replace("${UpperContextName}", Context.ToUppercaseFirst())
                    .Replace("${MemberParamCollection}", AttributeParamCollection)
                    .Replace("${EntityIndexType}", EntityIndex.EntityIndexType)
                    .Replace("${AttributeCollection}", AttributeCollection);
        }

        string GetEntityIndexMethodSet(String Context, IEnumerable<CustomEntityIndexModel> Member) {
            var MethodCollection = Member
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