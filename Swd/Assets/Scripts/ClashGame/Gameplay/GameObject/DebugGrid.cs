using System;
using clash.gameplay.Utilities;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;


namespace clash.gameplay.GameObject
{
    public class DebugGrid : IDisposable
    {
        private UnityEngine.GameObject _parentGameObject = null;
        private UnityEngine.GameObject _go = null;
        private ClashWorld _world = null;
        
        private Texture2D _tileDataTex = null;
        private static readonly int kGridWidth = Shader.PropertyToID("_GridWidth");
        private static readonly int kGridHeight = Shader.PropertyToID("_GridHeight");
        private static readonly int kTileDataTex = Shader.PropertyToID("_TileDataTex");

        public DebugGrid(ClashWorld world,UnityEngine.GameObject parentGameObject)
        {
            _world = world;
            _parentGameObject = parentGameObject;
            _go = new UnityEngine.GameObject("[Clash][DebugGrid]");
            _go.transform.SetParent(_parentGameObject.transform);
        }

        public void BuildMesh(UnityEngine.Material material,GameStartMeta gameStartMeta,ClashConfigMeta configMeta,TileMapMeta tilemapMeta)
        {
            // Build Mesh 
            Mesh mesh = BuildGridMesh(material,gameStartMeta,configMeta);
            
            // Mesh
            MeshFilter meshFilter = _go.AddComponent<MeshFilter>();
            meshFilter.mesh = mesh;
            
            // data texture
            _tileDataTex = GenerateTexture(gameStartMeta.GridWidth,gameStartMeta.GridHeight,tilemapMeta);

            // material
            material.SetFloat(kGridWidth,gameStartMeta.GridWidth);
            material.SetFloat(kGridHeight,gameStartMeta.GridHeight);
            material.SetTexture(kTileDataTex,_tileDataTex);

            MeshRenderer meshRenderer = _go.AddComponent<MeshRenderer>();
            //meshRenderer.material = new Material(Shader.Find("Standard"));
            meshRenderer.material = material;
        }

        public void AttachCollider()
        {
            _go.AddComponent<MeshCollider>();
        }

        private Mesh BuildGridMesh(UnityEngine.Material material,GameStartMeta gameStart,ClashConfigMeta clashConfig)
        {
            // Build Mesh 
            Mesh mesh = new Mesh();

            int width = gameStart.GridWidth;
            int height = gameStart.GridHeight;
            
            Vector3[] vertices = new Vector3[width * height * 4];
            int[] triangles = new int[width * height * 6];
            Vector2[] uvs = new Vector2[width * height * 4];
            Vector2[] tileCoord = new Vector2[width * height * 4];

            
            int vertIndex = 0;
            int faceIndex = 0;
            for (int x = 0;x < width;x++)
            {
                for (int z = 0;z < height;z++)
                {
                    Vector3 center = ClashUtility.GetPositionAtTile(_world,x, z);

                    // semantic position
                    vertices[vertIndex] = center + new Vector3(-1, 0, -1) * clashConfig.TileSize * 0.5f;
                    vertices[vertIndex + 1] = center + new Vector3(1, 0, -1) * clashConfig.TileSize * 0.5f;
                    vertices[vertIndex + 2] = center + new Vector3(1, 0, 1) * clashConfig.TileSize * 0.5f;
                    vertices[vertIndex + 3] = center + new Vector3(-1, 0, 1) * clashConfig.TileSize * 0.5f;

                    // semantic UV
                    uvs[vertIndex] = new Vector2(0, 0);
                    uvs[vertIndex + 1] = new Vector2(1, 0);
                    uvs[vertIndex + 2] = new Vector2(1, 1);
                    uvs[vertIndex + 3] = new Vector2(0, 1);

                    tileCoord[vertIndex] = new Vector2(x,z);
                    tileCoord[vertIndex + 1] = new Vector2(x,z);
                    tileCoord[vertIndex + 2] = new Vector2(x,z);
                    tileCoord[vertIndex + 3] = new Vector2(x,z);
                    
                    // face
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
            mesh.uv2 = tileCoord;
            mesh.RecalculateNormals();

            return mesh;
        }

        private Texture2D GenerateTexture(int width,int height,TileMapMeta tilemapMeta)
        {
            Texture2D tex = new Texture2D(width,height,TextureFormat.RGB24,false);
            tex.filterMode = FilterMode.Point;
            tex.wrapMode = TextureWrapMode.Clamp;
            RefreshTexture(tex,width,height,tilemapMeta);
            return tex;
        }
        
        private void RefreshTexture(Texture2D tex,int width,int height,TileMapMeta tilemapMeta)
        {
            Color[] colors = new Color[width * height];
            for (int x = 0;x < width;x++)
            {
                for (int y = 0;y < height;y++)
                {

                    ETileTerrainType terrainType = tilemapMeta.GetTileTerrain(x, y);
                    Color col = Color.black;

                    switch (terrainType)
                    {
                        case ETileTerrainType.Ground:
                            col = Color.yellow;
                            break;
                        case ETileTerrainType.River:
                            col = Color.blue;
                            break;
                    }
                    colors[y * width + x] = col;
                }
            }
            tex.SetPixels(colors);
            tex.Apply();
        }


        public void RefreshTexture(int width,int height,TileMapMeta tilemapMeta)
        {
            RefreshTexture(_tileDataTex,width,height,tilemapMeta);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(_tileDataTex);
            _tileDataTex = null;
            
            UnityEngine.Object.Destroy(_go);
            _go = null;
        }
    }
}