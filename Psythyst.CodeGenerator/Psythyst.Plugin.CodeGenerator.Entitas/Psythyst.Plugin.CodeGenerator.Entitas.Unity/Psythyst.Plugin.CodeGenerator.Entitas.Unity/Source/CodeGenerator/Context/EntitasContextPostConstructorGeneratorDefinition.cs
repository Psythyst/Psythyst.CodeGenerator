using UnityEngine;

using Psythyst.Data.Entitas;

using Psythyst.Core.Unity;
using Psythyst.Core.Unity.Entitas;

namespace Psythyst.Plugin.CodeGenerator.Entitas.Unity
{
    /// <summary>
    /// EntitasContextPostConstructorGeneratorDefinition Class.
    /// </summary>
    [CreateAssetMenu(fileName = "New ContextPostConstructorGenerator Definition", menuName = "Psythyst/Entitas/Generator/Default/ContextPostConstructorGenerator Definition")]
    public class EntitasContextPostConstructorGeneratorDefinition : EntitasGeneratorDefinition<EntitasContextPostConstructorGenerator> { }
}