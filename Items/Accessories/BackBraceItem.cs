using HamstarHelpers.Helpers.Players;
using System;
using Terraria;
using Terraria.ModLoader;


namespace LockedAbilities.Items.Accessories {
	public class BackBraceItem : ModItem, IAbilityAccessoryItem {
		public const int Width = 22;
		public const int Height = 18;



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

		public int? GetAddedAccessorySlots( Player player ) {
			return null;
		}

		////////////////

		public bool EnablesArmorItem( Player player, int slot, Item item ) {
			if( !LockedAbilitiesConfig.Instance.BackBraceEnabled ) {
				return false;
			}

			if( slot >= 0 && slot < PlayerItemHelpers.VanillaAccessorySlotFirst ) {
				if( item.defense >= 4 ) {
					return true;
				}
			}
			return false;
		}

		public bool EnablesMiscItem( Player player, int slot, Item item ) {
			return false;
		}

		public bool EnablesEquipItem( Player player, Item item ) {
			return false;
		}

		////////////////

		public float WorldGenChestWeight( Chest chest ) {
			return LockedAbilitiesConfig.Instance.WorldGenChestImplantBackBraceChance;
		}
	}
}
