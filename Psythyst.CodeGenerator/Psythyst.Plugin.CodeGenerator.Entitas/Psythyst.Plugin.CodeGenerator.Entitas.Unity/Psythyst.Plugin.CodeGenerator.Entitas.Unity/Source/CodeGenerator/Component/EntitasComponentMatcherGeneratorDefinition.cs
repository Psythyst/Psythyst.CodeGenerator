using UnityEngine;

using Psythyst.Data.Entitas;
using Psythyst.Core.Unity;
using Psythyst.Core.Unity.Entitas;

namespace Psythyst.Plugin.CodeGenerator.Entitas.Unity
{
    /// <summary>
    /// EntitasComponentMatcherGeneratorDefinition Class.
    /// </summary>
    [CreateAssetMenu(fileName = "New ComponentMatcherGenerator Definition", menuName = "Psythyst/Entitas/Generator/Default/ComponentMatcherGenerator Definition")]
    public class EntitasComponentMatcherGeneratorDefinition : EntitasGeneratorDefinition<EntitasComponentMatcherGenerator> { }
}