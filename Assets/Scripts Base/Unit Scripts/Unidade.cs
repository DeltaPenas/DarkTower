using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unidade : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] public GameObject indicadorSelecao;
    [SerializeField] public GameObject indicadorDeBloqueio;
    [SerializeField] private VidaUnidade vidaUnidade;
    [SerializeField] private SpritePisca spritePisca;
    public UnitData unitData;

    [Header("Status")]
    public UnitStatus currentStatus;
    [Header("Ataques")]
    public List<AttackData> Ataques => unitData.ataques; 

    [Header("Infos")]

    public EstadoUnidade Estado;
    public bool Bloqueando = false;
    public Tile TileAtual {get; private set;}
    public bool EstaMovendo { get; private set; }
    public bool PodeMover = true;
    public bool PodeAgir = true;
    [SerializeField] private float velocidadeMovimento = 4f;
    public Vector2Int GridPosition => TileAtual.GridPosition;


    public void Awake()
    {
        spritePisca = GetComponent<SpritePisca>();
        vidaUnidade = GetComponent<VidaUnidade>();
        currentStatus = unitData.statusBase.Clone();
    }

    public void Spawn(Tile tile)
    {
        TileAtual = tile;

        tile.DefinirUnidade(this);
        
        

        transform.position = tile.transform.position;

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
        vidaUnidade.ReceberDano(dano);
        spritePisca.Piscar();
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
        PodeAgir = true;
        PodeMover = true;
        Desbloquear();
        Estado = EstadoUnidade.Disponivel;
    }

    public void Morrer()
    {
        TurnManager.Instance.RemoverUnidade(this);
        TileAtual.RemoverUnidade();
        TurnManager.Instance.VerificarFimDeJogo();
        Destroy(gameObject, 0.5f);
    }
    

    



}
