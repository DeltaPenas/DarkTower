using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public Unidade unidadeSelecionada {get; private set;}
   
    [SerializeField] private GridManager gridManager;
    private List<Tile> tilesEmAlcance = new();
    public void Selecionar(Unidade unidade)
    {
        if (unidade.Estado != EstadoUnidade.Disponivel)
        {
            Debug.Log("Unidade não disponivel");
            return;
        }
        

          if (unidadeSelecionada == unidade)
        {
            LimparSelecao();
            return;
        }
        LimparSelecao();
        // Seleciona a nova
        unidadeSelecionada = unidade;
        unidadeSelecionada.Selecionar();
        // Mostra o alcance
        MostrarMovimento();
        
        
        
        

    }
    public void LimparSelecao()
    {
        if(unidadeSelecionada == null) return;

        unidadeSelecionada.Deselecionar();
        unidadeSelecionada = null;
        foreach (Tile tiles in tilesEmAlcance)
        {
            tiles.LimparMovimento();
        }
        tilesEmAlcance.Clear();
        

        
    }

    private void MostrarMovimento()
    {
        tilesEmAlcance = gridManager.GetTilesEmAlcance(
            unidadeSelecionada.TileAtual,
            unidadeSelecionada.Movimento
        );
         foreach (Tile tile in tilesEmAlcance)
        {
            tile.MostrarMovimento();
        }
    }

    public void ClicarTile(Tile tile)
    {
        if (unidadeSelecionada == null)
        {
           Debug.Log("Nenhuma Unidade selecionada!"); 
           return; 
        } 
        if (!tile.EstaDestacado)
        {
          Debug.Log("Tile não esta destacada!");   
          return;  
        } 


        unidadeSelecionada.Mover(tile);
        if(unidadeSelecionada.Team == Team.Player)
        {
            tile.SetVisual(TileVisual.Ocupado);
        }
        else
        {
            tile.SetVisual(TileVisual.OcupadoInimigo);
        }
        
        LimparSelecao();
        
        
        
       

       
    }

    
}