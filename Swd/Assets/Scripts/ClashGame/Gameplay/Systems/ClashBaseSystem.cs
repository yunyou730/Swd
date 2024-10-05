namespace clash.gameplay
{
    public abstract class ClashBaseSystem
    {
        public abstract void Dispose();
    }

    public interface IUpdateSystem
    {
        void Update(float deltaTime);
    }

    public interface ITickSystem
    {
        void Tick(int frameIndex);
    }

    public interface IStartSystem
    {
        void Start();
    }

}