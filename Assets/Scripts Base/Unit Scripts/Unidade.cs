using System.Collections.Generic;
using UnityEngine;

public class Unidade : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private GameObject indicadorSelecao;
    [SerializeField] private VidaUnidade vidaUnidade;

    [Header("Status")]
    public UnitStatus baseStatus;
    public UnitStatus currentStatus;
    public List<Elemento> fraquezas = new();
    public List<Elemento> resistenciais = new();
    public List<Elemento> imunidades = new();

    [Header("Infos")]

    public Team Team;
    public EstadoUnidade Estado;
    public bool Bloqueando = false;
    public Tile TileAtual {get; private set;}
    public bool PodeMover = true;
    public bool PodeAgir = true;
    public Vector2Int GridPosition => TileAtual.GridPosition;


    public void Awake()
    {
        vidaUnidade = GetComponent<VidaUnidade>();
        currentStatus = baseStatus.Clone();
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
    public virtual void SetStatus() //Definir status da Unidade, usar mais tarde
    {
        
    }

    public virtual void Mover(Tile destino)
    {
        if (destino == null)
            return;

        if (destino.EstaOcupada)
            return;

        TileAtual.RemoverUnidade();
        TileAtual.SetVisual(TileVisual.Normal);

        TileAtual = destino;

        destino.DefinirUnidade(this);

        transform.position = destino.transform.position;
        
        Debug.Log(Estado);
        if (Team == Team.Player)
        {
           destino.SetVisual(TileVisual.Ocupado);
        }
        else
        {
            destino.SetVisual(TileVisual.OcupadoInimigo);
        }
        
        
    }
    public virtual void Bloquear()
    {
        Bloqueando = true;
    }


    public void SetEstado(EstadoUnidade estado)
    {
        Estado = estado;
    }

    public virtual void ReceberDano(float dano)
    {
        vidaUnidade.ReceberDano(dano);
    }

    public float ModificadorElemento(Elemento elemento)
{
    if (resistenciais.Contains(elemento))
        return 0.5f;

    if (fraquezas.Contains(elemento))
        return 1.5f;

    return 1f;
}

    public void NovoTurno()
    {
        Debug.Log("Novo turno");
        PodeAgir = true;
        PodeMover = true;
        Bloqueando = false;
        Estado = EstadoUnidade.Disponivel;
    }
    

    



}
