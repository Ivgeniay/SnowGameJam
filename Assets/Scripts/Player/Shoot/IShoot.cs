using Assets.Scripts.Player.Weapon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Player.Shoot
{
    public interface IShoot
    {
        void GetAttack(AttackDTO attackDTO);
        void GetAim(AimDTO aimDTO);
    }
}
