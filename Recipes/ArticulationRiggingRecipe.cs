using LockedAbilities.Items.Accessories;
using System;
using Terraria.ID;
using Terraria.ModLoader;


namespace LockedAbilities.Recipes {
	class ArticulationRiggingRecipe : ModRecipe {
		public ArticulationRiggingRecipe() : base( LockedAbilitiesMod.Instance ) {
			this.AddTile( TileID.TinkerersWorkbench );

			if( LockedAbilitiesConfig.Instance.BackBraceEnabled ) {
				this.AddIngredient( ModContent.ItemType<BackBraceItem>() );
			}
			if( LockedAbilitiesConfig.Instance.BootLacesEnabled ) {
				this.AddIngredient( ModContent.ItemType<BootLacesItem>() );
			}
			if( LockedAbilitiesConfig.Instance.FlyingCertificateEnabled ) {
				this.AddIngredient( ModContent.ItemType<FlyingCertificateItem>() );
			}
			if( LockedAbilitiesConfig.Instance.GrappleHarnessEnabled ) {
				this.AddIngredient( ModContent.ItemType<GrappleHarnessItem>() );
			}
			if( LockedAbilitiesConfig.Instance.GunPermitEnabled ) {
				this.AddIngredient( ModContent.ItemType<GunPermitItem>() );
			}
			if( LockedAbilitiesConfig.Instance.MountReinEnabled ) {
				this.AddIngredient( ModContent.ItemType<MountReinItem>() );
			}
			if( LockedAbilitiesConfig.Instance.SafetyHarnessEnabled ) {
				this.AddIngredient( ModContent.ItemType<SafetyHarnessItem>() );
			}

			this.SetResult( ModContent.ItemType<ArticulationRiggingItem>() );
		}


		public override bool RecipeAvailable() {
			if( !LockedAbilitiesConfig.Instance.ArticulationRiggingEnabled ) {
				return false;
			}

			int itemIngredients = 0;

			if( LockedAbilitiesConfig.Instance.BackBraceEnabled ) {
				itemIngredients++;
			}
			if( LockedAbilitiesConfig.Instance.BootLacesEnabled ) {
				itemIngredients++;
			}
			if( LockedAbilitiesConfig.Instance.FlyingCertificateEnabled ) {
				itemIngredients++;
			}
			if( LockedAbilitiesConfig.Instance.GrappleHarnessEnabled ) {
				itemIngredients++;
			}
			if( LockedAbilitiesConfig.Instance.GunPermitEnabled ) {
				itemIngredients++;
			}
			if( LockedAbilitiesConfig.Instance.MountReinEnabled ) {
				itemIngredients++;
			}
			if( LockedAbilitiesConfig.Instance.SafetyHarnessEnabled ) {
				itemIngredients++;
			}

			return itemIngredients >= 2;
		}
	}
}
