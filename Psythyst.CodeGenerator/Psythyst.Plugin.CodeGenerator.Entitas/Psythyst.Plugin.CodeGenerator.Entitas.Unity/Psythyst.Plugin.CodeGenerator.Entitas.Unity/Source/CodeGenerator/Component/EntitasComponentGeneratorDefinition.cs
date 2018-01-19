using UnityEngine;

using Psythyst.Data.Entitas;

using Psythyst.Core.Unity;
using Psythyst.Core.Unity.Entitas;

namespace Psythyst.Plugin.CodeGenerator.Entitas.Unity
{
    /// <summary>
    /// EntitasComponentGeneratorDefinition Class.
    /// </summary>
    [CreateAssetMenu(fileName = "New ComponentGenerator Definition", menuName = "Psythyst/Entitas/Generator/Default/ComponentGenerator Definition")]
    public class EntitasComponentGeneratorDefinition : EntitasGeneratorDefinition<EntitasComponentGenerator> { }
}