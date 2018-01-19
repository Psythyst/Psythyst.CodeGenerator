using UnityEngine;

using Psythyst.Data.Entitas;

using Psythyst.Core.Unity;
using Psythyst.Core.Unity.Entitas;

namespace Psythyst.Plugin.CodeGenerator.Entitas.Unity
{
    /// <summary>
    /// EntitasComponentLookupGeneratorDefinition Class.
    /// </summary>
    [CreateAssetMenu(fileName = "New ComponentLookupGenerator Definition", menuName = "Psythyst/Entitas/Generator/Default/ComponentLookupGenerator Definition")]
    public class EntitasComponentLookupGeneratorDefinition : EntitasGeneratorDefinition<EntitasComponentLookupGenerator> { }
}