using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace LockedAbilities.Items {
	class GunPermitItem : ModItem, IAbilityAccessoryItem {
		public static int Width = 22;
		public static int Height = 18;



		////////////////

		public override void SetStaticDefaults() {
			var mymod = (LockedAbilitiesMod)this.mod;
			mymod.AddAbility( this );

			this.DisplayName.SetDefault( "Gun Permit" );
			this.Tooltip.SetDefault( "Allows equipping guns" );
		}

		public override void SetDefaults() {
			this.item.width = GunPermitItem.Width;
			this.item.height = GunPermitItem.Height;
			this.item.maxStack = 1;
			this.item.value = Item.buyPrice( 0, 25, 0, 0 );
			this.item.rare = 5;
			this.item.accessory = true;
		}


		////////////////

		public bool TestItemDisabled( Player player, int slot, Item item ) {
			return false;
		}

		public bool TestItemEnabled( Player player, int slot, Item item ) {
			if( item.ranged && item.useAmmo == AmmoID.Bullet ) {
				return true;
			}
			return false;
		}

		////

		public int? GetMaxAccessorySlot( Player player ) {
			return null;
		}
	}
}
