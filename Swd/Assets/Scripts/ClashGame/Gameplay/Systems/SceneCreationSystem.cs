namespace clash.gameplay
{
    public class SceneCreationSystem : ClashBaseSystem, IStartSystem
    {
        public SceneCreationSystem(ClashBaseWorld world) : base(world)
        {
            
        }

        public void OnStart()
        {
            GenerateDebugGrid();
            GenerateSceneTerrain();
            GenerateSceneDecoration();
        }

        public override void Dispose()
        {
                
        }
        
        private void GenerateDebugGrid()
        {
            
        }

        private void GenerateSceneTerrain()
        {
            
        }
        
        private void GenerateSceneDecoration()
        {
            
        }
    }
}