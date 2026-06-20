using UnityEngine;

public class Unidade : MonoBehaviour
{
    [SerializeField] private GameObject indicadorSelecao;
    public int Vida;
    public int Movimento;
    public int Ataque;
    public Team Team;

    public Tile TileAtual {get; private set;}
    public Vector2Int GridPosition => TileAtual.GridPosition;
    

    public void Spawn(Tile tile)
    {
        TileAtual = tile;

        tile.DefinirUnidade(this);

        transform.position = tile.transform.position;

        TileAtual.sp.color = Color.green;
    }

    public void Selecionar()
    {
        indicadorSelecao.SetActive(true);
    }

    public void Deselecionar()
    {
        indicadorSelecao.SetActive(false);
    }
    public virtual void SetStatus() //Definir status da Unidade, usar mais tarde
    {
        
    }
    public virtual void Mover(Tile destino)
    {

    }

    public virtual void ReceberDano(int dano)
    {

    }



}
