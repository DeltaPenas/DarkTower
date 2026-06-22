using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public Unidade unidadeSelecionada {get; private set;}
   
    [SerializeField] private GridManager gridManager;
    public enum ModoSelecao{ Movimento, Ataque, Nenhum}
    public ModoSelecao ModoAtual { get; private set; } = ModoSelecao.Nenhum;
    private List<Tile> tilesDestacadas = new();
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
            ModoAtual = ModoSelecao.Movimento;
            Debug.Log(ModoAtual);

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
        LimparHighLight();
        

        
    }
    public void LimparHighLight()
    {
        foreach (Tile tiles in tilesDestacadas)
        {
           MostrarTiles(tilesDestacadas, TileVisual.Normal);
        }
        tilesDestacadas.Clear();
    }

    private void MostrarMovimento()
    {
    tilesDestacadas = gridManager.GetTilesEmAlcance(
        unidadeSelecionada.TileAtual,
        unidadeSelecionada.Movimento
    );

        MostrarTiles(tilesDestacadas, TileVisual.Movimento);
    }

    private void MostrarAtaque()
    {
        tilesDestacadas = gridManager.GetTilesEmAlcance(
            unidadeSelecionada.TileAtual,
            unidadeSelecionada.AlcanceAtaque
        );

        MostrarTiles(tilesDestacadas, TileVisual.Ataque);
    }


    private void MostrarTiles(List<Tile> tiles, TileVisual visual)
    {
        foreach (Tile tile in tiles)
        {
            if (!tile.EstaOcupada)
            {
                tile.SetVisual(visual);
            }
            
        }
    }

    public void ClicarTile(Tile tile)
{
    if (unidadeSelecionada == null)
        return;

    switch (ModoAtual)
    {
        case ModoSelecao.Movimento:

            if (!tile.EstaDestacado)
                return;

            unidadeSelecionada.Mover(tile);

            unidadeSelecionada.SetEstado(EstadoUnidade.AguardandoAção);

            ModoAtual = ModoSelecao.Nenhum;

            LimparHighLight();

            break;

        case ModoSelecao.Ataque:

            if (!tile.EstaDestacado)
                return;

            ExecutarAtaque(tile);

            ModoAtual = ModoSelecao.Nenhum;

            LimparHighLight();

            unidadeSelecionada.SetEstado(EstadoUnidade.FinalizouTurno);

            LimparSelecao();

            break;
    }
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

    public void EntrarModoAtaque()
    {
        if(unidadeSelecionada == null) return;
        if(unidadeSelecionada.Estado != EstadoUnidade.AguardandoAção) return;
        LimparHighLight();
        ModoAtual = ModoSelecao.Ataque;
        MostrarAtaque();


    }

    //Temporario Bloquear
    public void Bloquear()
    {
        ExecutarAcão(AcaoUnidade.Bloquear);
    }

    private void ExecutarAtaque(Tile tile)
    {
         if (tile.UnidadeAtual == null)
            return;
        Unidade alvo = tile.UnidadeAtual;

        if (alvo.Team == unidadeSelecionada.Team)
            return;
        alvo.ReceberDano(unidadeSelecionada.Ataque);
        }

    
}