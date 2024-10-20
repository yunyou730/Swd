namespace clash.gameplay
{
    public class ModeMetaInfo : ClashBaseMetaInfo
    {
        public EClashGameMode? CurrentMode = null;
    }
    
    public class ModeSwitchMetaInfo : ClashBaseMetaInfo
    {
        private EClashGameMode? _nextMode = null;
        
        public void SetNextMode(EClashGameMode nextMode)
        {
            _nextMode = nextMode;
        }

        public EClashGameMode? GetNextMode()
        {
            return _nextMode;
        }

        public void ClearNextMode()
        {
            _nextMode = null;
        }

        public bool HasSwitchMode()
        {
            return _nextMode != null;
        }
    }
}