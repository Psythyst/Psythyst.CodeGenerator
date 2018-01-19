using UnityEngine;

using Psythyst.Data.Entitas;

using Psythyst.Core.Unity;
using Psythyst.Core.Unity.Entitas;

namespace Psythyst.Plugin.CodeGenerator.Entitas.Unity
{
    /// <summary>
    /// EntitasEntityIndexGeneratorDefinition Class.
    /// </summary>
    [CreateAssetMenu(fileName = "New EntityIndexGenerator Definition", menuName = "Psythyst/Entitas/Generator/Default/EntityIndexGenerator Definition")]
    public class EntitasEntityIndexGeneratorDefinition : EntitasGeneratorDefinition<EntitasEntityIndexGenerator> { }
}