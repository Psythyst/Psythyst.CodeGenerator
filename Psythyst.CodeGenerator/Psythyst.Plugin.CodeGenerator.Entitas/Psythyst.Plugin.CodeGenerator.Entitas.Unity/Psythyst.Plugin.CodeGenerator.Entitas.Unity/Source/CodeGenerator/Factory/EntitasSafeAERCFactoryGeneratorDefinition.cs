﻿using UnityEngine;

using Psythyst.Data.Entitas;

using Psythyst.Core.Unity;
using Psythyst.Core.Unity.Entitas;

namespace Psythyst.Plugin.CodeGenerator.Entitas.Unity
{
    /// <summary>
    /// EntitasSafeAERCFactoryGeneratorDefinition Class.
    /// </summary>
    [CreateAssetMenu(fileName = "New SafeAERCFactoryGenerator Definition", menuName = "Psythyst/Entitas/Generator/Default/SafeAERCFactoryGenerator Definition")]
    public class EntitasSafeAERCFactoryGeneratorDefinition : EntitasGeneratorDefinition<EntitasSafeAERCFactoryGenerator> { }
}