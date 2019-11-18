using HamstarHelpers.Classes.UI.ModConfig;
using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;


namespace LockedAbilities {
	class MyFloatInputElement : FloatInputElement { }





	public class LockedAbilitiesConfig : ModConfig {
		public override ConfigScope Mode => ConfigScope.ServerSide;


		////////////////

		[Tooltip( "Starting amount of available accessory slots" )]
		[Slider()]
		[Range( 0, 20 )]
		[DefaultValue( 1 )]
		public int InitialAccessorySlots { get; set; } = 1;

		////


		[Tooltip( "Ability item % chance in world gen chest" )]
		[Range( 0f, 1f )]
		[DefaultValue( 0.5f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float WorldGenChestImplantChance { get; set; } = 0.5f;

		[Range( 0f, 1f )]
		[DefaultValue( 1f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float WorldGenChestImplantBackBraceChance { get; set; } = 1f;

		[Range( 0f, 1f )]
		[DefaultValue( 1f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float WorldGenChestImplantBootLacesChance { get; set; } = 1f;

		[Range( 0f, 1f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		[DefaultValue( 1f )]
		public float WorldGenChestImplantFlyingCertificateChance { get; set; } = 1f;

		[Range( 0f, 1f )]
		[DefaultValue( 1f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float WorldGenChestImplantGrappleHarnessChance { get; set; } = 1f;

		[Range( 0f, 1f )]
		[DefaultValue( 1f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float WorldGenChestImplantGunPermitChance { get; set; } = 1f;

		[Range( 0f, 1f )]
		[DefaultValue( 1f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float WorldGenChestImplantMountReinChance { get; set; } = 1f;

		[Range( 0f, 1f )]
		[DefaultValue( 1f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float WorldGenChestImplantSafetyHarnessChance { get; set; } = 1f;

		[Range( 0f, 1f )]
		[DefaultValue( 1f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float WorldGenChestImplantDarkHeartPieceChance { get; set; } = 1f;

		////

		[DefaultValue( true )]
		public bool IsBackBraceItemFindable { get; set; } = true;

		[Range( 0, 99 )]
		[DefaultValue( 4 )]
		public int DarkHeartPiecesPerDarkHeart { get; set; } = 4;
	}
}
