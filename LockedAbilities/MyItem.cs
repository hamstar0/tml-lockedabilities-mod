﻿using HamstarHelpers.Helpers.Items.Attributes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace LockedAbilities {
	class LockedAbilitiesItem : GlobalItem {
		public override void ModifyTooltips( Item item, List<TooltipLine> tooltips ) {
			var config = LockedAbilitiesConfig.Instance;
			TooltipLine tip;

			if( item.wingSlot != -1 ) {
				if( config.Get<bool>( nameof(LockedAbilitiesConfig.WingsRequirePixieDust) ) ) {
					tip = new TooltipLine( this.mod, "LockedAbiltiesWingFuel", "Wings require pixie dust to use." );
					tip.overrideColor = Color.Yellow;
					tooltips.Add( tip );
				}
			} else {
				switch( item.type ) {
				case ItemID.RocketBoots:
				case ItemID.SpectreBoots:
				case ItemID.LightningBoots:
				case ItemID.FrostsparkBoots:
					if( config.Get<bool>( nameof(LockedAbilitiesConfig.RocketBootsRequireGels) ) ) {
						tip = new TooltipLine( this.mod, "LockedAbiltiesRocketFuel", "Rocket boots require gels to use." );
						tip.overrideColor = Color.Yellow;
						tooltips.Add( tip );
					}
					break;
				case ItemID.CloudinaBottle:
				case ItemID.CloudinaBalloon:
				case ItemID.BlizzardinaBottle:
				case ItemID.BlizzardinaBalloon:
				case ItemID.SandstorminaBottle:
				case ItemID.SandstorminaBalloon:
				case ItemID.FartInABalloon:
				case ItemID.FartinaJar:
				case ItemID.BalloonHorseshoeFart:
				case ItemID.TsunamiInABottle:
					int chainAmt = config.Get<int>( nameof(LockedAbilitiesConfig.GrappleRequiresChainAmount) );

					if( chainAmt > 0 && ItemAttributeHelpers.IsGrapple( item ) ) {
						tip = new TooltipLine( this.mod, "LockedAbilitiesGrappleChainAmmo",
							"Consumes "+chainAmt+" chain(s) per use" );
						ItemInformationAttributeHelpers.ApplyTooltipAt( tooltips, tip );
					}
					if( config.Get<bool>( nameof(LockedAbilitiesConfig.DoubleJumpsRequireGels) ) ) {
						tip = new TooltipLine( this.mod, "LockedAbiltiesJumpFuel",
							"Double jump items require gels to use." );
						tip.overrideColor = Color.Yellow;
						tooltips.Add( tip );
					}
					break;
				}
			}
		}
	}
}