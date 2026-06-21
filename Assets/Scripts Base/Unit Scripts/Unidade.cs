using UnityEngine;

public class Unidade : MonoBehaviour
{
    [SerializeField] private GameObject indicadorSelecao;
    public int Vida;
    public int Movimento;
    public int Ataque;
    public Team Team;
    public EstadoUnidade Estado;

    public Tile TileAtual {get; private set;}
    public Vector2Int GridPosition => TileAtual.GridPosition;
    

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
        SetEstado(EstadoUnidade.Moveu);
        Debug.Log(Estado);
        
    }

    public void SetEstado(EstadoUnidade estado)
    {
        switch (estado)
        {
            case EstadoUnidade.Disponivel:
                Estado = EstadoUnidade.Disponivel;
            break;
            case EstadoUnidade.FinalizouTurno:
                Estado = EstadoUnidade.FinalizouTurno;
            break;
            case EstadoUnidade.Moveu:
                Estado = EstadoUnidade.Moveu;
            break;
            case EstadoUnidade.Selecionada:
                Estado = EstadoUnidade.Selecionada;
            break;
        }
    }

    public virtual void ReceberDano(int dano)
    {

    }



}
