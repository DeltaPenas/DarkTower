using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public ActionMenu actionMenuUI;
    public Unidade unidadeSelecionada {get; private set;}
   
    [SerializeField] private GridManager gridManager;
    public enum ModoSelecao{ Movimento, Ataque, Nenhum}
    public ModoSelecao ModoAtual { get; private set; } = ModoSelecao.Nenhum;
    private List<Tile> tilesDestacadas = new();

    void Start()
    {
        actionMenuUI = FindAnyObjectByType<ActionMenu>();
    }



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
            ModoAtual = ModoSelecao.Nenhum;
            actionMenuUI.MostrarMenuPrincipal();

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
        foreach (Tile tile in tilesDestacadas)
        {
            tile.RestaurarVisual();
        }

        tilesDestacadas.Clear();
    }

    private void MostrarMovimento()
    {

        tilesDestacadas = gridManager.GetTilesEmAlcance(unidadeSelecionada.TileAtual, unidadeSelecionada.Movimento);

        foreach (Tile tile in tilesDestacadas)
    {
        if (!tile.EstaOcupada) tile.SetVisual(TileVisual.Movimento);
    }
    }




    private void MostrarAtaque()
{
    tilesDestacadas = gridManager.GetTilesEmAlcance( unidadeSelecionada.TileAtual, unidadeSelecionada.AlcanceAtaque);

    foreach (Tile tile in tilesDestacadas)
    {
        if (tile.UnidadeAtual != null &&
            tile.UnidadeAtual.Team != unidadeSelecionada.Team)
        {
            tile.SetVisual(TileVisual.Ataque);
        }
    }
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

    if (!tile.EstaDestacado)
        return;

    switch (ModoAtual)
    {
        case ModoSelecao.Movimento:
            ExecutarMovimento(tile);
            break;

        case ModoSelecao.Ataque:
            ExecutarAtaque(tile);
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
        Debug.Log("Entrou modo ataque");
        if(unidadeSelecionada == null) return;
        if (unidadeSelecionada.Estado != EstadoUnidade.Selecionada && unidadeSelecionada.Estado != EstadoUnidade.AguardandoAção) return;
        LimparHighLight();
        ModoAtual = ModoSelecao.Ataque;
        MostrarAtaque();


    }
    public void EntrarEmModoMovimento()
    {
        Debug.Log("Entrou modo ataque"); 
        if(unidadeSelecionada == null) return;
        if (unidadeSelecionada.Estado != EstadoUnidade.Selecionada && unidadeSelecionada.Estado != EstadoUnidade.AguardandoAção) return;
        LimparHighLight();
        ModoAtual = ModoSelecao.Movimento;
        MostrarMovimento();
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

    if (tile.UnidadeAtual.Team == unidadeSelecionada.Team)
        return;

    tile.UnidadeAtual.ReceberDano(unidadeSelecionada.Ataque);

    unidadeSelecionada.SetEstado(EstadoUnidade.FinalizouTurno);

    LimparHighLight();

    ModoAtual = ModoSelecao.Nenhum;

    actionMenuUI.EsconderMenuPrincipal();

    LimparSelecao();
    }

    private void ExecutarMovimento(Tile tile)
{
    unidadeSelecionada.Mover(tile);

    LimparHighLight();

    ModoAtual = ModoSelecao.Nenhum;

    // volta para o menu
    actionMenuUI.MostrarMenuPrincipal();
}
        
        

}