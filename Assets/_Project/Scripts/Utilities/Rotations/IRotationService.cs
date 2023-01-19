namespace Assets._Project.Scripts.Utilities.Rotations
{
    internal interface IRotationService
    {
        public void Register(IRotation IRotation);
        public void Unregister(IRotation IRotation);
    }
}
