using System;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.DotNET.Extensions;


namespace LockedAbilities.Items.Accessories {
	public class UtilitarianBeltItem : ModItem, IAbilityAccessoryItem {
		public const int Width = 24;
		public const int Height = 14;



		////////////////

		public override void SetStaticDefaults() {
			var mymod = (LockedAbilitiesMod)this.mod;
			mymod.AddAbility( this );

			this.DisplayName.SetDefault( "Utilitarian Belt" );
			this.Tooltip.SetDefault( "Enables equipping anything" );
		}

		public override void SetDefaults() {
			this.item.width = UtilitarianBeltItem.Width;
			this.item.height = UtilitarianBeltItem.Height;
			this.item.maxStack = 1;
			this.item.value = Item.buyPrice( 0, 75, 0, 0 );
			this.item.rare = 6;
			this.item.accessory = true;
		}


		////////////////

		public int? GetAddedAccessorySlots( Player player ) {
			return null;
		}

		////////////////

		public bool IsArmorItemEnabled( Player player, int slot, Item item ) {
			foreach( (Type abilityType, IAbilityAccessoryItem abilityItemSingleton) in LockedAbilitiesMod.Instance.AbilityItemSingletons ) {
				if( abilityType == this.GetType() ) { continue; }

				if( abilityItemSingleton.IsArmorItemEnabled(player, slot, item) ) {
					return true;
				}
			}

			return false;
		}

		public bool IsMiscItemEnabled( Player player, int slot, Item item ) {
			foreach( (Type abilityType, IAbilityAccessoryItem abilityItemSingleton) in LockedAbilitiesMod.Instance.AbilityItemSingletons ) {
				if( abilityType == this.GetType() ) { continue; }

				if( abilityItemSingleton.IsMiscItemEnabled( player, slot, item ) ) {
					return true;
				}
			}

			return false;
		}

		public bool IsEquipItemEnabled( Player player, Item item ) {
			foreach( (Type abilityType, IAbilityAccessoryItem abilityItemSingleton) in LockedAbilitiesMod.Instance.AbilityItemSingletons ) {
				if( abilityType == this.GetType() ) { continue; }

				if( abilityItemSingleton.IsEquipItemEnabled( player, item ) ) {
					return true;
				}
			}

			return false;
		}

		////////////////

		public float WorldGenChestWeight( Chest chest ) {
			return 0f;
		}
	}
}
