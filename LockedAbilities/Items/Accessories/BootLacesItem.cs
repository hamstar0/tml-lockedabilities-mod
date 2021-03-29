using System;
using Terraria;
using Terraria.ModLoader;


namespace LockedAbilities.Items.Accessories {
	public class BootLacesItem : ModItem, IAbilityAccessoryItem {
		public const int Width = 22;
		public const int Height = 18;



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

		public int? GetAddedAccessorySlots( Player player ) {
			return null;
		}

		////////////////

		public bool EnablesArmorItem( Player player, int slot, Item item ) {
			if( !LockedAbilitiesConfig.Instance.Get<bool>( nameof(LockedAbilitiesConfig.BootLacesEnabled) ) ) {
				return false;
			}

			if( item.shoeSlot != -1 && item.accessory && !item.vanity ) {
				if( item.handOnSlot == -1 && item.handOffSlot == -1 && item.waistSlot == -1 ) {
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

		public float WorldGenChestWeight() {
			var config = LockedAbilitiesConfig.Instance;
			return config.Get<float>( nameof(LockedAbilitiesConfig.WorldGenChestImplantBootLacesChance) );
		}
	}
}
