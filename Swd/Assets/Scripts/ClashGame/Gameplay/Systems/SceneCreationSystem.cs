namespace clash.gameplay
{
    public class SceneCreationSystem : ClashBaseSystem, IStartSystem
    {
        public void OnStart()
        {
            GenerateSceneTerrain();
            GenerateSceneDecoration();
        }

        public override void Dispose()
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