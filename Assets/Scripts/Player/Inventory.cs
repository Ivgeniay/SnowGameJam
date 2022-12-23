using Assets.Scripts.Player.Weapon;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.Serialization;
using System;
using Sirenix.OdinInspector;
using Random = System.Random;
using Assets.Scripts.Player.Weapon.Interfaces;

[Serializable]
public class Inventory : SerializedMonoBehaviour
{
    public event Action AmmoIsOver;
    public event Action AmmoReplenished;

    [SerializeField] private Transform gunPlaceTransform;
    [SerializeField] private Snowball snowball;
    [SerializeField] private SnowCannon snowCannon;
    [OdinSerialize] private Dictionary<IWeapon_, int> Weapons;

    private void Awake() {
        Weapons = new Dictionary<IWeapon_, int>();
        Weapons.Add(snowCannon, 9999);
    }

    public void AddAmmo(IWeapon_ weapon, int amount) {
        if (Weapons.TryGetValue(weapon, out int ammo)) Weapons[weapon] = ammo + amount;
        else Weapons[weapon] = amount;
        AmmoReplenished?.Invoke();
    }
    public void decrimentAmmo(IWeapon_ weapon) {
        Weapons[weapon] = Weapons[weapon] - 1;
        ChekIsEmptyAmmo();
    }
    public bool isAmmoEmpty(IWeapon_ weapon) {
        if (weapon is null) throw new NullReferenceException();
        return Weapons[weapon] <= 0;
    }

    public IWeapon_ GetNextWeapon(IWeapon_ currentWeapon)
    {
        List<IWeapon_> weapons = new List<IWeapon_>();

        foreach(KeyValuePair<IWeapon_, int> weapon in Weapons)
            weapons.Add(weapon.Key);

        var count = weapons.FindIndex(x => x == currentWeapon);
        if (count >= weapons.Count() - 1) {
            return weapons[0];
        }
        else {
            return weapons[count + 1];
        } 
    }

    public IWeapon_ GetPreviousWeapon(IWeapon_ currentWeapon)
    {
        List<IWeapon_> weapons = new List<IWeapon_>();

        foreach (KeyValuePair<IWeapon_, int> weapon in Weapons)
            weapons.Add(weapon.Key);

        var count = weapons.FindIndex(x => x == currentWeapon);
        if (count == 0) return weapons[weapons.Count()-1];
        else return weapons[count - 1];
    }

    public IWeapon_ GetWeapon(WeaponVariety weapon) {
        return GetWeapon(snowCannon);

        //switch (weapon)
        //{
        //    case WeaponVariety.snowball:
        //        return GetWeapon(snowball);
        //    case WeaponVariety.snowCannon:
        //        return GetWeapon(snowCannon);
        //    default:
        //        return GetRandomWeapon();
        //}
    }
    public IWeapon_ GetWeapon(IWeapon_ weapon)
    {
        if (Weapons.TryGetValue(weapon, out int value))
        {
            if (value > 0) return weapon;
        }
        return GetRandomWeapon();
    }

    public IWeapon_ GetRandomWeapon() {
        var getter = Weapons.Where(x => x.Value > 0).FirstOrDefault();
        Debug.Log(getter.Key);
        return getter.Key;
    }

    private void ChekIsEmptyAmmo() {
        int ammo = 0;
        foreach(KeyValuePair<IWeapon_, int> weapon in Weapons) ammo += weapon.Value;
        if (ammo <= 0) AmmoIsOver?.Invoke();
    }
}
