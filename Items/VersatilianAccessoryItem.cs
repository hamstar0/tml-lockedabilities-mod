using HamstarHelpers.Helpers.Players;
using System;
using Terraria;
using Terraria.ModLoader;


namespace LockedAbilities.Items {
	class VersatilianAccessoryItem : ModItem, IAbilityAccessoryItem {
		public static int Width = 22;
		public static int Height = 18;



		////////////////

		public override void SetStaticDefaults() {
			var mymod = (LockedAbilitiesMod)this.mod;
			mymod.AddAbility( this );

			this.DisplayName.SetDefault( "Versatilian Accessory" );
			this.Tooltip.SetDefault( "Allows equipping 4 other accessories" );
		}

		public override void SetDefaults() {
			this.item.width = VersatilianAccessoryItem.Width;
			this.item.height = VersatilianAccessoryItem.Height;
			this.item.maxStack = 1;
			this.item.value = Item.buyPrice( 0, 75, 0, 0 );
			this.item.rare = 8;
			this.item.accessory = true;
		}


		////////////////

		public bool TestItemDisabled( Player player, int slot, Item item ) {
			return false;
		}

		public bool TestItemEnabled( Player player, int slot, Item item ) {
			return true;
		}

		////

		public int? GetMaxAccessorySlot( Player player ) {
			return PlayerItemHelpers.VanillaAccessorySlotFirst + 4;
		}
	}
}
