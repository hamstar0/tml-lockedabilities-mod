using HamstarHelpers.Helpers.Players;
using System;
using Terraria;
using Terraria.ModLoader;


namespace LockedAbilities.Items {
	class ProficianAccessoryItem : ModItem, IAbilityAccessoryItem {
		public static int Width = 22;
		public static int Height = 18;



		////////////////

		public override void SetStaticDefaults() {
			var mymod = (LockedAbilitiesMod)this.mod;
			mymod.AddAbility( this );

			this.DisplayName.SetDefault( "Profician Accessory" );
			this.Tooltip.SetDefault( "Allows equipping 5 other accessories" );
		}

		public override void SetDefaults() {
			this.item.width = ProficianAccessoryItem.Width;
			this.item.height = ProficianAccessoryItem.Height;
			this.item.maxStack = 1;
			this.item.value = Item.buyPrice( 1, 0, 0, 0 );
			this.item.rare = 10;
			this.item.accessory = true;
		}


		////////////////

		public int? GetMaxArmorSlot( Player player ) {
			return PlayerItemHelpers.VanillaAccessorySlotFirst + 6;
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

		////////////////

		public float WorldGenChestWeight( Chest chest ) {
			return 1f;
		}
	}
}
