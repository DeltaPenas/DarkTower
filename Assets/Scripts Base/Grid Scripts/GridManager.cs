using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int largura, altura;
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private Transform camPos;
    private Tile[,] tiles;

    private void Awake()
    {
        GerarGrid();
    }

    private static readonly Vector2Int[] direcoes =
    {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right
    };

    public List<Tile> GetVizinhos(Tile tile)
    {
        List<Tile> vizinhos = new();

        foreach (Vector2Int dir in direcoes)
        {
            Tile vizinho = GetTilePos(tile.GridPosition + dir);

            if(vizinho != null)
            {
                vizinhos.Add(vizinho);
            }
        }

        return vizinhos;

    }



    void GerarGrid()
    {
        tiles = new Tile[largura, altura];
        for (int x =0; x < largura; x++)
        {
            for(int y=0; y < altura; y++)
            {
                var spawnTile = Instantiate(tilePrefab, new Vector3(x,y, 0), Quaternion.identity, transform);
                spawnTile.name = $"Tile: {x} e {y}";

                var ehOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnTile.Inicializar(ehOffset, new Vector2Int(x, y));

                tiles[x, y] = spawnTile;
            }
        }
        camPos.transform.position = new Vector3((float)largura/2 - 0.5f,(float)altura/2 - 0.5f, -10);
    }

    public Tile GetTilePos(Vector2Int pos)
    {
        if (pos.x < 0 || pos.x >= largura)
            return null;

        if (pos.y < 0 || pos.y >= altura)
            return null;

        return tiles[pos.x, pos.y];
    }

    public List<Tile> GetTilesEmAlcance(Tile origem, int alcance)
    {
        List<Tile> resultado = new();

        Queue<(Tile tile, int distancia)> fila = new();

        HashSet<Tile> visitados = new();

        fila.Enqueue((origem, 0));

        visitados.Add(origem);

        while (fila.Count > 0)
        {
            var atual = fila.Dequeue();

            resultado.Add(atual.tile);

            if (atual.distancia >= alcance)
                continue;

            foreach (Tile vizinho in GetVizinhos(atual.tile))
            {
                if (visitados.Contains(vizinho))
                    continue;

                visitados.Add(vizinho);

                fila.Enqueue((vizinho, atual.distancia + 1));
            }
        }

        return resultado;
    }

    
        
}
