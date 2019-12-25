using System;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.DotNET.Extensions;


namespace LockedAbilities.Items.Accessories {
	public class ArticulationRiggingItem : ModItem, IAbilityAccessoryItem {
		public const int Width = 28;
		public const int Height = 28;



		////////////////

		public override void SetStaticDefaults() {
			var mymod = (LockedAbilitiesMod)this.mod;
			mymod.AddAbility( this );

			this.DisplayName.SetDefault( "Articulation Rigging" );
			this.Tooltip.SetDefault( "Enables equipping anything" );
		}

		public override void SetDefaults() {
			this.item.width = ArticulationRiggingItem.Width;
			this.item.height = ArticulationRiggingItem.Height;
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

		public bool IsArmorItemAnAbility( Player player, int slot, Item item ) {
			foreach( (Type abilityType, IAbilityAccessoryItem abilityItemSingleton) in LockedAbilitiesMod.Instance.AbilityItemSingletons ) {
				if( abilityType == this.GetType() ) { continue; }

				if( abilityItemSingleton.IsArmorItemAnAbility(player, slot, item) ) {
					return true;
				}
			}

			return false;
		}

		public bool IsMiscItemAnAbility( Player player, int slot, Item item ) {
			foreach( (Type abilityType, IAbilityAccessoryItem abilityItemSingleton) in LockedAbilitiesMod.Instance.AbilityItemSingletons ) {
				if( abilityType == this.GetType() ) { continue; }

				if( abilityItemSingleton.IsMiscItemAnAbility( player, slot, item ) ) {
					return true;
				}
			}

			return false;
		}

		public bool IsEquipItemAnAbility( Player player, Item item ) {
			foreach( (Type abilityType, IAbilityAccessoryItem abilityItemSingleton) in LockedAbilitiesMod.Instance.AbilityItemSingletons ) {
				if( abilityType == this.GetType() ) { continue; }

				if( abilityItemSingleton.IsEquipItemAnAbility( player, item ) ) {
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
