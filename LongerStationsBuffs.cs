using Terraria.ModLoader;

namespace LongerStationsBuffs
{
	public class LongerStationsBuffs : Mod
	{
        public override void Load()
        {
            On.Terraria.Player.AddBuff += Player_AddBuff;
        }

        private void Player_AddBuff(On.Terraria.Player.orig_AddBuff orig, Terraria.Player self, int type, int timeToAdd, bool quiet, bool foodHack)
        {
            switch (type)
            {
                case Terraria.ID.BuffID.AmmoBox:
                case Terraria.ID.BuffID.Bewitched:
                case Terraria.ID.BuffID.Clairvoyance:
                case Terraria.ID.BuffID.Sharpened:
                case Terraria.ID.BuffID.SugarRush:
                    timeToAdd = 432000; // 2 hours.
                    break;
                default:
                    break;
            }
            orig.Invoke(self, type, timeToAdd, quiet, foodHack);
        }
    }
}