using Terraria;
using Terraria.ModLoader;

namespace LongerStationsBuffs
{
	public class LongerStationsBuffs : Mod
	{
		public override void Load()
		{
			On.Terraria.Player.AddBuff += Player_AddBuff;
		}

        private void Player_AddBuff(On.Terraria.Player.orig_AddBuff orig, Player self, int type, int timeToAdd, bool quiet, bool foodHack)
		{
			LongerStationsBuffsConfig modConfig = ModContent.GetInstance<LongerStationsBuffsConfig>();
			if (modConfig.ModEnabled)
			{
				bool changed = true;
				if (modConfig.customBuffDuration.enabled)
				{
					switch (type)
					{
						case Terraria.ID.BuffID.AmmoBox:
							timeToAdd = modConfig.customBuffDuration.AmmoBox;
							break;
						case Terraria.ID.BuffID.Bewitched:
							timeToAdd = modConfig.customBuffDuration.BewitchingTable;
							break;
						case Terraria.ID.BuffID.Clairvoyance:
							timeToAdd = modConfig.customBuffDuration.CrystalBall;
							break;
						case Terraria.ID.BuffID.Sharpened:
							timeToAdd = modConfig.customBuffDuration.SharpeningStation;
							break;
						case Terraria.ID.BuffID.SugarRush:
							timeToAdd = modConfig.customBuffDuration.SliceOfCake;
							break;
						default:
							changed = false;
							break;
					}
				}
				else
				{
					switch (type)
					{
						case Terraria.ID.BuffID.AmmoBox:
						case Terraria.ID.BuffID.Bewitched:
						case Terraria.ID.BuffID.Clairvoyance:
						case Terraria.ID.BuffID.Sharpened:
						case Terraria.ID.BuffID.SugarRush:
							timeToAdd = modConfig.BuffDuration;
							break;
						default:
							changed = false;
							break;
					}
				}
				if (changed)
				{
					timeToAdd = timeToAdd * 60 * 60;
				}
			}

			orig.Invoke(self, type, timeToAdd, quiet, foodHack);
		}

		public static bool IsPlayerLocalServerOwner(Player player)
		{
			if (Main.netMode == Terraria.ID.NetmodeID.MultiplayerClient)
			{
				return Netplay.Connection.Socket.GetRemoteAddress().IsLocalHost();
			}
			for (int plr = 0; plr < 255; plr++)
			{
				if (Netplay.Clients[plr].State == 10 && Main.player[plr] == player && Netplay.Clients[plr].Socket.GetRemoteAddress().IsLocalHost())
				{
					return true;
				}
			}
			return false;
		}
	}
}


