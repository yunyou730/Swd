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
    }
}