using HamstarHelpers.Helpers.Players;
using System;
using Terraria;
using Terraria.ModLoader;


namespace LockedAbilities.Items {
	class AccessoryAccessoryItem : ModItem, IAbilityAccessoryItem {
		public static int Width = 22;
		public static int Height = 18;



		////////////////

		public override void SetStaticDefaults() {
			var mymod = (LockedAbilitiesMod)this.mod;
			mymod.AddAbility( this );

			this.DisplayName.SetDefault( "Accessory Accessory" );
			this.Tooltip.SetDefault( "Allows equipping 2 other accessories" );
		}

		public override void SetDefaults() {
			this.item.width = AccessoryAccessoryItem.Width;
			this.item.height = AccessoryAccessoryItem.Height;
			this.item.maxStack = 1;
			this.item.value = Item.buyPrice( 0, 25, 0, 0 );
			this.item.rare = 3;
			this.item.accessory = true;
		}


		////////////////

		public bool TestItemDisabled( Player player, int slot, Item item ) {
			if( slot < PlayerItemHelpers.VanillaAccessorySlotFirst ) {
				return false;
			}
			return slot >= this.GetMaxAccessorySlot( player ).Value;
		}

		public bool TestItemEnabled( Player player, int slot, Item item ) {
			return true;
		}

		////

		public int? GetMaxAccessorySlot( Player player ) {
			return PlayerItemHelpers.VanillaAccessorySlotFirst + 2;
		}
	}
}
