using UnityEngine;

using Psythyst.Data.Entitas;

using Psythyst.Core.Unity;
using Psythyst.Core.Unity.Entitas;

namespace Psythyst.Plugin.CodeGenerator.Entitas.Unity
{
    /// <summary>
    /// EntitasComponentEntityGeneratorDefinition Class.
    /// </summary>
    [CreateAssetMenu(fileName = "New ComponentEntityGenerator Definition", menuName = "Psythyst/Entitas/Generator/Default/ComponentEntityGenerator Definition")]
    public class EntitasComponentEntityGeneratorDefinition : EntitasGeneratorDefinition<EntitasComponentEntityGenerator> { }
}