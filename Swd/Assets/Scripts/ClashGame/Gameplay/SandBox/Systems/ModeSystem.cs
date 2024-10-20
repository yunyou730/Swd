using clash.gameplay.Utilities;

namespace clash.gameplay
{
    public class ModeSystem:ClashBaseSystem,IUpdateSystem,IStartSystem, ITickSystem
    {
        // private UnityEngine.GameObject _tileSelector = null;
        
        private ClashWorld _clashWorld = null;
        private ModeMetaInfo _modeMeta = null;
        private ModeSwitchMetaInfo _modeSwitchMeta = null;


        private int _tileSelectorUUID = 0;
        
        public ModeSystem(ClashBaseWorld world) : base(world)
        {
            _clashWorld = (ClashWorld)world;
            _modeMeta = _clashWorld.GetWorldMeta<ModeMetaInfo>();
            _modeSwitchMeta = _clashWorld.GetWorldMeta<ModeSwitchMetaInfo>();
        }
        
        public void OnStart()
        {
            _tileSelectorUUID = ClashTileEditFunc.CreateTileSelectorEntity(_clashWorld);
        }
        
        public void OnUpdate(float deltaTime)
        {
            
        }
        
        public void OnTick(int frameIndex)
        {
            if (_modeSwitchMeta.HasSwitchMode() && _modeSwitchMeta.GetNextMode() != _modeMeta.CurrentMode)
            {
                if (_modeMeta.CurrentMode != null)
                {
                    OnExitMode((EClashGameMode)_modeMeta.CurrentMode);
                }

                _modeMeta.CurrentMode = _modeSwitchMeta.GetNextMode();
                _modeSwitchMeta.ClearNextMode();
                
                if (_modeMeta.CurrentMode != null)
                {
                    OnEnterMode((EClashGameMode)_modeMeta.CurrentMode);
                }
            }
        }
        
        public override void Dispose()
        {
            _tileSelectorUUID = 0;
        }


        private void OnEnterMode(EClashGameMode mode)
        {
            switch (mode)
            {
                case EClashGameMode.MapEdit:
                    OnEnterEditMode();
                    break;
            }
        }

        private void OnExitMode(EClashGameMode mode)
        {
            switch (mode)
            {
                case EClashGameMode.MapEdit:
                    OnExitEditMode();
                    break;
            }
        }
        
        private void OnEnterEditMode()
        {
            var tileSelectorEntity = _world.GetEntity(_tileSelectorUUID);
            var gfxComp = tileSelectorEntity.GetComponent<GfxComponent>();
            gfxComp.SetVisible(true);
        }

        private void OnExitEditMode()
        {
            var tileSelectorEntity = _world.GetEntity(_tileSelectorUUID);
            var gfxComp = tileSelectorEntity.GetComponent<GfxComponent>();
            gfxComp.SetVisible(false);           
        }
    }
}