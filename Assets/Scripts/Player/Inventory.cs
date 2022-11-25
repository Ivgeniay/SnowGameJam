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
    public event Action AmmoIsOver;
    public event Action AmmoReplenished;

    [SerializeField] private Snowball snowball;
    [OdinSerialize] private Dictionary<IWeapon, int> Weapons;

    public void AddAmmo(IWeapon weapon, int amount) {
        if (Weapons.TryGetValue(weapon, out int ammo)) Weapons[weapon] = ammo + amount;
        else Weapons[weapon] = amount;
        AmmoReplenished?.Invoke();
    }
    public bool isAmmoEmpty(IWeapon weapon) => Weapons[weapon] <= 0;
    public void decrimentAmmo(IWeapon weapon) {
        Weapons[weapon] = Weapons[weapon] - 1;
        isEmptyAmmo();
    }

    public IWeapon GetWeapon(IWeapon weapon) {
        if (Weapons.TryGetValue(weapon, out int value)) {
            if (value > 0) return weapon;
        } 
        return GetRandomWeapon();
    }
    public IWeapon GetWeapon(WeaponVariety weapon) {
        switch (weapon) {
            case WeaponVariety.snowball: 
                return GetWeapon(snowball);
            default: 
                return GetRandomWeapon();
        }
    }

    public IWeapon GetRandomWeapon() {
        var getter = Weapons.Where(x => x.Value > 0).FirstOrDefault();
        return getter.Key;
    }

    private void isEmptyAmmo() {
        int ammo = 0;
        foreach(KeyValuePair<IWeapon, int> weapon in Weapons) ammo += weapon.Value;
        if (ammo <= 0) AmmoIsOver?.Invoke();
    }
}
