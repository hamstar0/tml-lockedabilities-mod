using System;
using Terraria;


namespace LockedAbilities {
	public interface IAbilityAccessoryItem {
		int? GetMaxAccessorySlot( Player player );
		bool IsArmorItemAnAbility( Player player, int slot, Item item );
		bool IsMiscItemAnAbility( Player player, Item item );
		bool IsEquipItemAnAbility( Player player, Item item );
	}
}
