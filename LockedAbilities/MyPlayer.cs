using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Players;
using HamstarHelpers.Services.Timers;
using LockedAbilities.Protocols;


namespace LockedAbilities {
	partial class LockedAbilitiesPlayer : ModPlayer {
		private bool HasBlizzardJumped = false;
		private bool HasCloudJumped = false;
		private bool HasFartJumped = false;
		private bool HasSailJumped = false;
		private bool HasSandstormJumped = false;
		private bool HasRocketChecked = false;
		private bool HasWingsChecked = false;


		////////////////

		public int InternalAllowedAccessorySlots { get; private set; } = 1;
		public int TotalAllowedAccessorySlots { get; private set; } = 1;

		////

		public override bool CloneNewInstances => false;



		////////////////

		public override void Initialize() {
			var config = LockedAbilitiesConfig.Instance;
			this.InternalAllowedAccessorySlots = config.Get<int>( nameof(LockedAbilitiesConfig.InitialAccessorySlots) );
			this.TotalAllowedAccessorySlots = this.InternalAllowedAccessorySlots;
		}

		public override void Load( TagCompound tag ) {
			var config = LockedAbilitiesConfig.Instance;
			this.InternalAllowedAccessorySlots = config.Get<int>( nameof(LockedAbilitiesConfig.InitialAccessorySlots) );
			this.TotalAllowedAccessorySlots = this.InternalAllowedAccessorySlots;

			if( tag.ContainsKey("highest_acc_slots") ) {
				this.InternalAllowedAccessorySlots = tag.GetInt( "highest_acc_slots" );
				this.TotalAllowedAccessorySlots = this.InternalAllowedAccessorySlots;
			}
		}

		public override TagCompound Save() {
			return new TagCompound {
				{ "highest_acc_slots", this.InternalAllowedAccessorySlots }
			};
		}

		////////////////

		public override void SyncPlayer( int toWho, int fromWho, bool newPlayer ) {
			if( Main.netMode == 1 ) {
				if( toWho == -1 && fromWho == -1 && newPlayer ) {
					//this.OnCurrentClientConnect();
					PlayerDarkHeartsProtocol.Sync( Main.myPlayer, this.InternalAllowedAccessorySlots );
				}
			}
			if( Main.netMode == 2 ) {
				if( toWho == -1 && fromWho == this.player.whoAmI ) {
					//this.OnServerConnect( Main.player[fromWho] );
				}
			}
		}


		////////////////

		public override void PreUpdate() {
			int firstAccSlot = PlayerItemHelpers.VanillaAccessorySlotFirst;
			int maxAccSlot = PlayerItemHelpers.GetCurrentVanillaMaxAccessories( this.player ) + firstAccSlot;
			ISet<Type> equippedAbilityItemTypes = new HashSet<Type>();

			this.TestMountState();

			// Find equipped ability items
			for( int i = firstAccSlot; i < maxAccSlot; i++ ) {
				Item item = this.player.armor[i];
				if( item == null || item.IsAir || item.modItem == null || !(item.modItem is IAbilityAccessoryItem) ) {
					continue;
				}

				this.TestMaxAllowedAccessorySlots( item );

				equippedAbilityItemTypes.Add( item.modItem.GetType() );
			}

			this.TestArmorSlots( equippedAbilityItemTypes );
			this.TestMiscSlots( equippedAbilityItemTypes );

			if( !this.player.dead ) {
				this.UpdateVerticalMovement( this.player.velocity.Y == 0 && !this.player.HasBuff(BuffID.Webbed) );
			}
		}


		////////////////


		public override bool PreItemCheck() {
			if( !Main.mouseLeft && !Main.mouseRight ) {
				return base.PreItemCheck();
			}

			string timerName = "LockedAbilitiesEquipCheck_" + this.player.whoAmI;
			if( Timers.GetTimerTickDuration( timerName ) > 0 ) {
				Timers.SetTimer( timerName, 2, false, () => false );
				return false;
			}

			int firstAccSlot = PlayerItemHelpers.VanillaAccessorySlotFirst;
			int maxAccSlot = PlayerItemHelpers.GetCurrentVanillaMaxAccessories( this.player ) + firstAccSlot;
			ISet<Type> equippedAbilityItemTypes = new HashSet<Type>();

			// Find equipped ability items
			for( int i = firstAccSlot; i < maxAccSlot; i++ ) {
				Item item = this.player.armor[i];
				if( item == null || item.IsAir || item.modItem == null || !( item.modItem is IAbilityAccessoryItem ) ) {
					continue;
				}

				equippedAbilityItemTypes.Add( item.modItem.GetType() );
			}

			if( !this.TestEquipItem( equippedAbilityItemTypes, this.player.HeldItem ) ) {
				Timers.SetTimer( timerName, 2, false, () => false );
				return false;
			}

			return base.PreItemCheck();
		}


		/*public override bool ConsumeAmmo( Item weapon, Item ammo ) {
			return this.CanUse( weapon ) && this.CanUse( ammo );
		}

		public override bool Shoot( Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack ) {
			return this.CanUse( item );
		}

		////

		private bool CanUse( Item item ) {
			int firstAccSlot = PlayerItemHelpers.VanillaAccessorySlotFirst;
			int maxAccSlot = PlayerItemHelpers.GetCurrentVanillaMaxAccessories( this.player ) + firstAccSlot;
			ISet<Type> equippedAbilityItemTypes = new HashSet<Type>();

			// Find equipped ability items
			for( int i = firstAccSlot; i < maxAccSlot; i++ ) {
				Item abilityItem = this.player.armor[i];
				if( abilityItem == null || abilityItem.IsAir ) {
					continue;
				}
				if( abilityItem.modItem == null || !(abilityItem.modItem is IAbilityAccessoryItem) ) {
					continue;
				}

				equippedAbilityItemTypes.Add( abilityItem.modItem.GetType() );
			}

			return this.TestEquipItem( equippedAbilityItemTypes, item );
		}*/
	}
}
