using clash.gameplay.GameObject;

namespace clash.gameplay
{
    public class SceneCreationSystem : ClashBaseSystem, IStartSystem
    {
        
        
        public SceneCreationSystem(ClashBaseWorld world) : base(world)
        {
            
        }

        public void OnStart()
        {
            GenerateSceneTerrain();
            GenerateSceneDecoration();
        }


        private void GenerateSceneTerrain()
        {
            
        }
        
        private void GenerateSceneDecoration()
        {
            
        }
        
        public override void Dispose()
        {
            
        }
    }
}