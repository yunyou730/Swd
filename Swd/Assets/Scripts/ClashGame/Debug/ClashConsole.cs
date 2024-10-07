using clash.gameplay;
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
                var world = clashGame.World;
                var unitFactoryMeta = world.GetWorldComponent<UnitFactoryMeta>();
                unitFactoryMeta.Datas.Add(new UnitGenerateData(unitTag,tileX,tileY));
            }
        }
    }
}