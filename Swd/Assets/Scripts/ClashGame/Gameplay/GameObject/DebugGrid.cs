using System;
using clash.gameplay.Utilities;
using Unity.VisualScripting;
using UnityEngine;


namespace clash.gameplay.GameObject
{
    public class DebugGrid : IDisposable
    {
        private UnityEngine.GameObject _parentGameObject = null;
        private UnityEngine.GameObject _go = null;
        private ClashWorld _world = null;

        public DebugGrid(ClashWorld world,UnityEngine.GameObject parentGameObject)
        {
            _world = world;
            _parentGameObject = parentGameObject;
            _go = new UnityEngine.GameObject("[Clash][DebugGrid]");
            _go.transform.SetParent(_parentGameObject.transform);
        }

        public void BuildMesh(UnityEngine.Material material,GameStartMeta gameStartMeta,ClashConfigMeta configMeta)
        {
            // Build Mesh 
            Mesh mesh = BuildGridMesh(material,gameStartMeta,configMeta);
            

            // Mesh
            MeshFilter meshFilter = _go.AddComponent<MeshFilter>();
            meshFilter.mesh = mesh;

            // material
            MeshRenderer meshRenderer = _go.AddComponent<MeshRenderer>();
            //meshRenderer.material = new Material(Shader.Find("Standard"));
            meshRenderer.material = material;
        }


        private Mesh BuildGridMesh(UnityEngine.Material material,GameStartMeta gameStart,ClashConfigMeta clashConfig)
        {
            // _world.GameData.GridWidth;


            // Build Mesh 
            Mesh mesh = new Mesh();

            int width = gameStart.GridWidth;
            int height = gameStart.GridHeight;
            
            Vector3[] vertices = new Vector3[width * height * 4];
            int[] triangles = new int[width * height * 6];
            Vector2[] uvs = new Vector2[width * height * 4];

            
            int vertIndex = 0;
            int faceIndex = 0;
            for (int x = 0;x < width;x++)
            {
                for (int z = 0;z < height;z++)
                {
                    Vector3 center = ClashUtility.GetPositionAtTile(_world,x, z);

                    vertices[vertIndex] = center + new Vector3(-1, 0, -1) * clashConfig.TileSize * 0.5f;
                    vertices[vertIndex + 1] = center + new Vector3(1, 0, -1) * clashConfig.TileSize * 0.5f;
                    vertices[vertIndex + 2] = center + new Vector3(1, 0, 1) * clashConfig.TileSize * 0.5f;
                    vertices[vertIndex + 3] = center + new Vector3(-1, 0, 1) * clashConfig.TileSize * 0.5f;

                    uvs[vertIndex] = new Vector2(0, 0);
                    uvs[vertIndex + 1] = new Vector2(1, 0);
                    uvs[vertIndex + 2] = new Vector2(1, 1);
                    uvs[vertIndex + 3] = new Vector2(0, 1);
                    
                    triangles[faceIndex++] = vertIndex + 0;
                    triangles[faceIndex++] = vertIndex + 2;
                    triangles[faceIndex++] = vertIndex + 1;
                    triangles[faceIndex++] = vertIndex + 0;
                    triangles[faceIndex++] = vertIndex + 3;
                    triangles[faceIndex++] = vertIndex + 2;

                    vertIndex += 4;
                }
            }
            

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uvs;
            mesh.RecalculateNormals();

            return mesh;
        }

        public void Dispose()
        {
            UnityEngine.GameObject.Destroy(_go);
            _go = null;
        }
    }
}