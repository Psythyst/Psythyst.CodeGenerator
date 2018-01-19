using System.Collections.Generic;
using System.Linq;

using Psythyst;
using Psythyst.Data.Entitas;

namespace Psythyst.Plugin.CodeGenerator.Entitas
{
    /// <summary>
    /// EntitasUnSafeAERCFactoryGenerator Class.
    /// </summary>
    public class EntitasUnSafeAERCFactoryGenerator : IGenerator<ProjectModel, OutputModel>
    {
        const string UNSAFEAERC_FACTORY_TEMPLATE =
@"public class UnSafeAERCFactory : IAERCFactory
{
    public Entitas.IAERC Create(Entitas.IEntity Entity) => new Entitas.UnsafeAERC();
}
";
        public int Priority { get { return 0; } }

        public IEnumerable<OutputModel> Generate(ProjectModel ProjectModel)
        {
            return new List<OutputModel>()
            {
                new OutputModel("UnSafeAERCFactory.cs", UNSAFEAERC_FACTORY_TEMPLATE)
            };
        }
    }
}