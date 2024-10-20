using clash.gameplay;
using clash.gameplay.Utilities;
using UnityEngine;
using IngameDebugConsole;

namespace clash.debug
{
    public class ClashConsole
    {
        [ConsoleMethod( "ClashGameTest", "ClashGameTest" )]
        public static void CreateCubeAt( Vector3 position )
        {
            Debug.Log("test1");
            GameObject.CreatePrimitive( PrimitiveType.Cube ).transform.position = position;
        }

        [ConsoleMethod("CreateUnitAt","Create Unit at Tile")]
        public static void CreateUnitAt(string unitTag,int tileX,int tileY)
        {
            GameObject go = GameObject.Find("ClashGame");
            if (go != null)
            {
                var clashGame = go.GetComponent<ClashGame>();
                var clashWorld = clashGame.GP.World;
                var cmdMeta = clashWorld.GetWorldMeta<CmdMeta>();
                cmdMeta.AddCmdCreateUnitAtTile(tileX,tileY,unitTag);
            }
        }

        [ConsoleMethod("ShowMenu","ShowMenu")]
        public static void ShowMenu(EMenuType menuType)
        {
            Debug.Log("ShowMenu");
            GameObject go = GameObject.Find("ClashGame");
            if (go != null)
            {
                var clashGame = go.GetComponent<ClashGame>();
                clashGame.MenuManager.ShowMenu(menuType);
            }
        }
        
        [ConsoleMethod("CloseMenu","CloseMenu")]
        public static void CloseMenu(EMenuType menuType)
        {
            Debug.Log("CloseMenu");
            GameObject go = GameObject.Find("ClashGame");
            if (go != null)
            {
                var clashGame = go.GetComponent<ClashGame>();
                clashGame.MenuManager.CloseMenu(menuType);
            }            
        }
    }
}