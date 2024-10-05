namespace clash.gameplay
{
    public abstract class ClashBaseSystem
    {
        public abstract void Dispose();
    }
    
    
    public interface IStartSystem
    {
        void OnStart();
    }

    public interface IUpdateSystem
    {
        void OnUpdate(float deltaTime);
    }

    public interface ITickSystem
    {
        void OnTick(int frameIndex);
    }
}