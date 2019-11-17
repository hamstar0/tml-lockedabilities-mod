using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;


namespace LockedAbilities {
	public class LockedAbilitiesConfig : ModConfig {
		public override ConfigScope Mode => ConfigScope.ServerSide;


		////////////////

		[Label( "Starting amount of available accessory slots" )]
		[Slider()]
		[Range( 0, 6 )]
		[DefaultValue( 1 )]
		public int InitialAccessorySlots { get; set; } = 1;

		[Label( "Ability accessory % chance in chest (needs Chest Implants mod)" )]
		[Range( 0f, 1f )]
		[DefaultValue( 0.5f )]
		public float WorldGenChestImplantChance { get; set; }

		////

		public bool IsBackBraceItemFindable { get; set; } = true;
	}
}
