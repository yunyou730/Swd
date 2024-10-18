namespace clash.gameplay
{
    public class ModeSystem:ClashBaseSystem,IUpdateSystem,IStartSystem, ITickSystem
    {
        private UnityEngine.GameObject _tileSelector = null;
        
        private ClashWorld _clashWorld = null;
        private ModeMetaInfo _modeMeta = null;
        private ModeSwitchMetaInfo _modeSwitchMeta = null;
        
        public ModeSystem(ClashBaseWorld world) : base(world)
        {
            _clashWorld = (ClashWorld)world;
            _modeMeta = _clashWorld.GetWorldMeta<ModeMetaInfo>();
            _modeSwitchMeta = _clashWorld.GetWorldMeta<ModeSwitchMetaInfo>();
        }
        
        public void OnStart()
        {
            var tileSelectorPrefab = _clashWorld.ResManager.GetAsset<UnityEngine.GameObject>("Assets/Resources_moved/clashgame/scenes/tile_selector/TileSelector.prefab");
            _tileSelector = UnityEngine.GameObject.Instantiate(tileSelectorPrefab);
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
            UnityEngine.Object.Destroy(_tileSelector);
            _tileSelector = null;
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
            _tileSelector.SetActive(true);
        }

        private void OnExitEditMode()
        {
            _tileSelector.SetActive(false);           
        }
    }
}