using System.Collections.Generic;
using JetBrains.Annotations;
using RimWorld;
using UnityEngine;
using Verse;

namespace Amnabi;

public class StatExstention: DefModExtension
{
    [CanBeNull]public List<StatModifierQuality> itemStatFactorsByQuality, itemStatOffsetsByQuality;
    [CanBeNull]public List<StatModifier> itemStatFactors, itemStatOffsets;
    public QualityStatPart StatPart = new ();
    
    public void Initialize(ThingDef parent)
    {
        if (parent is null)
            return;
        Log.Warning("Perant of name " + parent.defName + " Was initilised ");
    }
}