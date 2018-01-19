using UnityEngine;

using Psythyst.Data.Entitas;

using Psythyst.Core.Unity;
using Psythyst.Core.Unity.Entitas;

namespace Psythyst.Plugin.CodeGenerator.Entitas.Unity
{
    /// <summary>
    /// EntitasContextMatcherGeneratorDefinition Class.
    /// </summary>
    [CreateAssetMenu(fileName = "New ContextMatcherGenerator Definition", menuName = "Psythyst/Entitas/Generator/Default/ContextMatcherGenerator Definition")]
    public class EntitasContextMatcherGeneratorDefinition : EntitasGeneratorDefinition<EntitasContextMatcherGenerator> { }
}