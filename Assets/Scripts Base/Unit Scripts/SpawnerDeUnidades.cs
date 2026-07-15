using System.Collections.Generic;
using UnityEditor.Purchasing;
using UnityEngine;

public class SpawnerDeUnidades : MonoBehaviour
{

    public List<Unidade> todasUnidades = new();
    [SerializeField] private Unidade inimigoPrefab;
    [SerializeField] private Unidade unidadeMago;
    [SerializeField] private Unidade unidadeGuarda;
    [SerializeField] private Unidade unidadeClerigo;


    [SerializeField] private GridManager grid;


    public void Start()
    {

        Tile tile = grid.GetTilePos(new Vector2Int(0, 0));
        Tile tile4 = grid.GetTilePos(new Vector2Int(0, 1));
        Tile tile5 = grid.GetTilePos(new Vector2Int(0,2));

        Spawn(unidadeMago, tile);
        Spawn(unidadeGuarda, tile4);
        Spawn(unidadeClerigo, tile5);


        Tile tile2 = grid.GetTilePos(new Vector2Int(1, 0));
        Tile tile3 = grid.GetTilePos(new Vector2Int(2,0));
        Spawn(inimigoPrefab, tile2);
        Spawn(inimigoPrefab, tile3);


        TurnManager.Instance.CarregarUiDeUnidades();



    }
    public void Spawn(Unidade prefab, Tile tile)
    {
        Unidade unidade = Instantiate(prefab);

        unidade.Spawn(tile);
        todasUnidades.Add(unidade);
        TurnManager.Instance.RegistrarUnidade(unidade);
        if (unidade.unitData.Team == Team.Player)
        {
            tile.SetVisual(TileVisual.Ocupado);
        }else 
        {
           tile.SetVisual(TileVisual.OcupadoInimigo); 
        }
        

    }

    
}