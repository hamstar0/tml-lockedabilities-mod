using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;


namespace LockedAbilities {
	public class LockedAbilitiesConfig : ModConfig {
		public override ConfigScope Mode => ConfigScope.ServerSide;


		////////////////

		[Label("Starting amount of available accessory slots")]
		[Slider()]
		[Range(0, 6)]
		[DefaultValue(1)]
		public int InitialAccessorySlots = 1;
	}
}
