namespace clash.gameplay
{
    public abstract class ClashBaseSystem
    {
        protected ClashBaseWorld _world;
        public ClashBaseSystem(ClashBaseWorld world)
        {
            _world = world;
        }
        
        protected T GetWorld<T>() where T : ClashBaseWorld
        {
            return (T)_world;
        }

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