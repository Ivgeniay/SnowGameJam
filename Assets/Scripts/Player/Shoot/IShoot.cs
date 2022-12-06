using Assets.Scripts.Player.Shoot.DTO;

namespace Assets.Scripts.Player.Shoot
{
    public interface IShoot
    {
        void GetAttack(AttackDTO attackDTO);
        void GetAim(AimDTO aimDTO);
    }
}
