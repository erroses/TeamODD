namespace GameJam.Project
{
    public interface IGameSystem
    {
        public TimeoutCounter TimeoutCounter { get; }

        public void Initialize();

        public void Dispose();
    }
}
