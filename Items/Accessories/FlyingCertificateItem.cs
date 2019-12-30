using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace LockedAbilities.Items.Accessories {
	public class FlyingCertificateItem : ModItem, IAbilityAccessoryItem {
		public const int Width = 22;
		public const int Height = 18;



		////////////////

		public override void SetStaticDefaults() {
			var mymod = (LockedAbilitiesMod)this.mod;
			mymod.AddAbility( this );

			this.DisplayName.SetDefault( "Flying Certificate" );
			this.Tooltip.SetDefault( "Allows equipping flight accessories" );
		}

		public override void SetDefaults() {
			this.item.width = FlyingCertificateItem.Width;
			this.item.height = FlyingCertificateItem.Height;
			this.item.maxStack = 1;
			this.item.value = Item.buyPrice( 0, 50, 0, 0 );
			this.item.rare = 5;
			this.item.accessory = true;
		}


		////////////////

		public int? GetAddedAccessorySlots( Player player ) {
			return null;
		}

		////////////////

		public bool IsArmorItemEnabled( Player player, int slot, Item item ) {
			if( !LockedAbilitiesConfig.Instance.FlyingCertificateEnabled ) {
				return false;
			}

			if( item.accessory && !item.vanity ) {
				if( item.wingSlot != -1 ) {
					return true;
				}
				switch( item.type ) {
				case ItemID.RocketBoots:
				case ItemID.SpectreBoots:
				case ItemID.LightningBoots:
				case ItemID.FrostsparkBoots:
					return true;
				}
			}
			return false;
		}

		public bool IsMiscItemEnabled( Player player, int slot, Item item ) {
			return false;
		}

		public bool IsEquipItemEnabled( Player player, Item item ) {
			return false;
		}

		////////////////

		public float WorldGenChestWeight( Chest chest ) {
			return LockedAbilitiesConfig.Instance.WorldGenChestImplantFlyingCertificateChance;
		}
	}
}
