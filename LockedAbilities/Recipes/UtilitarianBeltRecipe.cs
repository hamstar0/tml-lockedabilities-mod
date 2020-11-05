using System;
using Terraria.ID;
using Terraria.ModLoader;
using LockedAbilities.Items.Accessories;


namespace LockedAbilities.Recipes {
	class UtilitarianBeltRecipe : ModRecipe {
		public UtilitarianBeltRecipe() : base( LockedAbilitiesMod.Instance ) {
			var config = LockedAbilitiesConfig.Instance;

			this.AddTile( TileID.TinkerersWorkbench );

			if( config.Get<bool>( nameof(config.BackBraceEnabled) ) ) {
				this.AddIngredient( ModContent.ItemType<BackBraceItem>() );
			}
			if( config.Get<bool>( nameof(config.BootLacesEnabled) ) ) {
				this.AddIngredient( ModContent.ItemType<BootLacesItem>() );
			}
			if( config.Get<bool>( nameof(config.FlyingCertificateEnabled) ) ) {
				this.AddIngredient( ModContent.ItemType<FlyingCertificateItem>() );
			}
			if( config.Get<bool>( nameof(config.GrappleHarnessEnabled) ) ) {
				this.AddIngredient( ModContent.ItemType<GrappleHarnessItem>() );
			}
			if( config.Get<bool>( nameof(config.GunPermitEnabled) ) ) {
				this.AddIngredient( ModContent.ItemType<GunPermitItem>() );
			}
			if( config.Get<bool>( nameof(config.MountReinEnabled) ) ) {
				this.AddIngredient( ModContent.ItemType<MountReinItem>() );
			}
			if( config.Get<bool>( nameof(config.SafetyHarnessEnabled) ) ) {
				this.AddIngredient( ModContent.ItemType<SafetyHarnessItem>() );
			}

			this.SetResult( ModContent.ItemType<UtilitarianBeltItem>() );
		}


		public override bool RecipeAvailable() {
			var config = LockedAbilitiesConfig.Instance;

			if( !config.Get<bool>( nameof(config.UtilitarianBeltEnabled) ) ) {
				return false;
			}

			int itemIngredients = 0;

			if( config.Get<bool>( nameof(config.BackBraceEnabled) ) ) {
				itemIngredients++;
			}
			if( config.Get<bool>( nameof(config.BootLacesEnabled) ) ) {
				itemIngredients++;
			}
			if( config.Get<bool>( nameof(config.FlyingCertificateEnabled) ) ) {
				itemIngredients++;
			}
			if( config.Get<bool>( nameof(config.GrappleHarnessEnabled) ) ) {
				itemIngredients++;
			}
			if( config.Get<bool>( nameof(config.GunPermitEnabled) ) ) {
				itemIngredients++;
			}
			if( config.Get<bool>( nameof(config.MountReinEnabled) ) ) {
				itemIngredients++;
			}
			if( config.Get<bool>( nameof(config.SafetyHarnessEnabled) ) ) {
				itemIngredients++;
			}

			return itemIngredients >= 2;
		}
	}
}
