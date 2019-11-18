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

		////


		[Label( "Ability accessory % chance in chest (needs Chest Implants mod)" )]
		[Range( 0f, 1f )]
		[DefaultValue( 0.5f )]
		public float WorldGenChestImplantChance { get; set; }

		[Range( 0f, 1f )]
		[DefaultValue( 1f )]
		public float WorldGenChestImplantBackBraceChance { get; set; } = 0f;

		[Range( 0f, 1f )]
		[DefaultValue( 1f )]
		public float WorldGenChestImplantBootLacesChance { get; set; } = 0f;

		[Range( 0f, 1f )]
		[DefaultValue( 1f )]
		public float WorldGenChestImplantFlyingCertificateChance { get; set; } = 0f;

		[Range( 0f, 1f )]
		[DefaultValue( 1f )]
		public float WorldGenChestImplantGrappleHarnessChance { get; set; } = 0f;

		[Range( 0f, 1f )]
		[DefaultValue( 1f )]
		public float WorldGenChestImplantGunPermitChance { get; set; } = 0f;

		[Range( 0f, 1f )]
		[DefaultValue( 1f )]
		public float WorldGenChestImplantMountReinChance { get; set; } = 0f;

		[Range( 0f, 1f )]
		[DefaultValue( 1f )]
		public float WorldGenChestImplantSafetyHarnessChance { get; set; } = 0f;

		[Range( 0f, 1f )]
		[DefaultValue( 1f )]
		public float WorldGenChestImplantDarkHeartPieceChance { get; set; } = 0f;

		////

		[DefaultValue( true )]
		public bool IsBackBraceItemFindable { get; set; } = true;

		[Range( 0, 99 )]
		[DefaultValue( 4 )]
		public int DarkHeartPiecesPerDarkHeart { get; set; } = 4;
	}
}
