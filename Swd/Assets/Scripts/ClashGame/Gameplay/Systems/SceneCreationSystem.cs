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
            var gameStartMeta = world.GetWorldComponent<GameStartMeta>();
            var clashConfigMeta = world.GetWorldComponent<ClashConfigMeta>();
            var material = world.ResManager.GetAsset<UnityEngine.Material>("Assets/Resources_moved/clashgame/scenes/grid_debugger/GridDebugger Variant.mat");
            
            _debugGrid = new DebugGrid(world,world.RootGameObject);
            _debugGrid.BuildMesh(material,gameStartMeta,clashConfigMeta);
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