namespace Assets.Scripts.Player.Control
{
    public interface IControllable
    {
        public void Move();
        public PlayerControlContext GetContext();
    }
}
