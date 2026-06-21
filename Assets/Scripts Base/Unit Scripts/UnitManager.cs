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
    switch (unidade.Estado)
    {
        case EstadoUnidade.Disponivel:

            if (unidadeSelecionada == unidade)
            {
                LimparSelecao();
                return;
            }

            LimparSelecao();

            unidadeSelecionada = unidade;

            unidadeSelecionada.SetEstado(EstadoUnidade.Selecionada);
            unidadeSelecionada.Selecionar();

            MostrarMovimento();

            break;

        case EstadoUnidade.AguardandoAção:

            AbrirMenuDeAcoes(unidade);

            break;

        case EstadoUnidade.FinalizouTurno:

            Debug.Log("Essa unidade já terminou o turno.");

            break;
    }
}
    public void LimparSelecao()
    {
        if(unidadeSelecionada == null) return;

        unidadeSelecionada.Deselecionar();
        unidadeSelecionada = null;
        LimparMovimento();
        

        
    }
    public void LimparMovimento()
    {
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

        if (unidadeSelecionada.Team == Team.Player)
            tile.SetVisual(TileVisual.Ocupado);
        else
            tile.SetVisual(TileVisual.OcupadoInimigo);

        unidadeSelecionada.SetEstado(EstadoUnidade.AguardandoAção);

        LimparMovimento();
    }

    private void AbrirMenuDeAcoes(Unidade unidade)
    {
        Debug.Log($"Abrindo Menu de ações da unidade: {unidade}");
    }

    private void ExecutarAcão(AcaoUnidade acao)
    {
        switch (acao)
        {
            case AcaoUnidade.Atacar:
                Debug.Log("Atacou");
            break;
            case AcaoUnidade.Bloquear:
                Debug.Log("Bloqueou");
                unidadeSelecionada.Bloquear();
                unidadeSelecionada.SetEstado(EstadoUnidade.FinalizouTurno);
            break;
            case AcaoUnidade.Item:
                Debug.Log("Usou um item");
            break;
        }
    }

    //Temporario Bloquear
    public void Bloquear()
    {
        ExecutarAcão(AcaoUnidade.Bloquear);
    }

    
}