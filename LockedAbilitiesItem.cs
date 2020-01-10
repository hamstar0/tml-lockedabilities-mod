using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace LockedAbilities {
	class LockedAbilitiesItem : GlobalItem {
		public override void ModifyTooltips( Item item, List<TooltipLine> tooltips ) {
			TooltipLine tip;

			if( item.wingSlot != -1 ) {
				if( LockedAbilitiesConfig.Instance.WingsRequirePixieDust ) {
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
					if( LockedAbilitiesConfig.Instance.RocketBootsRequireGels ) {
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
					if( LockedAbilitiesConfig.Instance.DoubleJumpsRequireGels ) {
						tip = new TooltipLine( this.mod, "LockedAbiltiesJumpFuel", "Double jump items require gels to use." );
						tip.overrideColor = Color.Yellow;
						tooltips.Add( tip );
					}
					break;
				}
			}
		}
	}
}
