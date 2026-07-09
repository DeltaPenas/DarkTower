using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unidade : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] public GameObject indicadorSelecao;
    [SerializeField] public GameObject indicadorDeBloqueio;
    [SerializeField] public VidaUnidade vidaUnidade;
    [SerializeField] public RecursosUnidade recursosUnidade;
    [SerializeField] public SpritePisca spritePisca;
    public UnitData unitData;
    

    [Header("Status")]
    public UnitStatus currentStatus;
    [Header("Ataques")]
    public List<AttackData> Ataques => unitData.ataques; 

    [Header("Infos")]

    public EstadoUnidade Estado;
    public List<StatusModifier> modificadores = new();
    public List<BattleConditions> condicoes = new();
    public bool Bloqueando = false;
    public Tile TileAtual {get; private set;}
    public bool EstaMovendo { get; private set; }
    public bool PodeMover = true;
    public bool PodeAgir = true;
    public bool PodeCurar = true;
    [SerializeField] private float velocidadeMovimento = 4f;
    public Vector2Int GridPosition => TileAtual.GridPosition;


    public void Awake()
    {
        spritePisca = GetComponent<SpritePisca>();
        vidaUnidade = GetComponent<VidaUnidade>();
        recursosUnidade = GetComponent<RecursosUnidade>();
        currentStatus = unitData.statusBase.Clone();
        

    }

    public void Spawn(Tile tile)
    {
        TileAtual = tile;

        tile.DefinirUnidade(this);
        
        

        transform.position = tile.transform.position;

    }

    void VerificarStatusAtual()
    {
        Debug.Log("Status atual: " + GetAtaqueAtual() +  " Status Base: " +  currentStatus.ataque);
    }

    public void Selecionar()
    {
        indicadorSelecao.SetActive(true);
        SetEstado(EstadoUnidade.Selecionada);
        Debug.Log($"Unidade Selecionada:{this}");
    }

    public void Deselecionar()
    {
        indicadorSelecao.SetActive(false);
        if(Estado == EstadoUnidade.Selecionada)
        {
            SetEstado(EstadoUnidade.Disponivel);
        }
        
    }
    public void Mover(List<Tile> caminho)
    {
        StartCoroutine(MoverCoroutine(caminho));
    }
    public IEnumerator MoverCoroutine(List<Tile> caminho)
    {
        EstaMovendo = true;
        ActionMenu.Instance.EsconderTudo();
        

            foreach (Tile tile in caminho)
            {
                TileAtual.RemoverUnidade();
                yield return MoverPara(tile);

                TileAtual = tile;
                TileAtual.DefinirUnidade(this);
            }

        EstaMovendo = false;
        if(unitData.Team == Team.Player)
        {
            ActionMenu.Instance.FecharPainelDeMovimento();
            ActionMenu.Instance.MostrarMenuPrincipal();
            ActionMenu.Instance.DesabilitarButtonMove();
        }
        
    }

    private IEnumerator MoverPara(Tile tile)
    {
        Vector3 inicio = transform.position;
        Vector3 fim = tile.transform.position;

        float tempo = 0f;
        
        while(tempo < 1)
        {
            tempo += Time.deltaTime * velocidadeMovimento;
            transform.position = Vector3.Lerp(inicio, fim, tempo);
            yield return null;
        }
        transform.position = fim;
    }



    public virtual void Bloquear()
    {
        Bloqueando = true;
        indicadorDeBloqueio.SetActive(true);
    }
    public virtual void Desbloquear()
    {
        Bloqueando = false;
        indicadorDeBloqueio.SetActive(false);
    }


    public void SetEstado(EstadoUnidade estado)
    {
        Estado = estado;
    }

    public virtual void ReceberDano(float dano)
    {
      
    }
    public virtual void ReceberCura(float cura)
    {
    }
    

    public float ModificadorElemento(ElementData elemento)
{
    if (unitData.resistencias.Contains(elemento))
        return 0.5f;

    if (unitData.fraquezas.Contains(elemento))
        return 1.5f;

    return 1f;
}

    public void NovoTurno()
    {
    
        Debug.Log("Novo turno");

        // Estado base da unidade
        PodeAgir = true;
        PodeMover = true;
        PodeCurar = true;

        Desbloquear();

        
        AtualizarCondicoes();

        
        AtualizarModificações();

        Estado = EstadoUnidade.Disponivel;
    }

    public void Morrer()
    {
        TurnManager.Instance.RemoverUnidade(this);
        TileAtual.RemoverUnidade();
        TurnManager.Instance.VerificarFimDeJogo();
        Destroy(gameObject, 0.5f);
    }


    //Toda Parte De Modificadores

    public float GetAtaqueAtual()
{
    float ataqueAtual = currentStatus.ataque;

    foreach(StatusModifier modificador in modificadores)
    {
        // Verifica o tipo do atributo do modificador
        if(modificador.atributo != UnitStatus.StatsType.ataque)continue;

        // Como ele altera flat (um inteiro) ou %
        switch(modificador.tipoModificador)
        {
            case TipoModificador.flat:
                ataqueAtual += modificador.valor;
                break;

            case TipoModificador.porcentagem:
                ataqueAtual *= 1 + modificador.valor;
                break;
        }
    }

    return ataqueAtual;
}

    public float GetDefesaAtual()
    {
        float defesaAtual = currentStatus.defesa;

        foreach (StatusModifier modificador in modificadores)
        {
            if(modificador.atributo != UnitStatus.StatsType.defesa) continue;

            switch(modificador.tipoModificador)
            {
                case TipoModificador.flat:
                    defesaAtual += modificador.valor;
                    break;

                case TipoModificador.porcentagem:
                    defesaAtual *= 1 + modificador.valor;
                    break;
            }
        }

        return defesaAtual;
        
    }
    public float GetVidaMaximaAtual()
    {
       float vidaMaximaAtual = currentStatus.vida;

       foreach (StatusModifier modificador in modificadores)
        {
            if(modificador.atributo != UnitStatus.StatsType.vida) continue;

            switch(modificador.tipoModificador)
            {
                case TipoModificador.flat:
                    vidaMaximaAtual += modificador.valor;
                    break;

                case TipoModificador.porcentagem:
                    vidaMaximaAtual *= 1 + modificador.valor;
                    break;
            }
        }

       return vidaMaximaAtual; 
    }

    public float GetManaAtual()
    {
       float manaMaximaAtual = currentStatus.mana;

       foreach (StatusModifier modificador in modificadores)
        {
            if(modificador.atributo != UnitStatus.StatsType.mana) continue;

            switch(modificador.tipoModificador)
            {
                case TipoModificador.flat:
                    manaMaximaAtual += modificador.valor;
                    break;

                case TipoModificador.porcentagem:
                    manaMaximaAtual *= 1 + modificador.valor;
                    break;
            }
        }

       return manaMaximaAtual;
    }
    public int GetMovimentoAtual()
    {
        int movimentoAtual = currentStatus.movimento;
        foreach (StatusModifier modificador in modificadores)
        {
            if(modificador.atributo != UnitStatus.StatsType.movimento) continue;

            switch(modificador.tipoModificador)
            {
                case TipoModificador.flat:
                    movimentoAtual += (int)modificador.valor;
                    break;

                case TipoModificador.porcentagem:
                    movimentoAtual *= (int)(1 + modificador.valor);
                    break;
            }
        }

        return movimentoAtual;
    }


    public void AdicionarModificação(StatusModifier mod)
    {
        modificadores.Add(mod);
    }
    public void AdicionarCondição(BattleConditions cond)
    {
        condicoes.Add(cond);
        cond.AoAplicar(this);
    }


    private void RemoverModificacao(int indice)
    {
        Debug.Log($"A modificação {modificadores[indice]} acabou");
        modificadores.RemoveAt(indice);
    }
    private void RemoverCondição(int indice)
    {
        Debug.Log($"Condição {condicoes[indice]}a acabou");
        condicoes[indice].AoRemover(this);
        condicoes.RemoveAt(indice);
    }

    public void AtualizarModificações()
    {
        for (int i = modificadores.Count - 1; i >= 0; i--)
        {
            modificadores[i].duracao--;

            if (modificadores[i].duracao <= 0)
            {
                RemoverModificacao(i);
            }
        }
    }
    public void AtualizarCondicoes()
    {
        for(int i = condicoes.Count -1; i >= 0; i--)
        {
            condicoes[i].duração--;
            condicoes[i].InicioDoTurno(this);

            if(condicoes[i].duração <= 0)
            {
                RemoverCondição(i);
            }
        }
    }


    public string GetTextoModificadores()
    {
        if(modificadores.Count == 0) return "nenhum";

        string texto = "";

        foreach (StatusModifier mod in modificadores)
        {
            texto += $"Mod:{ mod.nome} turnos:{mod.duracao} -  ";
        }
        return texto;
        
    }

    public string GetTextoCondições()
    {
        if(condicoes.Count == 0) return "nenhum";

        string texto = "";

        foreach (BattleConditions cond in condicoes)
        {
            texto += $"Mod:{ cond.nome} turnos:{cond.duração} -  ";
        }
        return texto;
        
    }
    

    



}
