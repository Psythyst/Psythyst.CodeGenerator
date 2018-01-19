using UnityEngine;

using Psythyst.Data.Entitas;

using Psythyst.Core.Unity;
using Psythyst.Core.Unity.Entitas;

namespace Psythyst.Plugin.CodeGenerator.Entitas.Unity
{
    /// <summary>
    /// EntitasEntityGeneratorDefinition Class.
    /// </summary>
    [CreateAssetMenu(fileName = "New EntityGenerator Definition", menuName = "Psythyst/Entitas/Generator/Default/EntityGenerator Definition")]
    public class EntitasEntityGeneratorDefinition : EntitasGeneratorDefinition<EntitasEntityGenerator> { }
}