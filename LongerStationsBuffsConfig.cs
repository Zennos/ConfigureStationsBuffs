using System.ComponentModel;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader.Config;

namespace LongerStationsBuffs
{
    public class LongerStationsBuffsConfig : ModConfig
    {
        [Header("$Mods.LongerStationsBuffs.Configs.Note")]
        [DefaultValue(true)]
        public bool ModEnabled;

		[DefaultValue(false)]
        public bool persistAfterDeath;

        const string InterpolatedTooltip = "$Mods.LongerStationsBuffs.Configs.InMinuteTooltip";

        [TooltipKey(InterpolatedTooltip)]
        [TooltipArgs(10)]
        [Range(0, 59940)]
        [DefaultValue(0)]
        public int BewitchingTable;

        [TooltipKey(InterpolatedTooltip)]
        [TooltipArgs(10)]
        [Range(0, 59940)]
        [DefaultValue(0)]
        public int AmmoBox;

        [TooltipKey(InterpolatedTooltip)]
        [TooltipArgs(10)]
        [Range(0, 59940)]
        [DefaultValue(0)]
        public int CrystalBall;

        [TooltipKey(InterpolatedTooltip)]
        [TooltipArgs(10)]
        [Range(0, 59940)]
        [DefaultValue(0)]
        public int SharpeningStation;

        [TooltipKey(InterpolatedTooltip)]
        [TooltipArgs(2)]
        [Range(0, 59940)]
        [DefaultValue(0)]
        public int SliceOfCake;

        public override ConfigScope Mode => ConfigScope.ServerSide;

		public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref NetworkText message)
		{
			if (!LongerStationsBuffs.IsPlayerLocalServerOwner(Main.player[whoAmI]))
			{
                message = NetworkText.FromLiteral(Language.GetTextValue("Mods.LongerStationsBuffs.Configs.OnlyOwner"));
                return false;
			}
			return base.AcceptClientChanges(pendingConfig, whoAmI, ref message);
		}

    }
}
