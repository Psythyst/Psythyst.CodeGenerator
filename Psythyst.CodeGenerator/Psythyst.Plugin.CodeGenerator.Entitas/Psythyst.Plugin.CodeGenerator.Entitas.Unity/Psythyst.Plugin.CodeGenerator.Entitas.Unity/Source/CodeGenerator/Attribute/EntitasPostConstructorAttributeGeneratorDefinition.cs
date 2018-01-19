using UnityEngine;

using Psythyst.Data.Entitas;

using Psythyst.Core.Unity;
using Psythyst.Core.Unity.Entitas;

namespace Psythyst.Plugin.CodeGenerator.Entitas.Unity
{
    /// <summary>
    /// EntitasPostConstructorAttributeGeneratorDefinition Class.
    /// </summary>
    [CreateAssetMenu(fileName = "New PostConstructorAttributeGenerator Definition", menuName = "Psythyst/Entitas/Generator/Default/PostConstructorAttributeGenerator Definition")]
    public class EntitasPostConstructorAttributeGeneratorDefinition : EntitasGeneratorDefinition<EntitasPostConstructorAttributeGenerator> { }
}