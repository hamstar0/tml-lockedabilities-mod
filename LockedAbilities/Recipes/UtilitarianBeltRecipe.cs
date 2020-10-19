using LockedAbilities.Items.Accessories;
using System;
using Terraria.ID;
using Terraria.ModLoader;


namespace LockedAbilities.Recipes {
	class UtilitarianBeltRecipe : ModRecipe {
		public UtilitarianBeltRecipe() : base( LockedAbilitiesMod.Instance ) {
			var config = LockedAbilitiesConfig.Instance;

			this.AddTile( TileID.TinkerersWorkbench );

			if( config.Get<bool>( nameof(LockedAbilitiesConfig.BackBraceEnabled) ) ) {
				this.AddIngredient( ModContent.ItemType<BackBraceItem>() );
			}
			if( config.Get<bool>( nameof(LockedAbilitiesConfig.BootLacesEnabled) ) ) {
				this.AddIngredient( ModContent.ItemType<BootLacesItem>() );
			}
			if( config.Get<bool>( nameof(LockedAbilitiesConfig.FlyingCertificateEnabled) ) ) {
				this.AddIngredient( ModContent.ItemType<FlyingCertificateItem>() );
			}
			if( config.Get<bool>( nameof(LockedAbilitiesConfig.GrappleHarnessEnabled) ) ) {
				this.AddIngredient( ModContent.ItemType<GrappleHarnessItem>() );
			}
			if( config.Get<bool>( nameof(LockedAbilitiesConfig.GunPermitEnabled) ) ) {
				this.AddIngredient( ModContent.ItemType<GunPermitItem>() );
			}
			if( config.Get<bool>( nameof(LockedAbilitiesConfig.MountReinEnabled) ) ) {
				this.AddIngredient( ModContent.ItemType<MountReinItem>() );
			}
			if( config.Get<bool>( nameof(LockedAbilitiesConfig.SafetyHarnessEnabled) ) ) {
				this.AddIngredient( ModContent.ItemType<SafetyHarnessItem>() );
			}

			this.SetResult( ModContent.ItemType<UtilitarianBeltItem>() );
		}


		public override bool RecipeAvailable() {
			var config = LockedAbilitiesConfig.Instance;

			if( !config.Get<bool>( nameof(LockedAbilitiesConfig.UtilitarianBeltEnabled) ) ) {
				return false;
			}

			int itemIngredients = 0;

			if( config.Get<bool>( nameof(LockedAbilitiesConfig.BackBraceEnabled) ) ) {
				itemIngredients++;
			}
			if( config.Get<bool>( nameof(LockedAbilitiesConfig.BootLacesEnabled) ) ) {
				itemIngredients++;
			}
			if( config.Get<bool>( nameof(LockedAbilitiesConfig.FlyingCertificateEnabled) ) ) {
				itemIngredients++;
			}
			if( config.Get<bool>( nameof(LockedAbilitiesConfig.GrappleHarnessEnabled) ) ) {
				itemIngredients++;
			}
			if( config.Get<bool>( nameof(LockedAbilitiesConfig.GunPermitEnabled) ) ) {
				itemIngredients++;
			}
			if( config.Get<bool>( nameof(LockedAbilitiesConfig.MountReinEnabled) ) ) {
				itemIngredients++;
			}
			if( config.Get<bool>( nameof(LockedAbilitiesConfig.SafetyHarnessEnabled) ) ) {
				itemIngredients++;
			}

			return itemIngredients >= 2;
		}
	}
}
