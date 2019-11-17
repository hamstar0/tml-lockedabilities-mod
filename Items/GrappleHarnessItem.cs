using HamstarHelpers.Helpers.Items.Attributes;
using System;
using Terraria;
using Terraria.ModLoader;


namespace LockedAbilities.Items {
	class GrappleHarnessItem : ModItem, IAbilityAccessoryItem {
		public static int Width = 22;
		public static int Height = 18;



		////////////////

		public override void SetStaticDefaults() {
			var mymod = (LockedAbilitiesMod)this.mod;
			mymod.AddAbility( this );

			this.DisplayName.SetDefault( "Grapple Harness" );
			this.Tooltip.SetDefault( "Allows equipping grappling items" );
		}

		public override void SetDefaults() {
			this.item.width = GrappleHarnessItem.Width;
			this.item.height = GrappleHarnessItem.Height;
			this.item.maxStack = 1;
			this.item.value = Item.buyPrice( 0, 10, 0, 0 );
			this.item.rare = 5;
			this.item.accessory = true;
		}


		////////////////

		public int? GetMaxArmorSlot( Player player ) {
			return null;
		}

		////////////////

		public bool IsArmorItemAnAbility( Player player, int slot, Item item ) {
			return false;
		}

		public bool IsMiscItemAnAbility( Player player, int slot, Item item ) {
			if( ItemAttributeHelpers.IsGrapple( item ) ) {
				return true;
			}
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
