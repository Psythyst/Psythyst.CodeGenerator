using System;
using System.Collections.Generic;
using System.Linq;

using Psythyst;
using Psythyst.Data.Entitas;

namespace Psythyst.Plugin.CodeGenerator.Entitas
{
    /// <summary>
    /// EntitasEntityIndexGenerator Class.
    /// </summary>
    public class EntitasEntityIndexGenerator : IGenerator<ProjectModel, OutputModel>
    {
        public int Priority { get { return 0; } }

        const string ENTITY_INDEX_TEMPLATE = @"this.AddEntityIndex(new ${EntityIndexType}<${UpperContextName}Entity,${MemberType}>(${UpperComponentName}${UpperMemberName}, this.GetGroup(${UpperContextName}Matcher.${UpperComponentName}), (e, c) => ((${UpperComponentName}Component)c).${LowerMemberName}));";

        const string ENTITY_INDEX_METHOD_TEMPLATE = @"    [PostConstructor] public void Add${UpperComponentName}${UpperMemberName}${EntityIndexType}() => ${EntityIndex}";

        const string ENTITY_INDEX_CONSTANT_TEMPLATE = @"    public const string ${UpperComponentName}${UpperMemberName} = ""${UpperComponentName}${UpperMemberName}"";";

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

                    var MemberCollection = Component.Member
                        .Where(x => x.EntityIndex != EntityIndexType.None);

                    var MethodSet = GetEntityIndexMethodSet(Context, Component.Name, MemberCollection);
                    var ConstantSet = GetEntityIndexConstantSet(Component.Name, MemberCollection);

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

        string GetEntityIndexConstant(String Component, ComponentMemberModel Member) {
            return ENTITY_INDEX_CONSTANT_TEMPLATE
                .Replace("${UpperComponentName}", Component.ToUppercaseFirst())
                .Replace("${UpperMemberName}", Member.Name.ToUppercaseFirst());
        }

        string GetEntityIndexConstantSet(String Component, IEnumerable<ComponentMemberModel> Member) {
            var ConstantCollection = Member
                .Select(x => GetEntityIndexConstant(Component, x));

            return String.Join("\n", ConstantCollection);
        }

        string GetEntityIndexMethod(String Context, String Component, ComponentMemberModel Member) {
            return ENTITY_INDEX_METHOD_TEMPLATE
                .Replace("${UpperComponentName}", Component.ToUppercaseFirst())
                .Replace("${UpperMemberName}", Member.Name.ToUppercaseFirst())
                .Replace("${EntityIndexType}", Member.EntityIndex.ToString())
                .Replace("${EntityIndex}", GetEntityIndex(Context, Component, Member));
        }

        string GetEntityIndexMethodSet(String Context, String Component, IEnumerable<ComponentMemberModel> Member) {
            var MethodCollection = Member
                .Select(x => GetEntityIndexMethod(Context, Component, x));

            return String.Join("\n", MethodCollection);
        }

        string GetEntityIndex(String Context, String Component, ComponentMemberModel Member)
        {
            var EntityIndex = String.Empty;

            if (Member.EntityIndex.ToString() == "PrimaryIndex") EntityIndex = "PrimaryEntityIndex";
            else if (Member.EntityIndex.ToString() == "Index") EntityIndex = "EntityIndex";

            return ENTITY_INDEX_TEMPLATE
                .Replace("${UpperContextName}", Context.ToUppercaseFirst())
                .Replace("${UpperComponentName}", Component.ToUppercaseFirst())
                .Replace("${UpperMemberName}", Member.Name.ToUppercaseFirst())
                .Replace("${LowerMemberName}", Member.Name.ToLowercaseFirst())
                .Replace("${EntityIndexType}", $"Entitas.{EntityIndex}")
                .Replace("${MemberType}", Member.Type);
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