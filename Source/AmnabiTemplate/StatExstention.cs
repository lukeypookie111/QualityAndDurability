using System.Collections.Generic;
using JetBrains.Annotations;
using RimWorld;
using UnityEngine;
using Verse;

namespace Amnabi;

[UsedImplicitly(ImplicitUseTargetFlags.Members)]
public class StatExtension: DefModExtension
{
    [CanBeNull]public List<StatModifierQuality> itemStatFactorsByQuality, itemStatOffsetsByQuality;
    [CanBeNull]public List<StatModifier> itemStatFactors, itemStatOffsets;
    
    public void Initialize(ThingDef parent)
    {
        if (parent is null)
            return;
        Log.Warning("Perant of name " + parent.defName + " Was initilised ");
    }
}