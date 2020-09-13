using HamstarHelpers.Helpers.Items.Attributes;
using System;
using Terraria;
using Terraria.ModLoader;


namespace LockedAbilities.Items.Accessories {
	public class GrappleHarnessItem : ModItem, IAbilityAccessoryItem {
		public const int Width = 22;
		public const int Height = 18;



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

		public int? GetAddedAccessorySlots( Player player ) {
			return null;
		}

		////////////////

		public bool EnablesArmorItem( Player player, int slot, Item item ) {
			return false;
		}

		public bool EnablesMiscItem( Player player, int slot, Item item ) {
			if( !LockedAbilitiesConfig.Instance.GrappleHarnessEnabled ) {
				return false;
			}

			if( ItemAttributeHelpers.IsGrapple( item ) ) {
				return true;
			}
			return false;
		}

		public bool EnablesEquipItem( Player player, Item item ) {
			return false;
		}

		////////////////

		public float WorldGenChestWeight( Chest chest ) {
			var config = LockedAbilitiesConfig.Instance;
			return config.Get<float>( nameof(LockedAbilitiesConfig.WorldGenChestImplantGrappleHarnessChance) );
		}
	}
}
