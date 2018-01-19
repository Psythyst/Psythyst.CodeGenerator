using System.Collections.Generic;
using System.Linq;

using Psythyst;
using Psythyst.Data.Entitas;

namespace Psythyst.Plugin.CodeGenerator.Entitas
{
    /// <summary>
    /// EntitasAERCFactoryInterfaceGenerator Class.
    /// </summary>
    public class EntitasAERCFactoryInterfaceGenerator : IGenerator<ProjectModel, OutputModel>
    {
        const string AERC_FACTORY_INTERFACE_TEMPLATE =
@"public interface IAERCFactory
{
    Entitas.IAERC Create(Entitas.IEntity Entity);
}
";
        public int Priority { get { return 0; } }

        public IEnumerable<OutputModel> Generate(ProjectModel ProjectModel)
        {
            return new List<OutputModel>()
            {
                new OutputModel("IAERCFactory.cs", AERC_FACTORY_INTERFACE_TEMPLATE)
            };
        }
    }
}