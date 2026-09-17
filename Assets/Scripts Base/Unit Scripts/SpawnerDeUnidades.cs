using System.Collections.Generic;
using UnityEditor.Purchasing;
using UnityEngine;

public class SpawnerDeUnidades : MonoBehaviour
{

    public List<Unidade> todasUnidades = new();

    [SerializeField] private List<Unidade> unidadesAliadas;
    [SerializeField] private List<Unidade> unidadesInimigas;

    [SerializeField] private Unidade unidadeMago;
    [SerializeField] private Unidade unidadeGuarda;
    [SerializeField] private Unidade unidadeClerigo;


    [SerializeField] private GridManager grid;


    public void Start()
    {
        SpawnarAliados();
        SpawnarInimigos();

       


        TurnManager.Instance.CarregarUiDeUnidades();
        TurnManager.Instance.IniciarCombate();
    }
    private void Spawn(Unidade prefab, Tile tile)
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

    private void SpawnarAliados()
    {
        int indice = 0;

        foreach(Unidade aliado in unidadesAliadas)
        {
            Tile tile = new Tile();
             tile = grid.GetTilePos(new Vector2Int(0, indice));

            Spawn(aliado, tile);

            indice++;
        }
    }
    private void SpawnarInimigos()
    {

        foreach(Unidade inimigo in unidadesInimigas)
        {
            Tile tileLivre = GridManager.Instance.GetTileAleatorioLivre();
            Spawn(inimigo, tileLivre);
        }
    }

    
   

    
}