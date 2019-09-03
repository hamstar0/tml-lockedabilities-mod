using System;
using Terraria;


namespace LockedAbilities {
	public interface IAbilityAccessoryItem {
		int? GetMaxAccessorySlot( Player player );
		bool TestItemEnabled( Player player, int slot, Item item );
		bool TestItemDisabled( Player player, int slot, Item item );
	}
}
