using Psythyst;
using Psythyst.Core.Data;

namespace Psythyst.CodeGenerator.CSharp
{
    /// <summary>
    /// ProjectUnitExtension Class.
    /// </summary>
    public static class ProjectUnitExtension
    {
        public static IProjectUnit<ProjectModel, OutputModel> WithCSharpContextGenerator(this IProjectUnit<ProjectModel, OutputModel> Instance, bool Condition = true) 
            => Instance.AddGenerator(new CSharpContextGenerator(), Condition);

        public static IProjectUnit<ProjectModel, OutputModel> WithCSharpContextMatcherGenerator(this IProjectUnit<ProjectModel, OutputModel> Instance, bool Condition = true)
            => Instance.AddGenerator(new CSharpContextMatcherGenerator(), Condition);

        public static IProjectUnit<ProjectModel, OutputModel> WithCSharpComponentLookupGenerator(this IProjectUnit<ProjectModel, OutputModel> Instance, bool Condition = true)
            => Instance.AddGenerator(new CSharpComponentLookupGenerator(), Condition);

        public static IProjectUnit<ProjectModel, OutputModel> WithCSharpComponentEntityGenerator(this IProjectUnit<ProjectModel, OutputModel> Instance, bool Condition = true)
            => Instance.AddGenerator(new CSharpComponentEntityGenerator(), Condition);

        public static IProjectUnit<ProjectModel, OutputModel> WithCSharpComponentMatcherGenerator(this IProjectUnit<ProjectModel, OutputModel> Instance, bool Condition = true)
            => Instance.AddGenerator(new CSharpComponentMatcherGenerator(), Condition);
        public static IProjectUnit<ProjectModel, OutputModel> WithCSharpComponentContextGenerator(this IProjectUnit<ProjectModel, OutputModel> Instance, bool Condition = true)
            => Instance.AddGenerator(new CSharpComponentContextGenerator(), Condition);

        public static IProjectUnit<ProjectModel, OutputModel> WithCSharpEntityGenerator(this IProjectUnit<ProjectModel, OutputModel> Instance, bool Condition = true)
            => Instance.AddGenerator(new CSharpEntityGenerator(), Condition);

        public static IProjectUnit<ProjectModel, OutputModel> WithCSharpComponentGenerator(this IProjectUnit<ProjectModel, OutputModel> Instance, bool Condition = true)
            => Instance.AddGenerator(new CSharpComponentGenerator(), Condition);
            
        public static IProjectUnit<ProjectModel, OutputModel> WithCSharpContextsGenerator(this IProjectUnit<ProjectModel, OutputModel> Instance, bool Condition = true)
            => Instance.AddGenerator(new CSharpContextsGenerator(), Condition);
    }
}