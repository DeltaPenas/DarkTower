using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public ActionMenu actionMenuUI;
    public Unidade unidadeSelecionada {get; private set;}
    public Unidade unidadeEmFoco { get; private set; }
    private AttackData ataqueSelecionado;
    
   
    [SerializeField] private GridManager gridManager;
    [SerializeField] private AttackResolver attackResolver;
    [SerializeField] private ItemResolver itemResolver;
    [SerializeField] private AttackExecutor attackExecutor;
    public enum ModoSelecao{ Movimento, Ataque, Nenhum}
    public ModoSelecao ModoAtual { get; private set; } = ModoSelecao.Nenhum;
    private List<Tile> tilesDestacadas = new();
    public List<GameObject> prefabEfeitos;
    public List<ItemData> itensDeTeste;
    public AttackData ataqueTeste;
    public bool unidadeEmMovimento;
    public Inventario Inventario { get; private set; }
    public InventarioController inventarioController;

    

    void Start()
    {
        Inventario = new Inventario();
        itemResolver = GetComponent<ItemResolver>();
        attackExecutor = GetComponent<AttackExecutor>();
        attackResolver = GetComponent<AttackResolver>();
        actionMenuUI = FindAnyObjectByType<ActionMenu>();
        inventarioController = FindAnyObjectByType<InventarioController>();
        CarregarInventario();
    }

    public void CarregarInventario()
    {
        foreach (ItemData data in itensDeTeste)
        {
            Inventario.Adicionar(data, 2);
            

        }

        foreach (Item i in Inventario.itens)
        {
           Debug.Log($"Item: {i.Data.nome}, Quantidade: {i.Quantidade}"); 
        }

        inventarioController.InicializarItensDoInventario();
        
    }

    public void MostrarInformações(Unidade unidade)
    {
        unidadeEmFoco = unidade;

        ActionMenu.Instance.MostrarInforButton();
        actionMenuUI.ConfigurarMenuDeInformaçõesDasUnidades(unidade);

    }
    public void FecharInformaçãoes()
    {
        unidadeEmFoco = null;
        actionMenuUI.FecharInfoButton();
    }
    
    


    public void Selecionar(Unidade unidade)
    {
        if (ModoAtual == ModoSelecao.Ataque) return;
        if (ModoAtual == ModoSelecao.Movimento) return;
        if(unidadeEmMovimento) return;
        

        if (unidadeSelecionada == unidade)
        {
            LimparSelecao();
            return;
        }

        LimparSelecao();

        MostrarInformações(unidade);

        if (unidade.unitData.Team != Team.Player)
            return;

        switch (unidade.Estado)
        {
            case EstadoUnidade.Disponivel:

                unidadeSelecionada = unidade;

                unidadeSelecionada.SetEstado(EstadoUnidade.Selecionada);
                unidadeSelecionada.Selecionar();

                ModoAtual = ModoSelecao.Nenhum;

                actionMenuUI.MostrarMenuPrincipal();

                ValidarAcoes(unidadeSelecionada);

                break;

            case EstadoUnidade.FinalizouTurno:
                Debug.Log("Essa unidade já terminou o turno.");
                break;
        }
    }

    public void LimparSelecao()
    {
        if (unidadeSelecionada != null)
        {
            unidadeSelecionada.Deselecionar();
            unidadeSelecionada = null;
        }

        FecharInformaçãoes();
        ActionMenu.Instance.EsconderTudo();

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

        tilesDestacadas = gridManager.GetTilesEmAlcance(unidadeSelecionada.TileAtual, unidadeSelecionada.GetMovimentoAtual());

        foreach (Tile tile in tilesDestacadas)
    {
        if (!tile.EstaOcupada) tile.SetVisual(TileVisual.Movimento);
    }
    }




    private void MostrarAtaque()
    {

        tilesDestacadas = gridManager.GetTilesEmAlcance( unidadeSelecionada.TileAtual, ataqueSelecionado.alcance);

        foreach (Tile tile in tilesDestacadas)
        {
            if (tile.UnidadeAtual == null)
                continue;

            if (!attackResolver.ValidarAlvo(unidadeSelecionada, tile.UnidadeAtual, ataqueSelecionado)) continue;

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
            StartCoroutine(ExecutarAtaque(tile));
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
                unidadeSelecionada.SetEstado(EstadoUnidade.FinalizouTurno);
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

    private bool ValidarMana(Unidade unidade, AttackData attackData)
    {
        if(unidade.recursosUnidade.manaAtual >= attackData.custoMana)
        {
            return  true;
        }
        else
        {
            Debug.Log("Mana Insuficiente");
            return false;
        }

    }

    private IEnumerator ExecutarAtaque(Tile tile)
    {
        if (tile.UnidadeAtual == null) yield return null;
        
        if(!attackResolver.ValidarAlvo(unidadeSelecionada, tile.UnidadeAtual, ataqueSelecionado)) yield return null;

        if(!ValidarMana(unidadeSelecionada, ataqueSelecionado)) yield return null;
        unidadeSelecionada.PerderMana(ataqueSelecionado.custoMana);

        yield return attackExecutor.Executar(unidadeSelecionada, ataqueSelecionado, tile);

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

    }

    private void AdicionarItem(ItemData itemData)
    {
        Inventario.Adicionar(itemData, 1);
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


    //Sessão dos itens


    public void UsarItem(Item item)
    {
        if(item.Quantidade <= 0) return;
        
        itemResolver.ValidarEfeitoDoItem(item, unidadeSelecionada);

        item.itemFrame.AtualizarItem();

        inventarioController.FecharInventario();

        ExecutarAcão(AcaoUnidade.Item);
        LimparSelecao();
        TurnManager.Instance.VerificarFimDoTurno();

        

       

    }




    
        

}