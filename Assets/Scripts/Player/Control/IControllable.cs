namespace Assets.Scripts.Player.Control
{
    public interface IControllable
    {
        public void MoveUpdate();
        public PlayerControlContext GetContext();
    }
}
