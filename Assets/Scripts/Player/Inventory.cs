using Assets.Scripts.Player.Weapon;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.Serialization;
using System;
using Sirenix.OdinInspector;
using Random = System.Random;
using Sirenix.Utilities;

[Serializable]
public class Inventory : SerializedMonoBehaviour
{
    public event Action AmmoIsOver;
    public event Action AmmoReplenished;

    [SerializeField] private Snowball snowball;
    [OdinSerialize] private Dictionary<IWeapon, int> Weapons;

    private void Start()
    {
        //Weapons.ForEach(x => Debug.Log($"{x.Key} {x.Value}"));
        //Debug.Log(snowball.name);
    }

    public void AddAmmo(IWeapon weapon, int amount) {
        if (Weapons.TryGetValue(weapon, out int ammo)) Weapons[weapon] = ammo + amount;
        else Weapons[weapon] = amount;
        AmmoReplenished?.Invoke();
    }
    public void decrimentAmmo(IWeapon weapon) {
        Weapons[weapon] = Weapons[weapon] - 1;
        ChekIsEmptyAmmo();
    }
    public bool isAmmoEmpty(IWeapon weapon) {
        if (weapon is null) throw new NullReferenceException();
        return Weapons[weapon] <= 0;
    }
    public IWeapon GetWeapon(IWeapon weapon) {
        if (Weapons.TryGetValue(weapon, out int value)) {
            if (value > 0) return weapon;
        } 
        return GetRandomWeapon();
    }

    public IWeapon GetNextWeapon(IWeapon currentWeapon)
    {
        Debug.Log("NextWeapon");
        List<IWeapon> weapons = new List<IWeapon>();

        foreach(KeyValuePair<IWeapon, int> weapon in Weapons)
            weapons.Add(weapon.Key);

        var count = weapons.FindIndex(x => x == currentWeapon);
        if (count >= weapons.Count() - 1) {
            Debug.Log(weapons[0]);
            return weapons[0];
        }
        else {
            Debug.Log(weapons[count + 1]);
            return weapons[count + 1];
        } 
    }

    public IWeapon GetPreviousWeapon(IWeapon currentWeapon)
    {
        List<IWeapon> weapons = new List<IWeapon>();

        foreach (KeyValuePair<IWeapon, int> weapon in Weapons)
            weapons.Add(weapon.Key);

        var count = weapons.FindIndex(x => x == currentWeapon);
        if (count == 0) return weapons[weapons.Count()-1];
        else return weapons[count - 1];
    }

    public IWeapon GetWeapon(WeaponVariety weapon) {
        switch (weapon)
        {
            case WeaponVariety.snowball:
                return GetWeapon(snowball);
            default:
                return GetRandomWeapon();
        }
    }

    public IWeapon GetRandomWeapon() {
        var getter = Weapons.Where(x => x.Value > 0).FirstOrDefault();
        Debug.Log(getter);
        return getter.Key;
    }

    private void ChekIsEmptyAmmo() {
        int ammo = 0;
        foreach(KeyValuePair<IWeapon, int> weapon in Weapons) ammo += weapon.Value;
        if (ammo <= 0) AmmoIsOver?.Invoke();
    }
}
