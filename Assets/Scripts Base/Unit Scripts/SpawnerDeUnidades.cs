using System.Collections.Generic;
using UnityEditor.Purchasing;
using UnityEngine;

public class SpawnerDeUnidades : MonoBehaviour
{

    public List<Unidade> todasUnidades = new();
    [SerializeField] private Unidade unitPrefab, inimigoPrefab;
    [SerializeField] private GridManager grid;


    public void Start()
    {

        Tile tile = grid.GetTilePos(new Vector2Int(0, 0));

        Spawn(unitPrefab, tile);

        Tile tile2 = grid.GetTilePos(new Vector2Int(1, 0));
        Spawn(inimigoPrefab, tile2);



    }
    public void Spawn(Unidade prefab, Tile tile)
    {
        Unidade unidade = Instantiate(prefab);

        unidade.Spawn(tile);
        todasUnidades.Add(unidade);
        if (unidade.Team == Team.Player)
        {
            tile.SetVisual(TileVisual.Ocupado);
        }else 
        {
           tile.SetVisual(TileVisual.OcupadoInimigo); 
        }
        

    }
}