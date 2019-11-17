using System;
using Terraria;


namespace LockedAbilities {
	public interface IAbilityAccessoryItem {
		float WorldGenChestWeight();
		int? GetMaxArmorSlot( Player player );
		bool IsArmorItemAnAbility( Player player, int slot, Item item );
		bool IsMiscItemAnAbility( Player player, int slot, Item item );
		bool IsEquipItemAnAbility( Player player, Item item );
	}
}
