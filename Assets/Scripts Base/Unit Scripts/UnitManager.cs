using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public ActionMenu actionMenuUI;
    public Unidade unidadeSelecionada {get; private set;}
    private AttackData ataqueSelecionado;
   
    [SerializeField] private GridManager gridManager;
    public enum ModoSelecao{ Movimento, Ataque, Nenhum}
    public ModoSelecao ModoAtual { get; private set; } = ModoSelecao.Nenhum;
    private List<Tile> tilesDestacadas = new();
    public AttackData ataqueTeste;

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
                if(unidadeSelecionada.unitData.Team == Team.Player)
                {
                    unidadeSelecionada.SetEstado(EstadoUnidade.Selecionada);
                    unidadeSelecionada.Selecionar();
                    ModoAtual = ModoSelecao.Nenhum;
                    actionMenuUI.MostrarMenuPrincipal();
                }
            ValidarAcoes(unidadeSelecionada);
            Debug.Log(ModoAtual);

            break;

        case EstadoUnidade.AguardandoAção:

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

        tilesDestacadas = gridManager.GetTilesEmAlcance(unidadeSelecionada.TileAtual, unidadeSelecionada.currentStatus.movimento);

        foreach (Tile tile in tilesDestacadas)
    {
        if (!tile.EstaOcupada) tile.SetVisual(TileVisual.Movimento);
    }
    }




    private void MostrarAtaque()
    {
    tilesDestacadas = gridManager.GetTilesEmAlcance(
        unidadeSelecionada.TileAtual, ataqueSelecionado.alcance 
    );

    foreach (Tile tile in tilesDestacadas)
    {
        // Não destaca tiles ocupadas por aliados
        if (tile.UnidadeAtual != null &&
            tile.UnidadeAtual.unitData.Team == unidadeSelecionada.unitData.Team)
        {
            continue;
        }

        tile.SetVisual(TileVisual.Ataque);
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
    public void LimparModos()
    {
        Debug.Log("Voltou ao modo base (Nenhum)");
        if(unidadeSelecionada == null) return;
        if (unidadeSelecionada.Estado != EstadoUnidade.Selecionada && unidadeSelecionada.Estado != EstadoUnidade.AguardandoAção) return;
        LimparHighLight();
        ModoAtual = ModoSelecao.Nenhum; 
    }



    //Temporario Bloquear
    public void Bloquear()
    {
        ExecutarAcão(AcaoUnidade.Bloquear);
        LimparSelecao();
        TurnManager.Instance.VerificarFimDoTurno();
    }
    public void SelecionarAtaque(AttackData ataque)
    {
        ataqueSelecionado = ataque;
        EntrarModoAtaque();
    }

    private void ExecutarAtaque(Tile tile)
    {
    if (tile.UnidadeAtual == null)
        return;

    if (tile.UnidadeAtual.unitData.Team == unidadeSelecionada.unitData.Team)
        return;

    float dano = DamageCalculator.Calcular(unidadeSelecionada, tile.UnidadeAtual, ataqueSelecionado);    ////AQUI, DOIDO

    tile.UnidadeAtual.ReceberDano(dano);
    Debug.Log("Dano recebido = " + dano);

    unidadeSelecionada.SetEstado(EstadoUnidade.FinalizouTurno);

    LimparHighLight();

    ModoAtual = ModoSelecao.Nenhum;

    actionMenuUI.EsconderMenuPrincipal();
    actionMenuUI.FecharPainelDeAtaque();

    LimparSelecao();
    TurnManager.Instance.VerificarFimDoTurno();
    }

    private void ExecutarMovimento(Tile tile)
    {
        List<Tile> caminho =
            GridManager.Instance.EncontrarCaminho(
                unidadeSelecionada.TileAtual,
                tile);

        // Se não existe caminho, não faz nada
        if (caminho.Count == 0)
            return;

        unidadeSelecionada.Mover(caminho);

        unidadeSelecionada.PodeMover = false;

        LimparHighLight();

        ModoAtual = ModoSelecao.Nenhum;

        //actionMenuUI.FecharPainelDeMovimento();
        //actionMenuUI.MostrarMenuPrincipal();
        //actionMenuUI.DesabilitarButtonMove();
    }

    public void ValidarAcoes(Unidade unidade)
    {
        if (unidade.PodeMover)
        {
            actionMenuUI.HabilitarButtonMove();
        }
        else
        {
           actionMenuUI.DesabilitarButtonMove(); 
        }
    }

    
        

}