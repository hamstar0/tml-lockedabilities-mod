using HamstarHelpers.Helpers.Players;
using HamstarHelpers.Helpers.TModLoader;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace LockedAbilities.Items.Consumable {
	public class DarkHeartItem : ModItem {
		public const int Width = 24;
		public const int Height = 24;



		////////////////

		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Dark Heart" );
			this.Tooltip.SetDefault( "A low-grade Demon Heart"
					+ '\n' + "Unlocks a sealed accessory slot" );
		}

		public override void SetDefaults() {
			this.item.width = DarkHeartItem.Width;
			this.item.height = DarkHeartItem.Height;
			this.item.consumable = true;
			this.item.useTime = 17;
			this.item.useAnimation = 17;
			this.item.useTurn = true;
			this.item.useStyle = 2;
			this.item.UseSound = SoundID.Item3;
			this.item.maxStack = 30;
			this.item.value = Item.buyPrice( 0, 10, 0, 0 );
			this.item.rare = 3;
		}

		////////////////

		public override bool UseItem( Player player ) {
			if( player.itemAnimation > 0 && player.itemTime == 0 ) {
				player.itemTime = item.useTime;
				return true;
			}
			return base.UseItem( player );
		}

		public override bool ConsumeItem( Player player ) {
			var myplayer = TmlHelpers.SafelyGetModPlayer<LockedAbilitiesPlayer>( player );
			int vanillaMaxAcc = PlayerItemHelpers.GetCurrentVanillaMaxAccessories( player );
			bool canIncreaseAccSlots = myplayer.InternalAllowedAccessorySlots >= 0
				&& myplayer.InternalAllowedAccessorySlots < vanillaMaxAcc;

			if( canIncreaseAccSlots ) {
				myplayer.IncreaseAllowedAccessorySlots();
			}
			return canIncreaseAccSlots;
		}


		////////////////

		public override void AddRecipes() {
			var myrecipe = new DarkHeartItemRecipe( this );
			myrecipe.AddRecipe();
		}
	}




	class DarkHeartItemRecipe : ModRecipe {
		public DarkHeartItemRecipe( DarkHeartItem myitem ) : base( myitem.mod ) {
			var config = LockedAbilitiesConfig.Instance;
			int pieces = config.Get<int>( nameof(LockedAbilitiesConfig.DarkHeartPiecesPerDarkHeart) );

			this.AddTile( TileID.DemonAltar );
			this.AddIngredient( ModContent.GetInstance<DarkHeartPieceItem>(), pieces );

			this.SetResult( myitem, 1 );
		}


		public override bool RecipeAvailable() {
			var config = LockedAbilitiesConfig.Instance;

			return config.Get<int>( nameof(LockedAbilitiesConfig.DarkHeartPiecesPerDarkHeart) ) > 0;
		}
	}
}
