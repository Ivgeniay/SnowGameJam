using Assets.Scripts.Player.Weapon;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Snowball sb;
    [SerializeField] private Snowball tt;
    [SerializeField] private int snowBalls;

    public bool isEmpty() => snowBalls <= 0;
    public void AddSnowballs(int snowball) => this.snowBalls += snowball;
    public void decrimentSnowball() {
        if (isEmpty()) return;
        snowBalls--;
    }

    public bool isAmmoLeft(IWeapon weapon) => snowBalls >= 0;
    public void decrimentAmmo(IWeapon weapon) => decrimentSnowball();

    public IWeapon GetWeapon(WeaponVariety weapon) {
        return sb;
    }
    public IWeapon GetRandomWeapon() {
        return sb;
    }

}
