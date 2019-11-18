using System;
using Terraria;
using Terraria.ModLoader;


namespace LockedAbilities.Items {
	class DarkHeartPieceItem : ModItem {
		public const int Width = 18;
		public const int Height = 18;



		////////////////

		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Dark Heart Piece" );
			this.Tooltip.SetDefault( "A fragment of low-grade Demon Heart"
					+ '\n' + "Crafted into a Dark Heart"
					+ '\n' + "Dark Hearts unlocks sealed accessory slots" );
		}

		public override void SetDefaults() {
			this.item.width = DarkHeartPieceItem.Width;
			this.item.height = DarkHeartPieceItem.Height;
			this.item.consumable = true;
			this.item.maxStack = 30;
			this.item.value = Item.buyPrice( 0, 2, 50, 0 );
			this.item.rare = 3;
		}
	}
}
