using UnityEngine;

using Psythyst.Data.Entitas;

using Psythyst.Core.Unity;
using Psythyst.Core.Unity.Entitas;

namespace Psythyst.Plugin.CodeGenerator.Entitas.Unity
{
    /// <summary>
    /// EntitasCustomEntityIndexExtensionGeneratorDefinition Class.
    /// </summary>
    [CreateAssetMenu(fileName = "New CustomEntityIndexExtensionGenerator Definition", menuName = "Psythyst/Entitas/Generator/Default/CustomEntityIndexExtensionGenerator Definition")]
    public class EntitasCustomEntityIndexExtensionGeneratorDefinition : EntitasGeneratorDefinition<EntitasCustomEntityIndexExtensionGenerator> { }
}