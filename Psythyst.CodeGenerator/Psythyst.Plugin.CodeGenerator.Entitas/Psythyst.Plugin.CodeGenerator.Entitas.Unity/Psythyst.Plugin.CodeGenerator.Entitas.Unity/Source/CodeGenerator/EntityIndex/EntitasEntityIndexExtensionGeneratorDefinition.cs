using UnityEngine;

using Psythyst.Data.Entitas;

using Psythyst.Core.Unity;
using Psythyst.Core.Unity.Entitas;

namespace Psythyst.Plugin.CodeGenerator.Entitas.Unity
{
    /// <summary>
    /// EntitasEntityIndexExtensionGeneratorDefinition Class.
    /// </summary>
    [CreateAssetMenu(fileName = "New EntityIndexExtensionGenerator Definition", menuName = "Psythyst/Entitas/Generator/Default/EntityIndexExtensionGenerator Definition")]
    public class EntitasEntityIndexExtensionGeneratorDefinition : EntitasGeneratorDefinition<EntitasEntityIndexExtensionGenerator> { }
}