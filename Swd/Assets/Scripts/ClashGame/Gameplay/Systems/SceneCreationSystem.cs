using clash.gameplay.GameObject;

namespace clash.gameplay
{
    public class SceneCreationSystem : ClashBaseSystem, IStartSystem
    {
        private DebugGrid _debugGrid = null;
        
        public SceneCreationSystem(ClashBaseWorld world) : base(world)
        {
            
        }

        public void OnStart()
        {
            GenerateDebugGrid();
            GenerateSceneTerrain();
            GenerateSceneDecoration();
        }

        private void GenerateDebugGrid()
        {
            ClashWorld world = GetWorld<ClashWorld>();
            
            _debugGrid = new DebugGrid(world.RootGameObject);

            
            var gameStart = world.GetWorldComponent<GameStartWorldComponent>();
            var material = world.ResManager.GetAsset<UnityEngine.Material>("Assets/Resources_moved/clashgame/scenes/grid_debugger/GridDebugger Variant.mat");
            _debugGrid.BuildMesh(material,gameStart);
        }

        private void GenerateSceneTerrain()
        {
            
        }
        
        private void GenerateSceneDecoration()
        {
            
        }
        
        public override void Dispose()
        {
            if (_debugGrid != null)
            {
                _debugGrid.Dispose();
                _debugGrid = null;
            }
        }
    }
}