using Assets.Scripts.Player.Weapon.DTO;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player.Weapon
{
    public interface INonPhysicWeapon
    {
        public IEnumerator SetNonPhyMove(NonPhysicParameters nonPhysicParameters);
        public Vector3[] ItineraryPoints { get; set; }
    }
}
