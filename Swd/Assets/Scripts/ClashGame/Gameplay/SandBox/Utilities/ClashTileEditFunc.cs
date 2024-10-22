namespace clash.gameplay.Utilities
{
    public class ClashTileEditFunc
    {
        // public static int CreateTileSelectorEntity(ClashWorld world)
        // {
        //     ClashBaseEntity entity = world.CreateEntity();
        //     
        //     // tile selector component
        //     entity.AttachComponent<TileSelectorComponent>();
        //     
        //     // gfx component
        //     var gfxComp = entity.AttachComponent<GfxComponent>();
        //     var tileSelectorPrefab = world.ResManager.GetAsset<UnityEngine.GameObject>("Assets/Resources_moved/clashgame/materials/tile_selector/TileSelector.prefab");
        //     gfxComp.GO = UnityEngine.GameObject.Instantiate(tileSelectorPrefab);
        //
        //     return entity.UUID;
        // }

        public static void ChangeTileTerrainType(ClashWorld world,int tileX,int tileY,ETileTerrainType tileTerrainType)
        {
            var gameStartMeta = world.GetWorldMeta<GameStartMeta>();
            if (tileX >= 0 && tileX < gameStartMeta.GridWidth && tileY >= 0 && tileY < gameStartMeta.GridHeight)
            {
                var tilemapMeta = world.GetWorldMeta<TileMapMeta>();
                tilemapMeta.SetTileTerrain(tileX, tileY, tileTerrainType);    
            }
        }
    }
}