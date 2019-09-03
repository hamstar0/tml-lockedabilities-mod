using System;
using Terraria;


namespace LockedAbilities {
	public interface IAbilityAccessoryItem {
		int? GetMaxAccessorySlot( Player player );
		bool IsArmorItemEnabled( Player player, int slot, Item item );
		bool IsArmorItemDisabled( Player player, int slot, Item item );
		bool IsMiscItemEnabled( Player player, Item item );
		bool IsMiscItemDisabled( Player player, Item item );
		bool IsEquipItemEnabled( Player player, Item item );
		bool IsEquipItemDisabled( Player player, Item item );
	}
}
