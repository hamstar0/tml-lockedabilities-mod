using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsGeneral.Libraries.Items.Attributes;


namespace LockedAbilities {
	class LockedAbilitiesItem : GlobalItem {
		public override void ModifyTooltips( Item item, List<TooltipLine> tooltips ) {
			var config = LockedAbilitiesConfig.Instance;
			TooltipLine tip;

			if( item.wingSlot != -1 ) {
				if( config.Get<bool>( nameof(config.WingsRequirePixieDust) ) ) {
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
					if( config.Get<bool>( nameof(config.RocketBootsRequireGels) ) ) {
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
					int chainAmt = config.Get<int>( nameof(config.GrappleRequiresChainAmount) );

					if( chainAmt > 0 && ItemAttributeLibraries.IsGrapple( item ) ) {
						tip = new TooltipLine( this.mod, "LockedAbilitiesGrappleChainAmmo",
							"Consumes "+chainAmt+" chain(s) per use" );
						ItemInformationAttributeLibraries.ApplyTooltipAt( tooltips, tip );
					}
					if( config.Get<bool>( nameof(config.DoubleJumpsRequireGels) ) ) {
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
