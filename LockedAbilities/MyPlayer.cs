using System;
using System.Collections.Generic;
using System.Linq;
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

		internal void OnCurrentPlayerEnter() {
			PlayerDarkHeartsProtocol.Broadcast( this.InternalAllowedAccessorySlots );
		}

		public override void clientClone( ModPlayer clientClone ) {
			var myclone = (LockedAbilitiesPlayer)clientClone;

			myclone.InternalAllowedAccessorySlots = this.InternalAllowedAccessorySlots;
		}

		////////////////

		public override void SendClientChanges( ModPlayer clientPlayer ) {
			if( clientPlayer.player.whoAmI != Main.myPlayer ) {
				return;
			}

			var myclone = (LockedAbilitiesPlayer)clientPlayer;

			if( myclone.InternalAllowedAccessorySlots != this.InternalAllowedAccessorySlots ) {
				PlayerDarkHeartsProtocol.Broadcast( this.InternalAllowedAccessorySlots );
			}
		}


		////////////////

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


		////////////////

		public override void PreUpdate() {
			IList<Item> equippedAbilityItemList = this.GetEquippedAbilityItems();
			ISet<Type> equippedAbilityItemTypes = new HashSet<Type>(
				equippedAbilityItemList
					.Where( i => i.modItem != null )
					.Select( i => i.modItem?.GetType() )
			);

			this.TestMountState();

			// Find equipped ability items
			foreach( Item item in equippedAbilityItemList ) {
				this.TestMaxAllowedAccessorySlots( item );
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
			int maxAccSlot = PlayerItemHelpers.GetCurrentVanillaMaxAccessories(this.player) + firstAccSlot;
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


		////////////////

		private IList<Item> GetEquippedAbilityItems() {
			int firstAccSlot = PlayerItemHelpers.VanillaAccessorySlotFirst;
			int maxAccSlot = PlayerItemHelpers.GetCurrentVanillaMaxAccessories(player) + firstAccSlot;
			var items = new List<Item>();

			for( int i = firstAccSlot; i < maxAccSlot; i++ ) {
				Item item = player.armor[i];
				if( item == null || item.IsAir || item.modItem == null || !(item.modItem is IAbilityAccessoryItem) ) {
					continue;
				}

				items.Add( item );
			}

			return items;
		}
	}
}
