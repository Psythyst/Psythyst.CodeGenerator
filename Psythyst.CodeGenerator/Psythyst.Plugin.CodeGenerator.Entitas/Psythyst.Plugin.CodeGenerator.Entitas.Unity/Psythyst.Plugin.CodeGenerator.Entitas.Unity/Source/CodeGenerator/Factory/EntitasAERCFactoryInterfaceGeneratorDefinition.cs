using UnityEngine;

using Psythyst.Data.Entitas;

using Psythyst.Core.Unity;
using Psythyst.Core.Unity.Entitas;

namespace Psythyst.Plugin.CodeGenerator.Entitas.Unity
{
    /// <summary>
    /// EntitasAERCFactoryInterfaceGeneratorDefinition Class.
    /// </summary>
    [CreateAssetMenu(fileName = "New AERCFactoryInterfaceGenerator Definition", menuName = "Psythyst/Entitas/Generator/Default/AERCFactoryInterfaceGenerator Definition")]
    public class EntitasAERCFactoryInterfaceGeneratorDefinition : EntitasGeneratorDefinition<EntitasAERCFactoryInterfaceGenerator> { }
}