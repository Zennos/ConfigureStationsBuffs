using Microsoft.CodeAnalysis.CSharp.Syntax;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LongerStationsBuffs
{
	public class LongerStationsBuffs : Mod
	{

		bool isInfinite = true;

		public override void Load()
		{
            Terraria.On_Player.AddBuff += Player_AddBuff;
        }

        public override void Unload()
        {
			SetInfiniteTimeFromBuffs(false);
        }

        private void Player_AddBuff(Terraria.On_Player.orig_AddBuff orig, Player self, int type, int timeToAdd, bool quiet, bool foodHack)
		{
			LongerStationsBuffsConfig modConfig = ModContent.GetInstance<LongerStationsBuffsConfig>();

            if (modConfig.ModEnabled)
			{
				CheckBuff(ref timeToAdd, type, BuffID.AmmoBox, modConfig.AmmoBox, modConfig.persistAfterDeath);
				CheckBuff(ref timeToAdd, type, BuffID.Bewitched, modConfig.BewitchingTable, modConfig.persistAfterDeath);
				CheckBuff(ref timeToAdd, type, BuffID.Clairvoyance, modConfig.CrystalBall, modConfig.persistAfterDeath);
				CheckBuff(ref timeToAdd, type, BuffID.Sharpened, modConfig.SharpeningStation, modConfig.persistAfterDeath);
				CheckBuff(ref timeToAdd, type, BuffID.SugarRush, modConfig.SliceOfCake, modConfig.persistAfterDeath);
			} else if (isInfinite)
			{
				SetInfiniteTimeFromBuffs(true, false);
			}

			orig.Invoke(self, type, timeToAdd, quiet, foodHack);
		}

		public void CheckBuff(ref int timeToAdd, int buffId, int configBuffId, int newTimeInMin, bool persistAfterDeath = false)
		{
			if (buffId != configBuffId) return;
			if (newTimeInMin > 0)
			{
				timeToAdd = newTimeInMin * 60 * 60;
				ChangeInfiniteBuffState(buffId, false, persistAfterDeath);
			}
            else
            {
				ChangeInfiniteBuffState(buffId, buffId == BuffID.SugarRush ? false : true, persistAfterDeath);
			}
		}

		public void ChangeInfiniteBuffState(int buffId, bool state, bool persistAfterDeath = false)
		{
            BuffID.Sets.TimeLeftDoesNotDecrease[buffId] = state;
            Main.buffNoTimeDisplay[buffId] = state;
            Main.persistentBuff[buffId] = persistAfterDeath;
        }

        public void SetInfiniteTimeFromBuffs(bool state, bool persistAfterDeath = false)
		{
			var buffIds = new [] { BuffID.AmmoBox, BuffID.Bewitched, BuffID.Clairvoyance, BuffID.Sharpened, BuffID.Clairvoyance };

			for (int i = 0; i < buffIds.Length; i++)
			{
				var buffId = buffIds[i];
				BuffID.Sets.TimeLeftDoesNotDecrease[buffId] = state;
				Main.buffNoTimeDisplay[buffId] = state;
                Main.persistentBuff[buffId] = persistAfterDeath;
            }

            this.isInfinite = state;

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


