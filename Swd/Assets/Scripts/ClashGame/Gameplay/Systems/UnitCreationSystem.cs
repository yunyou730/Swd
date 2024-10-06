namespace clash.gameplay
{
    public class UnitCreationSystem:ClashBaseSystem, IStartSystem,ITickSystem
    {
        UnitFactoryWorldComp _unitFactory = null;
        
        public UnitCreationSystem(ClashBaseWorld world) : base(world)
        {
            _unitFactory = world.GetWorldComponent<UnitFactoryWorldComp>();
        }

        public override void Dispose()
        {
            
        }

        public void OnStart()
        {
            
        }

        public void OnTick(int frameIndex)
        {
            if (_unitFactory.Datas.Count == 0)
            {
                return;
            }

            foreach (var item in _unitFactory.Datas)
            {
                CreateUnit(item);
            }
            _unitFactory.Datas.Clear();
        }
        
        private void CreateUnit(UnitGenerateData data)
        {
            // data.TileX;
            // data.TileY;
            // data.UnitTag;
            
            
            
        }
    }
}