using System;
using Terraria;
using Terraria.ModLoader;


namespace LockedAbilities.Items.Accessories {
	public class MountReinItem : ModItem, IAbilityAccessoryItem {
		public static int Width = 22;
		public static int Height = 18;



		////////////////

		public override void SetStaticDefaults() {
			var mymod = (LockedAbilitiesMod)this.mod;
			mymod.AddAbility( this );

			this.DisplayName.SetDefault( "Saddle Harness" );
			this.Tooltip.SetDefault( "Allows mounts" );
		}

		public override void SetDefaults() {
			this.item.width = MountReinItem.Width;
			this.item.height = MountReinItem.Height;
			this.item.maxStack = 1;
			this.item.value = Item.buyPrice( 0, 50, 0, 0 );
			this.item.rare = 6;
			this.item.accessory = true;
		}


		////////////////

		public int? GetAddedAccessorySlots( Player player ) {
			return null;
		}

		////////////////

		public bool IsArmorItemAnAbility( Player player, int slot, Item item ) {
			return false;
		}

		public bool IsMiscItemAnAbility( Player player, int slot, Item item ) {
			if( !LockedAbilitiesConfig.Instance.MountReinEnabled ) {
				return false;
			}

			if( item.mountType >= 0 ) {
				return true;
			}
			return false;
		}

		public bool IsEquipItemAnAbility( Player player, Item item ) {
			return false;
		}

		////////////////

		public float WorldGenChestWeight( Chest chest ) {
			return LockedAbilitiesConfig.Instance.WorldGenChestImplantMountReinChance;
		}
	}
}
