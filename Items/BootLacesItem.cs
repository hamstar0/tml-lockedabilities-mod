using HamstarHelpers.Helpers.Items.Attributes;
using System;
using Terraria;
using Terraria.ModLoader;


namespace LockedAbilities.Items {
	class BootLacesItem : ModItem, IAbilityAccessoryItem {
		public static int Width = 22;
		public static int Height = 18;



		////////////////

		public override void SetStaticDefaults() {
			var mymod = (LockedAbilitiesMod)this.mod;
			mymod.AddAbility( this );

			this.DisplayName.SetDefault( "Boot Laces" );
			this.Tooltip.SetDefault( "Allows equipping shoe accessories" );
		}

		public override void SetDefaults() {
			this.item.width = BootLacesItem.Width;
			this.item.height = BootLacesItem.Height;
			this.item.maxStack = 1;
			this.item.value = Item.buyPrice( 0, 10, 0, 0 );
			this.item.rare = 4;
			this.item.accessory = true;
		}


		////////////////

		public int? GetMaxArmorSlot( Player player ) {
			return null;
		}

		////////////////

		public bool IsArmorItemAnAbility( Player player, int slot, Item item ) {
			if( item.shoeSlot != -1 && item.accessory && !item.vanity ) {
				if( item.handOnSlot == -1 && item.handOffSlot == -1 && item.waistSlot == -1 ) {
					return true;
				}
			}
			return false;
		}

		public bool IsMiscItemAnAbility( Player player, int slot, Item item ) {
			return false;
		}

		public bool IsEquipItemAnAbility( Player player, Item item ) {
			return false;
		}
	}
}
