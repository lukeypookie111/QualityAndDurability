using System.Collections.Generic;
using JetBrains.Annotations;
using RimWorld;
using Verse;

namespace Amnabi;

public class StatExstention: DefModExtension
{
    [CanBeNull]public List<StatModifier> itemStatFactors, itemStatOffsets;
	
    [CanBeNull]public List<StatModifierQuality> itemStatFactorsByQuality, itemStatOffsetsByQuality;
}