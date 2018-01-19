using System.Collections.Generic;
using System.Linq;

using Psythyst;
using Psythyst.Data.Entitas;

namespace Psythyst.Plugin.CodeGenerator.Entitas
{
    /// <summary>
    /// EntitasSafeAERCFactoryGenerator Class.
    /// </summary>
    public class EntitasSafeAERCFactoryGenerator : IGenerator<ProjectModel, OutputModel>
    {
        const string SAFEAERC_FACTORY_TEMPLATE =
@"public class SafeAERCFactory : IAERCFactory
{
    public Entitas.IAERC Create(Entitas.IEntity Entity) => new Entitas.SafeAERC(Entity);
}
";
        public int Priority { get { return 0; } }

        public IEnumerable<OutputModel> Generate(ProjectModel ProjectModel)
        {
            return new List<OutputModel>()
            {
                new OutputModel("SafeAERCFactory.cs", SAFEAERC_FACTORY_TEMPLATE)
            };
        }
    }
}