namespace DZM.Core
{
    public interface ISystem
    {
        public void Setup();

        public void Execute();

        public void Cleanup();
    }
}
