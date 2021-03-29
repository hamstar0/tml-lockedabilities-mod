using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.DotNET.Extensions;


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

		public bool EnablesArmorItem( Player player, int slot, Item item ) {
			if( !LockedAbilitiesConfig.Instance.Get<bool>( nameof(LockedAbilitiesConfig.FlyingCertificateEnabled) ) ) {
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
					foreach( (Type abilityEnablingItemType, IAbilityAccessoryItem abilityEnablingItemSingleton) in LockedAbilitiesMod.Instance.AbilityItemSingletons ) {
						if( abilityEnablingItemType != this.GetType() ) {
							if( abilityEnablingItemSingleton.EnablesArmorItem( player, slot, item ) ) {
								return true;
							}
						}
					}
					return false;
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
			return config.Get<float>( nameof(LockedAbilitiesConfig.WorldGenChestImplantFlyingCertificateChance) );
		}
	}
}
