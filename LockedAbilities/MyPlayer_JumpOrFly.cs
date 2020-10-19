using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Players;


namespace LockedAbilities {
	partial class LockedAbilitiesPlayer : ModPlayer {
		private void UpdateVerticalMovement( bool isOnGround ) {
			//Main.NewText( "jump:" + this.player.jump + ", jumpAgainCloud:" + this.player.jumpAgainCloud+", canCloud? "+this.player.doubleJumpCloud
			//	+ ", rocket: " + this.player.rocketBoots + ", rocket time:" + this.player.rocketTime + " < " + this.player.rocketTimeMax );
			//Main.NewText( this.player.wings+" time"+this.player.wingTime+"<"+this.player.wingTimeMax);
			this.UpdateDoubleJumps( isOnGround );
			this.UpdateRocketBoots( isOnGround );
			this.UpdateWings( isOnGround );
		}


		////////////////

		private void UpdateDoubleJumps( bool isOnGround ) {
			if( !LockedAbilitiesConfig.Instance.Get<bool>( nameof(LockedAbilitiesConfig.DoubleJumpsRequireGels) ) ) {
				return;
			}

			if( !isOnGround ) {
				if( this.player.jump == 0 ) {
					if( !this.player.inventory.Any( i => i != null && !i.IsAir && i.type == ItemID.Gel ) ) {
						bool hadDoubleJump = this.player.jumpAgainBlizzard
							|| this.player.jumpAgainCloud
							|| this.player.jumpAgainFart
							|| this.player.jumpAgainSail
							|| this.player.jumpAgainSandstorm;

						if( hadDoubleJump ) {
							Main.NewText( "Need gels to double jump.", Color.Yellow );
						}

						this.player.jumpAgainBlizzard = false;
						this.player.jumpAgainCloud = false;
						this.player.jumpAgainFart = false;
						this.player.jumpAgainSail = false;
						this.player.jumpAgainSandstorm = false;
					}
				}

				if( this.player.doubleJumpBlizzard && !this.player.jumpAgainBlizzard && !this.HasBlizzardJumped ) {
					this.HasBlizzardJumped = true;
					PlayerItemHelpers.RemoveInventoryItemQuantity( this.player, ItemID.Gel, 1 );
				} else
				if( this.player.doubleJumpCloud && !this.player.jumpAgainCloud && !this.HasCloudJumped ) {
					this.HasCloudJumped = true;
					PlayerItemHelpers.RemoveInventoryItemQuantity( this.player, ItemID.Gel, 1 );
				} else
				if( this.player.doubleJumpFart && !this.player.jumpAgainFart && !this.HasFartJumped ) {
					this.HasFartJumped = true;
					PlayerItemHelpers.RemoveInventoryItemQuantity( this.player, ItemID.Gel, 1 );
				} else
				if( this.player.doubleJumpSail && !this.player.jumpAgainSail && !this.HasSailJumped ) {
					this.HasSailJumped = true;
					PlayerItemHelpers.RemoveInventoryItemQuantity( this.player, ItemID.Gel, 1 );
				}
				if( this.player.doubleJumpSandstorm && !this.player.jumpAgainSandstorm && !this.HasSandstormJumped ) {
					this.HasSandstormJumped = true;
					PlayerItemHelpers.RemoveInventoryItemQuantity( this.player, ItemID.Gel, 1 );
				}
			} else {
				this.HasBlizzardJumped = false;
				this.HasCloudJumped = false;
				this.HasFartJumped = false;
				this.HasSailJumped = false;
				this.HasSandstormJumped = false;
			}
		}


		private void UpdateRocketBoots( bool isOnGround ) {
			if( !LockedAbilitiesConfig.Instance.Get<bool>( nameof(LockedAbilitiesConfig.RocketBootsRequireGels) ) ) {
				return;
			}

			if( this.player.rocketBoots == 0 ) {
				return;
			}

			if( !isOnGround ) {
				if( this.player.rocketTime > 1 ) {
					if( !this.player.inventory.Any( i => i != null && !i.IsAir && i.type == ItemID.Gel ) ) {
						this.player.rocketTime = 1;
					} else if( this.player.rocketTime > 0 && this.player.rocketTime < this.player.rocketTimeMax ) {
						if( !this.HasRocketChecked ) {
							this.HasRocketChecked = true;
							PlayerItemHelpers.RemoveInventoryItemQuantity( this.player, ItemID.Gel, 1 );
						}
					}
				}
			} else {
				this.HasRocketChecked = false;
			}
		}


		private void UpdateWings( bool isOnGround ) {
			if( !LockedAbilitiesConfig.Instance.Get<bool>( nameof(LockedAbilitiesConfig.WingsRequirePixieDust) ) ) {
				return;
			}

			if( this.player.wings == 0 ) {
				return;
			}

			if( !isOnGround ) {
				if( this.player.wingTime > 10 ) {
					if( !this.player.inventory.Any( i => i != null && !i.IsAir && i.type == ItemID.PixieDust ) ) {
						this.player.wingTime = 10;
					} else if( this.player.wingTime > 0 && this.player.wingTime < this.player.wingTimeMax ) {
						if( !this.HasWingsChecked ) {
							this.HasWingsChecked = true;
							this.ConsumePixieDust();
						}
					}
				}
			} else {
				this.HasWingsChecked = false;
			}
		}

		private void ConsumePixieDust() {
			PlayerItemHelpers.RemoveInventoryItemQuantity( this.player, ItemID.PixieDust, 1 );

			for( int i=0; i<4; i++ ) {
				int dustIdx = Dust.NewDust(
					Position: this.player.position,
					Height: this.player.width,
					Width: this.player.height,
					Type: 55,
					SpeedX: 0f,
					SpeedY: 0f,
					Alpha: 200,
					newColor: Color.White,
					Scale: 1f
				);
				Main.dust[dustIdx].velocity *= 0.3f;
			}
		}
	}
}
