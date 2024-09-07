using LitJson;

namespace swd.gameplay
{
    public struct GameplayData
    {
        public string MapTag;
        public int MapWidth;
        public int MapHeight;

        public string MainCharacterTag;
        public int MCSpawnX;
        public int MCSpawnY;

        public double CameraFollowDistance;

        public GameplayData(JsonData jsonData)
        {
            MapTag = jsonData["map_tag"].ToString();
            MapWidth = (int)jsonData["map_width"];
            MapHeight = (int)jsonData["map_height"];

            MainCharacterTag = jsonData["main_character"].ToString();
            MCSpawnX = (int)jsonData["mc_spawn_x"];
            MCSpawnY = (int)jsonData["mc_spawn_y"];
            
            
            CameraFollowDistance = (double)jsonData["camera_distance"];
        }
    }
}