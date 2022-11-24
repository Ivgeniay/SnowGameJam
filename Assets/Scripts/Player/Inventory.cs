using Assets.Scripts.Player.Weapon;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.Serialization;
using System;
using Sirenix.OdinInspector;
using Random = System.Random;

[Serializable]
public class Inventory : SerializedMonoBehaviour
{
    [OdinSerialize] private Dictionary<IWeapon, int> Weapons;

    public void AddAmmo(IWeapon weapon, int amount) {
        if (Weapons.TryGetValue(weapon, out int ammo)) Weapons[weapon] = ammo + amount;
        else Weapons[weapon] = amount;
    }
    public bool isAmmoEmpty(IWeapon weapon) => Weapons[weapon] <= 0;
    public void decrimentAmmo(IWeapon weapon) {
        Weapons[weapon] = Weapons[weapon] - 1;
    }
    public IWeapon GetWeapon(WeaponVariety weapon) {
        var getter = Weapons.Where(x => x.Value > 0).FirstOrDefault();
        return getter.Key;
    }
}
