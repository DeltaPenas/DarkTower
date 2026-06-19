using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
   [SerializeField] private int largura, altura;
   [SerializeField] private Tile tilePrefab;
   [SerializeField] private Transform camPos;
   private Dictionary<Vector2, Tile> tiles;

    void Start()
    {
        GerarGrid();
    }

    void GerarGrid()
    {
        tiles = new Dictionary<Vector2, Tile>();
        for (int x =0; x < largura; x++)
        {
            for(int y=0; y < altura; y++)
            {
                var spawnTile = Instantiate(tilePrefab, new Vector3(x,y, 0), Quaternion.identity, transform);
                spawnTile.name = $"Tile: {x} e {y}";

                var ehOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnTile.Inicializar(ehOffset);

                tiles[new Vector2(x, y)] = spawnTile;
            }
        }
        camPos.transform.position = new Vector3((float)largura/2 - 0.5f,(float)altura/2 - 0.5f, -10);
    }

    public Tile GetTilePos(Vector2 pos)
    {
        if(tiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }
        return null;
    }
}
