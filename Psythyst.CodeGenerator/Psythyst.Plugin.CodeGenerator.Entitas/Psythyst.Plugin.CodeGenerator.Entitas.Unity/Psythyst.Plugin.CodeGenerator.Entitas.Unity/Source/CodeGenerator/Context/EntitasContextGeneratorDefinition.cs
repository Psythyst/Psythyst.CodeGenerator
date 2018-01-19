using UnityEngine;

using Psythyst.Data.Entitas;

using Psythyst.Core.Unity;
using Psythyst.Core.Unity.Entitas;

namespace Psythyst.Plugin.CodeGenerator.Entitas.Unity
{
    /// <summary>
    /// EntitasContextGeneratorDefinition Class.
    /// </summary>
    [CreateAssetMenu(fileName = "New ContextGenerator Definition", menuName = "Psythyst/Entitas/Generator/Default/ContextGenerator Definition")]
    public class EntitasContextGeneratorDefinition : EntitasGeneratorDefinition<EntitasContextGenerator> { }
}