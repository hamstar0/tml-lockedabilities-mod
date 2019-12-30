using System;
using Terraria;


namespace LockedAbilities {
	public interface IAbilityAccessoryItem {
		float WorldGenChestWeight( Chest chest );
		int? GetAddedAccessorySlots( Player player );
		bool IsArmorItemEnabled( Player player, int slot, Item item );
		bool IsMiscItemEnabled( Player player, int slot, Item item );
		bool IsEquipItemEnabled( Player player, Item item );
	}
}
