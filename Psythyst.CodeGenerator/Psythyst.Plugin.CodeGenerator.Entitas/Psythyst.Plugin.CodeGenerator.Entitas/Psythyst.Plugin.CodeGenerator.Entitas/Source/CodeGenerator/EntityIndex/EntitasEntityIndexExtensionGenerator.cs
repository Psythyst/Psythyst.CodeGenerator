using System;
using System.Collections.Generic;
using System.Linq;

using Psythyst;
using Psythyst.Data.Entitas;

namespace Psythyst.Plugin.CodeGenerator.Entitas
{
    /// <summary>
    /// EntitasEntityIndexExtensionGenerator Class.
    /// </summary>
    public class EntitasEntityIndexExtensionGenerator : IGenerator<ProjectModel, OutputModel>
    {
        public int Priority { get { return 0; } }

        const string PRIMARY_ENTITY_INDEX_METHOD_TEMPLATE = 
@"    public static ${UpperContextName}Entity GetEntityWith${UpperComponentName}${UpperMemberName}(this ${UpperContextName}Context Instance, ${MemberType} ${LowerMemberName}) => ((Entitas.PrimaryEntityIndex<${UpperContextName}Entity,${MemberType}>)Instance.GetEntityIndex(${UpperContextName}Context.${UpperComponentName}${UpperMemberName})).GetEntity(${LowerMemberName});";

        const string ENTITY_INDEX_METHOD_TEMPLATE = 
@"    public static System.Collections.Generic.HashSet<${UpperContextName}Entity> GetEntitiesWith${UpperComponentName}${UpperMemberName}(this ${UpperContextName}Context Instance, ${MemberType} ${LowerMemberName}) => ((Entitas.EntityIndex<${UpperContextName}Entity,${MemberType}>)Instance.GetEntityIndex(${UpperContextName}Context.${UpperComponentName}${UpperMemberName})).GetEntities(${LowerMemberName});";


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
                    
                    var MemberCollection = Component.Member.Where(x => x.EntityIndex != EntityIndexType.None);
                    var Method = GetEntityIndexMethodSet(Context, Component.Name, MemberCollection);

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

        string GetEntityIndexMethod(String Context, String Component, ComponentMemberModel Member) {
            
            var TEMPLATE = Member.EntityIndex == 
                EntityIndexType.PrimaryIndex ? 
                    PRIMARY_ENTITY_INDEX_METHOD_TEMPLATE :
                    ENTITY_INDEX_METHOD_TEMPLATE;
            
            return TEMPLATE
                    .Replace("${UpperContextName}", Context.ToUppercaseFirst())
                    .Replace("${UpperComponentName}", Component.ToUppercaseFirst())
                    .Replace("${UpperMemberName}", Member.Name.ToUppercaseFirst())
                    .Replace("${LowerMemberName}", Member.Name.ToLowercaseFirst())
                    .Replace("${MemberType}", Member.Type);
        }

        string GetEntityIndexMethodSet(String Context, String Component, IEnumerable<ComponentMemberModel> Member) {
            var MethodCollection = Member
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