using HamstarHelpers.Helpers.Players;
using System;
using Terraria;
using Terraria.ModLoader;


namespace LockedAbilities.Items {
	class BackBraceItem : ModItem, IAbilityAccessoryItem {
		public static int Width = 22;
		public static int Height = 18;



		////////////////

		public override void SetStaticDefaults() {
			var mymod = (LockedAbilitiesMod)this.mod;
			mymod.AddAbility( this );

			this.DisplayName.SetDefault( "Back Brace" );
			this.Tooltip.SetDefault( "Allows equipping heavy armor" );
		}

		public override void SetDefaults() {
			this.item.width = BackBraceItem.Width;
			this.item.height = BackBraceItem.Height;
			this.item.maxStack = 1;
			this.item.value = Item.buyPrice( 0, 10, 0, 0 );
			this.item.rare = 4;
			this.item.accessory = true;
		}


		////////////////

		public int? GetMaxAccessorySlot( Player player ) {
			return null;
		}

		////////////////

		public bool IsArmorItemAnAbility( Player player, int slot, Item item ) {
			if( slot >= 0 && slot < PlayerItemHelpers.VanillaAccessorySlotFirst ) {
				if( item.defense >= 4 ) {
					return true;
				}
			}
			return false;
		}

		public bool IsMiscItemAnAbility( Player player, Item item ) {
			return false;
		}

		public bool IsEquipItemAnAbility( Player player, Item item ) {
			return false;
		}
	}
}
