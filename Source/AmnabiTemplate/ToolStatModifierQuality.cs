using System;
using JetBrains.Annotations;
using RimWorld;
using Verse;

namespace Amnabi;

public class ToolStatModifierQuality
{
    [CanBeNull] public StatDef Stat;
    public float awful, poor, normal, good,excellent, masterwork, legendary;
    public float GetValue(QualityCategory qc)
        => qc switch
        {
            QualityCategory.Awful => awful,
            QualityCategory.Poor => poor,
            QualityCategory.Normal => normal,
            QualityCategory.Good => good,
            QualityCategory.Excellent => excellent,
            QualityCategory.Masterwork => masterwork,
            QualityCategory.Legendary => legendary,
            _ => throw new ArgumentOutOfRangeException(qc.ToString()),
        };
    public string ToStringAsFactorRange
        => GetValue(QualityCategory.Awful).ToStringByStyle(ToStringStyle.PercentZero)
           + " ~ "
           + GetValue(QualityCategory.Legendary).ToStringByStyle(ToStringStyle.PercentZero);
    
    
    public string ToStringAsOffsetRange
        => Stat!.Worker.ValueToString(GetValue(QualityCategory.Awful), finalized: false, ToStringNumberSense.Offset)
           + " ~ "
           + Stat.Worker.ValueToString(GetValue(QualityCategory.Legendary), finalized: false,
               ToStringNumberSense.Offset);
}