using System.Collections.Generic;
using System.Xml;
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
        //harmony.PatchAll();
        harmony.Patch(
            AccessTools.Method(typeof(CompQuality), nameof(CompQuality.SetQuality)),
            null,
            new HarmonyMethod(typeof(Harmony_QualityAndDurability), nameof(SetQualityPatch)));
        harmony.Patch(
            AccessTools.DeclaredPropertyGetter(typeof(Thing), nameof(Thing.MaxHitPoints)),
            null,
            new HarmonyMethod(typeof(Harmony_QualityAndDurability), nameof(MaxHitPointsPatch)));
       
        Log.Warning("Starting ThingDef Initilisation");
            DefDatabase<ThingDef>.AddAllInMods();
            var thingDefs = DefDatabase<ThingDef>.AllDefsListForReading;
            for (var i = thingDefs.Count; i-- > 0;)
            {
                var thingDef = thingDefs[i];
               
			    
                if (thingDef.GetModExtension<StatExstention>() is { } extension)
                    extension.Initialize(thingDef);
              
            }
            Log.Clear();
            Log.Message("what is this " +DefDatabase<ThingDef>.GetNamed("Primitive_Knife").defName);
            Log.Message(DefDatabase<ThingDef>.GetNamed("Primitive_Knife").GetModExtension< StatExstention>() != null);
            DefDatabase<ThingDef>.GetNamed("Primitive_Knife").weaponTags.ForEach(X => Log.Message(X));
            Log.Message(DefDatabase<ThingDef>.GetNamed("Primitive_Knife"));

            // ThingDef.equippedStatOffsets


            // if (AccessTools.PropertyGetter(typeof(ThingDef), nameof(ThingDef.equippedStatOffsets)) == null)
            // {
            //     Log.Error("Issue with getting proportie");
            //     Log.Error(typeof(ThingDef).ToString());
            //     Log.Error(nameof(ThingDef.equippedStatOffsets));
            //    
            //     Log.Warning(AccessTools.DeclaredPropertyGetter(typeof(Thing), nameof(Thing.MaxHitPoints))
            //         .ToString());
            //     Log.Warning("Proporty: " + AccessTools.DeclaredPropertyGetter(typeof(Thing), nameof(Thing.def))
            //         ); 
            //     Log.Warning("Method: " + AccessTools.Method(typeof(Thing), nameof(Thing.def))
            //         ); 
            //     Log.Warning("Field : " + AccessTools.Field(typeof(Thing), nameof(Thing.def))); 
            //     Log.Warning("Prop Gett : " + AccessTools.PropertyGetter(typeof(Thing), nameof(Thing.def))); 
            //     Log.Warning("Prop Different Approch : " + AccessTools.("List<StatModifier>:equippedStatOffsets")); 
            // }

            // harmony.Patch(
            //     AccessTools.DeclaredPropertyGetter(typeof(Thing), nameof(Thing.MaxHitPoints)),
            //     null,
            //     new HarmonyMethod(typeof(Harmony_QualityAndDurability), nameof(MeleeQualityPatch)));
    }
    
    

    public static void SetQualityPatch(CompQuality __instance)
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

    public static void MeleeQualityPatch(Thing __instance, ref int __result)
    {
        Log.Message("Patching " + __instance.def.defName);
        if (!__instance.def.IsWeapon && __instance.def.equippedStatOffsets.Count < 1 )
        {
            Log.Message("Patching  " + __instance.def.defName + " Failed");
            return;
        }
        
        var qualityComp = __instance.TryGetComp<CompQuality>();
        var toolOfsets = __instance.def;
        if (qualityComp != null)
        {
            foreach (var ToolOfset in toolOfsets.equippedStatOffsets)
            {
                ToolOfset.value = (int)(ToolOfset.value * adjustSwitch(qualityComp.Quality));
            }

        }
        Log.Message("Patching  " + __instance.def.defName + " Succeded");

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

    public static void LoadDataFromXmlCustom(XmlNode xmlRoot)
    {
        
    }
}