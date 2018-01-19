using Psythyst;
using Psythyst.Data.Entitas;

namespace Psythyst.Plugin.CodeGenerator.Entitas
{
    /// <summary>
    /// ProjectUnitExtension Class.
    /// </summary>
    public static class ProjectUnitExtension
    {
        public static IProjectUnit<ProjectModel, OutputModel> AddEntitasAERCFactoryInterfaceGenerator(this IProjectUnit<ProjectModel, OutputModel> Instance, bool Condition = true)
            => Instance.AddGenerator(new EntitasAERCFactoryInterfaceGenerator(), Condition);

        public static IProjectUnit<ProjectModel, OutputModel> AddEntitasComponentContextGenerator(this IProjectUnit<ProjectModel, OutputModel> Instance, bool Condition = true)
            => Instance.AddGenerator(new EntitasComponentContextGenerator(), Condition);

        public static IProjectUnit<ProjectModel, OutputModel> AddEntitasComponentEntityGenerator(this IProjectUnit<ProjectModel, OutputModel> Instance, bool Condition = true)
            => Instance.AddGenerator(new EntitasComponentEntityGenerator(), Condition);

        public static IProjectUnit<ProjectModel, OutputModel> AddEntitasComponentGenerator(this IProjectUnit<ProjectModel, OutputModel> Instance, bool Condition = true)
            => Instance.AddGenerator(new EntitasComponentGenerator(), Condition);

        public static IProjectUnit<ProjectModel, OutputModel> AddEntitasComponentLookupGenerator(this IProjectUnit<ProjectModel, OutputModel> Instance, bool Condition = true)
            => Instance.AddGenerator(new EntitasComponentLookupGenerator(), Condition);

        public static IProjectUnit<ProjectModel, OutputModel> AddEntitasComponentMatcherGenerator(this IProjectUnit<ProjectModel, OutputModel> Instance, bool Condition = true)
            => Instance.AddGenerator(new EntitasComponentMatcherGenerator(), Condition);

        public static IProjectUnit<ProjectModel, OutputModel> AddEntitasContextGenerator(this IProjectUnit<ProjectModel, OutputModel> Instance, bool Condition = true) 
            => Instance.AddGenerator(new EntitasContextGenerator(), Condition);

        public static IProjectUnit<ProjectModel, OutputModel> AddEntitasContextMatcherGenerator(this IProjectUnit<ProjectModel, OutputModel> Instance, bool Condition = true)
            => Instance.AddGenerator(new EntitasContextMatcherGenerator(), Condition);

        public static IProjectUnit<ProjectModel, OutputModel> AddEntitasContextPostConstructorGenerator(this IProjectUnit<ProjectModel, OutputModel> Instance, bool Condition = true)
            => Instance.AddGenerator(new EntitasContextPostConstructorGenerator(), Condition);

        public static IProjectUnit<ProjectModel, OutputModel> AddEntitasCustomEntityIndexExtensionGenerator(this IProjectUnit<ProjectModel, OutputModel> Instance, bool Condition = true)
            => Instance.AddGenerator(new EntitasCustomEntityIndexExtensionGenerator(), Condition);

        public static IProjectUnit<ProjectModel, OutputModel> AddEntitasEntityGenerator(this IProjectUnit<ProjectModel, OutputModel> Instance, bool Condition = true)
            => Instance.AddGenerator(new EntitasEntityGenerator(), Condition);

        public static IProjectUnit<ProjectModel, OutputModel> AddEntitasEntityIndexExtensionGenerator(this IProjectUnit<ProjectModel, OutputModel> Instance, bool Condition = true)
            => Instance.AddGenerator(new EntitasEntityIndexExtensionGenerator(), Condition);

        public static IProjectUnit<ProjectModel, OutputModel> AddEntitasEntityIndexGenerator(this IProjectUnit<ProjectModel, OutputModel> Instance, bool Condition = true)
            => Instance.AddGenerator(new EntitasEntityIndexGenerator(), Condition);

        public static IProjectUnit<ProjectModel, OutputModel> AddEntitasPostConstructorAttributeGenerator(this IProjectUnit<ProjectModel, OutputModel> Instance, bool Condition = true)
            => Instance.AddGenerator(new EntitasPostConstructorAttributeGenerator(), Condition);

        public static IProjectUnit<ProjectModel, OutputModel> AddEntitasSafeAERCFactoryGenerator(this IProjectUnit<ProjectModel, OutputModel> Instance, bool Condition = true)
            => Instance.AddGenerator(new EntitasSafeAERCFactoryGenerator(), Condition);

        public static IProjectUnit<ProjectModel, OutputModel> AddEntitasUnSafeAERCFactoryGenerator(this IProjectUnit<ProjectModel, OutputModel> Instance, bool Condition = true)
            => Instance.AddGenerator(new EntitasUnSafeAERCFactoryGenerator(), Condition);
    }
}