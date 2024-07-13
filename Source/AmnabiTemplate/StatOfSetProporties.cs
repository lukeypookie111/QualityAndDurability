using System.Collections.Generic;
using JetBrains.Annotations;
using RimWorld;
using Verse;

namespace Amnabi;
[PublicAPI]
public class QualityStatPart : StatPart
{
    //Uses the exstention to get data from the tool, applies that data and returns the transformed Value
    
    public override void TransformValue(StatRequest req, ref float val)
    {
        if (!TryGetTool(req.Thing, out var tool))
            return;
        val *= GetStatFactor(tool);
        val += GetStatOffset(tool);
        

    }

    public override string ExplanationPart(StatRequest req)
    {
        if (!TryGetTool(req.Thing, out var tool))
        {
            return null;
        }
        else
        {
            var statWorker = parentStat.Worker;
            return $"{tool.LabelNoCount}: {
                statWorker.ValueToString(GetStatFactor(tool), false, ToStringNumberSense.Factor)} {
                    statWorker.ValueToString(GetStatOffset(tool), false, ToStringNumberSense.Offset)}";
        }
    }

    private static bool TryGetTool([CanBeNull] Thing thing, [CanBeNull] out Thing Tool)
        => (Tool = thing is null || !thing.def.IsWeapon ? null : thing) != null;

    private float GetStatOffset(Thing tool)// Stat Offset , this would be the base value
    {
        var Quality = tool.TryGetComp<CompQuality>();
        var StatExstenstion = tool.def.GetModExtension<StatExtension>();
        
        // return StatExstenstion is null? 0 : 
       
        var QualitySetting =  Harmony_QualityAndDurability.adjustSwitch(Quality.Quality);
        var equipmentOffsets = tool.def.equippedStatOffsets;
        var ReturnValue = StatExstenstion is null ? 1.0f : StatExstenstion.itemStatOffsetsByQuality is { } itemStatFactorsByQuality
                                                           && Quality.Quality is { } quality
                                                           && itemStatFactorsByQuality.GetStatFactorFromList(parentStat, quality) is not 0f and var result
            ? result
            : StatExstenstion.itemStatOffsets?.GetStatFactorFromList(parentStat) ?? 0f;
        var Ofset = equipmentOffsets;
        Ofset.ForEach(X => X.value *= QualitySetting);
        equipmentOffsets.ForEach(X => X.value *= ReturnValue);
        
        Log.Message("Returned Value for GetStat Offset" + ReturnValue);
        equipmentOffsets.ForEach(X =>Log.Message("Stat Adjusted with return value" +X.stat.defName + " " + X.value));
        
        Log.Message("QualitySetting Value " + QualitySetting);
        Ofset.ForEach(X =>Log.Message("Stat Adjusted with QualitySetting" +X.stat.defName + " " + X.value));
        tool.def.equippedStatOffsets = equipmentOffsets;
        
        return ReturnValue;
    }
    
    private float GetStatFactor(Thing Tool) // Stat multiplier , multiply the base value
    {
        var Quality = Tool.TryGetComp<CompQuality>();
        var StatExstenstion = Tool.def.GetModExtension<StatExtension>();
        
        // return StatExstenstion is null? 0 : 

        var QualitySetting =  Harmony_QualityAndDurability.adjustSwitch(Quality.Quality);
        var ReturnValue = StatExstenstion is null ? 1.0f : StatExstenstion.itemStatFactorsByQuality is { } itemStatFactorsByQuality
                                                           && Quality.Quality is { } quality
                                                           && itemStatFactorsByQuality.GetStatFactorFromList(parentStat, quality) is not 1f and var result
            ? result
            : StatExstenstion.itemStatFactors?.GetStatFactorFromList(parentStat) ?? 1f;
        Log.Message("Returned Value for GetStat Factor " + ReturnValue);
        return ReturnValue;
    }
    
}

