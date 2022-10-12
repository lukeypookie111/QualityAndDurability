using HarmonyLib;
using RimWorld;
using Verse;

namespace Amnabi;

[StaticConstructorOnStartup]
public static class Harmony_QualityAndDurability
{
    static Harmony_QualityAndDurability()
    {
        var harmony = new Harmony("Amnabi.QualityAndDurability");
        harmony.PatchAll();
        harmony.Patch(
            AccessTools.Method(typeof(CompQuality), "SetQuality"),
            null,
            new HarmonyMethod(typeof(Harmony_QualityAndDurability), nameof(SetQualityPatch)));
        harmony.Patch(
            AccessTools.DeclaredPropertyGetter(typeof(Thing), "MaxHitPoints"),
            null,
            new HarmonyMethod(typeof(Harmony_QualityAndDurability), nameof(MaxHitPointsPatch)));
    }

    public static void SetQualityPatch(QualityCategory q, ArtGenerationContext source, CompQuality __instance)
    {
        __instance.parent.HitPoints = __instance.parent.MaxHitPoints;
    }

    public static void MaxHitPointsPatch(Thing __instance, ref int __result)
    {
        if (!__instance.def.IsWeapon && !__instance.def.IsApparel)
        {
            return;
        }

        var qualityComp = __instance.TryGetComp<CompQuality>();
        if (qualityComp != null)
        {
            __result = (int)(__result * adjustSwitch(qualityComp.Quality));
        }
    }

    public static float adjustSwitch(QualityCategory q)
    {
        switch (q)
        {
            case QualityCategory.Awful:
            {
                return QualityAndDurabilityMod.instance.Settings.Awful;
            }
            case QualityCategory.Poor:
            {
                return QualityAndDurabilityMod.instance.Settings.Poor;
            }
            case QualityCategory.Normal:
            {
                return QualityAndDurabilityMod.instance.Settings.Normal;
            }
            case QualityCategory.Good:
            {
                return QualityAndDurabilityMod.instance.Settings.Good;
            }
            case QualityCategory.Excellent:
            {
                return QualityAndDurabilityMod.instance.Settings.Excellent;
            }
            case QualityCategory.Masterwork:
            {
                return QualityAndDurabilityMod.instance.Settings.Masterwork;
            }
            case QualityCategory.Legendary:
            {
                return QualityAndDurabilityMod.instance.Settings.Legendary;
            }
        }

        return 1.0f;
    }
}