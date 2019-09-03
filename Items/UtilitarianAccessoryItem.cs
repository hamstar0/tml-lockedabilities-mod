using HamstarHelpers.Helpers.Players;
using System;
using Terraria;
using Terraria.ModLoader;


namespace LockedAbilities.Items {
	class UtilitarianAccessoryItem : ModItem, IAbilityAccessoryItem {
		public static int Width = 22;
		public static int Height = 18;



		////////////////

		public override void SetStaticDefaults() {
			var mymod = (LockedAbilitiesMod)this.mod;
			mymod.AddAbility( this );

			this.DisplayName.SetDefault( "Utilitarian Accessory" );
			this.Tooltip.SetDefault( "Allows equipping 3 other accessories" );
		}

		public override void SetDefaults() {
			this.item.width = UtilitarianAccessoryItem.Width;
			this.item.height = UtilitarianAccessoryItem.Height;
			this.item.maxStack = 1;
			this.item.value = Item.buyPrice( 0, 50, 0, 0 );
			this.item.rare = 7;
			this.item.accessory = true;
		}


		////////////////

		public int? GetMaxArmorSlot( Player player ) {
			return PlayerItemHelpers.VanillaAccessorySlotFirst + 4;
		}

		////////////////

		public bool IsArmorItemAnAbility( Player player, int slot, Item item ) {
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
