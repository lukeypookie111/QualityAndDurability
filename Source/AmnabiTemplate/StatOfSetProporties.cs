using System.Collections.Generic;
using JetBrains.Annotations;
using RimWorld;
using Verse;

namespace Amnabi;

public class QualityStatPart : RimWorld.StatPart_Quality
{
    //Uses the exstention to get data from the tool, applies that data and returns the transformed Value
    
    public override void TransformValue(StatRequest req, ref float val)
    {
        // req.StatBases
        throw new System.NotImplementedException();
    }

    public override string ExplanationPart(StatRequest req)
    {
        throw new System.NotImplementedException();
    }

    private static bool TryGetTool([CanBeNull] Thing thing, [CanBeNull] out ThingDef Tool)
        => (Tool = thing is null || !thing.def.IsWeapon ? null : thing.def) != null;

    private float GetStatOffest(Thing Tool)// Stat Offset , this would be the base value
    {
        var Quality = Tool.TryGetComp<CompQuality>();
        var StatExstenstion = Tool.def.GetModExtension<StatExstention>();
        
        // return StatExstenstion is null? 0 : 


        // Quality.
        // Tool.def
        return 0;
    }
    private float GetStatFactor(Thing Tool) // Stat multiplier , multiply the base value
    {
        var Quality = Tool.TryGetComp<CompQuality>();
        // Quality.
        // Tool.def
        return 0;
    }
    
}

// [DefOf]
// public static class StatModifier
// {
//     
// }