using UnityEngine;

using Psythyst.Data.Entitas;

using Psythyst.Core.Unity;
using Psythyst.Core.Unity.Entitas;

namespace Psythyst.Plugin.CodeGenerator.Entitas.Unity
{
    /// <summary>
    /// EntitasComponentContextGeneratorDefinition Class.
    /// </summary>
    [CreateAssetMenu(fileName = "New ComponentContextGenerator Definition", menuName = "Psythyst/Entitas/Generator/Default/ComponentContextGenerator Definition")]
    public class EntitasComponentContextGeneratorDefinition : EntitasGeneratorDefinition<EntitasComponentContextGenerator> { }
}