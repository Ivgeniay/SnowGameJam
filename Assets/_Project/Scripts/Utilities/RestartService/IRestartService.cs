namespace Assets._Project.Scripts.Utilities.RestartService
{
    internal interface IRestartService
    {
        public void Register(IRestartable IRestartable);
        public void Unregister(IRestartable IRestartable);
    }
}
