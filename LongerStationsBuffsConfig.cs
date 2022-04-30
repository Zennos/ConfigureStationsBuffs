using System.ComponentModel;
using Terraria;
using Terraria.ModLoader.Config;

namespace LongerStationsBuffs
{
    public class LongerStationsBuffsConfig : ModConfig
    {
		[Label("Mod active")]
		[DefaultValue(true)]
		public bool ModEnabled;

		[Label("Duration of all buff stations")]
		[Range(0, 59940)]
		[Tooltip("In minutes. Default: 120 (2h)")]
		[DefaultValue(120)]
		public int BuffDuration;

		public CustomBuffsDurations customBuffDuration = new CustomBuffsDurations();

		public override ConfigScope Mode => ConfigScope.ServerSide;

		public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref string message)
		{
			if (!LongerStationsBuffs.IsPlayerLocalServerOwner(Main.player[whoAmI]))
			{
				message = "Only the server owner can change this config";
				return false;
			}
			return base.AcceptClientChanges(pendingConfig, whoAmI, ref message);
		}

		[BackgroundColor(85, 107, 47, 255)]
		[Label("Custom Duration For Each Station Instead: ")]
		public class CustomBuffsDurations
		{
			[Label("Enabled")]
			public bool enabled;

			[Label("[i:2999] Bewitching Table")]
			[Range(0, 59940)]
			[Tooltip("In minutes. Default: 10")]
			[DefaultValue(10)]
			public int BewitchingTable;

			[Label("[i:2177] Ammo Box")]
			[Range(0, 59940)]
			[Tooltip("In minutes. Default: 10")]
			[DefaultValue(10)]
			public int AmmoBox;

			[Label("[i:487] Crystal Ball")]
			[Range(0, 59940)]
			[Tooltip("In minutes. Default: 10")]
			[DefaultValue(10)]
			public int CrystalBall;

			[Label("[i:3198] Sharpening Station")]
			[Range(0, 59940)]
			[Tooltip("In minutes. Default: 10")]
			[DefaultValue(10)]
			public int SharpeningStation;

			[Label("[i:3750] Slice of Cake")]
			[Range(0, 59940)]
			[Tooltip("In minutes. Default: 2")]
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
				return enabled ? "Enabled" : "Disabled";
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
