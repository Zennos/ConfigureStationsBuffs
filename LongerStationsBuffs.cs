using log4net.Repository.Hierarchy;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace LongerStationsBuffs
{
	public class LongerStationsBuffs : Mod
	{

		bool anyBuffChanged = false;

		public override void Load()
		{
            Terraria.On_Player.AddBuff += Player_AddBuff;
        }

        public override void Unload()
        {
			if (!anyBuffChanged) return;
			ChangeAllBuffsToDefault();
        }

		public void ChangeAllBuffsToDefault()
		{
			ChangeBuff(BuffID.Bewitched, true, false);
			ChangeBuff(BuffID.AmmoBox, true, false);
			ChangeBuff(BuffID.Clairvoyance, true, false);
			ChangeBuff(BuffID.Sharpened, true, false);
			ChangeBuff(BuffID.SugarRush, false, false);
			ChangeBuff(BuffID.AmmoBox, true, false);
			ChangeBuff(BuffID.WarTable, true, false);

            anyBuffChanged = false;
        }

        private void Player_AddBuff(Terraria.On_Player.orig_AddBuff orig, Player self, int type, int timeToAdd, bool quiet, bool foodHack)
		{
            LongerStationsBuffsConfig modConfig = ModContent.GetInstance<LongerStationsBuffsConfig>();

			if (!modConfig.ModEnabled)
			{
				if (anyBuffChanged) ChangeAllBuffsToDefault();

                orig.Invoke(self, type, timeToAdd, quiet, foodHack);
                return;
			}

			int newTime = 0;

            switch (type)
            {
                case BuffID.Bewitched:
                    ChangeBuff(type, infinite: modConfig.BewitchingTable == 0, modConfig.persistAfterDeath);
                    newTime = modConfig.BewitchingTable * 60 * 60;
                    break;
                case BuffID.AmmoBox:
					ChangeBuff(type, infinite: modConfig.AmmoBox == 0, modConfig.persistAfterDeath);
					newTime = modConfig.AmmoBox * 60 * 60;
                    break;
                case BuffID.Clairvoyance:
                    ChangeBuff(type, infinite: modConfig.CrystalBall == 0, modConfig.persistAfterDeath);
                    newTime = modConfig.CrystalBall * 60 * 60;
                    break;
                case BuffID.Sharpened:
                    ChangeBuff(type, infinite: modConfig.SharpeningStation == 0, modConfig.persistAfterDeath);
                    newTime = modConfig.SharpeningStation * 60 * 60;
                    break;
                case BuffID.SugarRush:
                    ChangeBuff(type, infinite: modConfig.SliceOfCake == 0, modConfig.persistAfterDeath);
                    newTime = modConfig.SliceOfCake * 60 * 60;
                    break;
                case BuffID.WarTable:
                    ChangeBuff(type, infinite: modConfig.WarTable == 0, modConfig.persistAfterDeath);
                    newTime = modConfig.WarTable * 60 * 60;
                    break;
                default:
                    break;
			}

			if (newTime > 0) {
				anyBuffChanged = true; 
				timeToAdd = newTime; 
			}

            orig.Invoke(self, type, timeToAdd, quiet, foodHack);
		}

		public void ChangeBuff(int buffId, bool infinite, bool persistAfterDeath)
		{
            BuffID.Sets.TimeLeftDoesNotDecrease[buffId] = infinite;
            Main.buffNoTimeDisplay[buffId] = infinite;
            Main.persistentBuff[buffId] = persistAfterDeath;

			anyBuffChanged = true;
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


