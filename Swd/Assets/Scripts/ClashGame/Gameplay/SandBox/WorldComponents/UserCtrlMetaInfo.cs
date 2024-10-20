namespace clash.gameplay
{
    public class UserCtrlMetaInfo : ClashBaseMetaInfo
    {
        public bool SelectTileDirtyFlag { private set; get; }
        public int SelectTileX { private set; get ; }
        public int SelectTileY { private set; get; }


        public UserCtrlMetaInfo()
        {
            SelectTileDirtyFlag = false;
        }

        public void SetSelectTile(int tileX,int tileY)
        {
            SelectTileDirtyFlag = true;
            SelectTileX = tileX;
            SelectTileY = tileY;
        }

        public void ResetSelectTileDirtyFlag()
        {
            SelectTileDirtyFlag = false;
        }
    }
}