using HamstarHelpers.Classes.UI.ModConfig;
using HamstarHelpers.Services.Configs;
using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;


namespace LockedAbilities {
	class MyFloatInputElement : FloatInputElement { }



	
	public class LockedAbilitiesConfig : StackableModConfig {
		public static LockedAbilitiesConfig Instance => ModConfigStack.GetMergedConfigs<LockedAbilitiesConfig>();



		////////////////

		public override ConfigScope Mode => ConfigScope.ServerSide;


		////////////////

		[Tooltip( "Starting amount of available accessory slots" )]
		[Slider()]
		[Range( -1, 20 )]
		[DefaultValue( 2 )]
		public int InitialAccessorySlots { get; set; } = 2;


		////

		[DefaultValue( true )]
		public bool BackBraceEnabled { get; set; } = true;

		[DefaultValue( true )]
		public bool BootLacesEnabled { get; set; } = true;

		[DefaultValue( true )]
		public bool FlyingCertificateEnabled { get; set; } = true;

		[DefaultValue( true )]
		public bool GrappleHarnessEnabled { get; set; } = true;

		[DefaultValue( true )]
		public bool GunPermitEnabled { get; set; } = true;

		[DefaultValue( true )]
		public bool MountReinEnabled { get; set; } = true;

		[DefaultValue( true )]
		public bool SafetyHarnessEnabled { get; set; } = true;


		////

		[Header( "Ability item % chance in world gen chest" )]
		[Range( 0f, 1f )]
		[DefaultValue( 0.25f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float WorldGenChestImplantChance { get; set; } = 0.25f;
		
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

		[Range( 0, 99 )]
		[DefaultValue( 4 )]
		public int DarkHeartPiecesPerDarkHeart { get; set; } = 4;



		////////////////

		public void Clear() {
			this.InitialAccessorySlots = -1;
			this.WorldGenChestImplantChance = 0f;
			this.WorldGenChestImplantBackBraceChance = 0f;
			this.WorldGenChestImplantBootLacesChance = 0f;
			this.WorldGenChestImplantFlyingCertificateChance = 0f;
			this.WorldGenChestImplantGrappleHarnessChance = 0f;
			this.WorldGenChestImplantGunPermitChance = 0f;
			this.WorldGenChestImplantMountReinChance = 0f;
			this.WorldGenChestImplantSafetyHarnessChance = 0f;
			this.WorldGenChestImplantDarkHeartPieceChance = 0f;
			this.DarkHeartPiecesPerDarkHeart = 0;
		}
	}
}
