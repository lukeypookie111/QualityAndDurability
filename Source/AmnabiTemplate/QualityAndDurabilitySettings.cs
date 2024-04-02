using Verse;

namespace Amnabi;

/// <summary>
///     Definition of the settings for the mod
/// </summary>
internal class QualityAndDurabilitySettings : ModSettings
{
    public float Awful = 0.5f;
    public float Excellent = 1.5f;
    public float Good = 1.25f;
    public float Legendary = 5f;
    public float Masterwork = 2.5f;
    public float Normal = 1f;
    public float Poor = 0.75f;

    /// <summary>
    ///     Saving and loading the values
    /// </summary>
    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref Awful, "Awful", 0.5f);
        Scribe_Values.Look(ref Poor, "Poor", 0.75f);
        Scribe_Values.Look(ref Normal, "Normal", 1f);
        Scribe_Values.Look(ref Good, "Good", 1.25f);
        Scribe_Values.Look(ref Excellent, "Excellent", 1.5f);
        Scribe_Values.Look(ref Masterwork, "Masterwork", 2.5f);
        Scribe_Values.Look(ref Legendary, "Legendary", 5f);
    }
}