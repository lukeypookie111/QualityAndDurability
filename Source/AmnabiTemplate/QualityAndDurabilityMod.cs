using Mlie;
using UnityEngine;
using Verse;

namespace Amnabi;

[StaticConstructorOnStartup]


internal class QualityAndDurabilityMod : Mod
{
    
    
    /// <summary>
    ///     The instance of the settings to be read by the mod
    /// </summary>
    public static QualityAndDurabilityMod instance;

    private static string currentVersion;

    /// <summary>
    ///     The private settings
    /// </summary>
    private QualityAndDurabilitySettings settings;

    /// <summary>
    ///     Cunstructor
    /// </summary>
    /// <param name="content"></param>
    public QualityAndDurabilityMod(ModContentPack content) : base(content)
    {
        instance = this;
        currentVersion =
            VersionFromManifest.GetVersionFromModMetaData(content.ModMetaData);
    }

    /// <summary>
    ///     The instance-settings for the mod
    /// </summary>
    internal QualityAndDurabilitySettings Settings
    {
        get
        {
            if (settings == null)
            {
                settings = GetSettings<QualityAndDurabilitySettings>();
            }

            return settings;
        }
        set => settings = value;
    }

    /// <summary>
    ///     The title for the mod-settings
    /// </summary>
    /// <returns></returns>
    public override string SettingsCategory()
    {
        return "Quality and Durability";
    }

    /// <summary>
    ///     The settings-window
    ///     For more info: https://rimworldwiki.com/wiki/Modding_Tutorials/ModSettings
    /// </summary>
    /// <param name="rect"></param>
    public override void DoSettingsWindowContents(Rect rect)
    {
        var listing_Standard = new Listing_Standard();
        listing_Standard.Begin(rect);
        listing_Standard.Gap();
        listing_Standard.Label("QAD.QualityMultipliers".Translate());
        listing_Standard.Gap();
        Settings.Awful = Widgets.HorizontalSlider(listing_Standard.GetRect(20), Settings.Awful, 0, Settings.Poor,
            false, "QAD.Awful".Translate(Settings.Awful.ToString("0.00")), "0", Settings.Poor.ToString(), 0.01f);
        listing_Standard.Gap();
        Settings.Poor = Widgets.HorizontalSlider(listing_Standard.GetRect(20), Settings.Poor, Settings.Awful,
            Settings.Normal, false, "QAD.Poor".Translate(Settings.Poor.ToString("0.00")), Settings.Awful.ToString(),
            Settings.Normal.ToString(),
            0.01f);
        listing_Standard.Gap();
        Settings.Normal = Widgets.HorizontalSlider(listing_Standard.GetRect(20), Settings.Normal, Settings.Poor,
            Settings.Good, false, "QAD.Normal".Translate(Settings.Normal.ToString("0.00")), Settings.Poor.ToString(),
            Settings.Good.ToString(),
            0.01f);
        listing_Standard.Gap();
        Settings.Good = Widgets.HorizontalSlider(listing_Standard.GetRect(20), Settings.Good, Settings.Normal,
            Settings.Excellent, false, "QAD.Good".Translate(Settings.Good.ToString("0.00")), Settings.Normal.ToString(),
            Settings.Excellent.ToString(), 0.01f);
        listing_Standard.Gap();
        Settings.Excellent = Widgets.HorizontalSlider(listing_Standard.GetRect(20), Settings.Excellent,
            Settings.Good, Settings.Masterwork, false, "QAD.Excellent".Translate(Settings.Excellent.ToString("0.00")),
            Settings.Good.ToString(),
            Settings.Masterwork.ToString(), 0.01f);
        listing_Standard.Gap();
        Settings.Masterwork = Widgets.HorizontalSlider(listing_Standard.GetRect(20), Settings.Masterwork,
            Settings.Excellent, Settings.Legendary, false,
            "QAD.Masterwork".Translate(Settings.Masterwork.ToString("0.00")),
            Settings.Excellent.ToString(), Settings.Legendary.ToString(), 0.01f);
        listing_Standard.Gap();
        Settings.Legendary = Widgets.HorizontalSlider(listing_Standard.GetRect(20), Settings.Legendary,
            Settings.Masterwork, 10f, false, "QAD.Legendary".Translate(Settings.Legendary.ToString("0.00")),
            Settings.Masterwork.ToString(),
            "10", 0.01f);
        if (currentVersion != null)
        {
            listing_Standard.Gap();
            GUI.contentColor = Color.gray;
            listing_Standard.Label("QAD.ModVersion".Translate(currentVersion));
            GUI.contentColor = Color.white;
        }

        listing_Standard.End();
        Settings.Write();
    }
}