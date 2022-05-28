using System;
using System.Collections.Generic;
using Terraria;


namespace LockedAbilities {
	public class LockedAbilitiesAPI {
		public static int GetAllowedAccessorySlots( Player player ) {
			var myplayer = player.GetModPlayer<LockedAbilitiesPlayer>();

			return myplayer.InternalAllowedAccessorySlots;
		}

		public static void SetAllowedAccessorySlots( Player player, int slots ) {
			var myplayer = player.GetModPlayer<LockedAbilitiesPlayer>();

			myplayer.SetAllowedAccessorySlots( slots );
		}
	}
}
