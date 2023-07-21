using System.ComponentModel;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader.Config;

namespace LongerStationsBuffs
{
    public class LongerStationsBuffsConfig : ModConfig
    {

		[DefaultValue(true)]
		public bool ModEnabled;

		const string InterpolatedTooltip = "$Mods.LongerStationsBuffs.Configs.InMinuteTooltip";

        [TooltipKey(InterpolatedTooltip)]
		[TooltipArgs("120 (2h)")]
		[Range(0, 59940)]
		[DefaultValue(120)]
		public int BuffDuration;

		public CustomBuffsDurations customBuffDuration = new CustomBuffsDurations();

		public override ConfigScope Mode => ConfigScope.ServerSide;

		public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref string message)
		{
			if (!LongerStationsBuffs.IsPlayerLocalServerOwner(Main.player[whoAmI]))
			{
				message = Language.GetTextValue("Mods.LongerStationsBuffs.Configs.OnlyOwner");
				return false;
			}
			return base.AcceptClientChanges(pendingConfig, whoAmI, ref message);
		}

		[BackgroundColor(85, 107, 47, 255)]
		public class CustomBuffsDurations
		{
			public bool enabled;

            [TooltipKey(InterpolatedTooltip)]
            [TooltipArgs(10)]
            [Range(0, 59940)]
			[DefaultValue(10)]
			public int BewitchingTable;

            [TooltipKey(InterpolatedTooltip)]
            [TooltipArgs(10)]
            [Range(0, 59940)]
			[DefaultValue(10)]
			public int AmmoBox;

            [TooltipKey(InterpolatedTooltip)]
            [TooltipArgs(10)]
            [Range(0, 59940)]
			[DefaultValue(10)]
			public int CrystalBall;

            [TooltipKey(InterpolatedTooltip)]
            [TooltipArgs(10)]
            [Range(0, 59940)]
			[DefaultValue(10)]
			public int SharpeningStation;

            [TooltipKey(InterpolatedTooltip)]
            [TooltipArgs(2)]
            [Range(0, 59940)]
			[DefaultValue(2)]
			public int SliceOfCake;

			public CustomBuffsDurations()
			{
				enabled = false;
				BewitchingTable = 10;
				AmmoBox = 10;
				CrystalBall = 10;
				SharpeningStation = 10;
				SliceOfCake = 2;
			}

			public override string ToString()
			{
				return enabled ? 
					Language.GetTextValue("GameUI.Enabled") : 
					Language.GetTextValue("GameUI.Disabled");
			}

			public override bool Equals(object obj)
			{
				if (obj is CustomBuffsDurations other)
				{
					return enabled == other.enabled && BewitchingTable == other.BewitchingTable && AmmoBox == other.AmmoBox && CrystalBall == other.CrystalBall && SharpeningStation == other.SharpeningStation && SliceOfCake == other.SliceOfCake;
				}
				return base.Equals(obj);
			}

			public override int GetHashCode()
			{
				return new { enabled, BewitchingTable, AmmoBox, CrystalBall, SharpeningStation, SliceOfCake }.GetHashCode();
			}
		}
	}
}
