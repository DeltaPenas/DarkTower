using UnityEngine;

public class Unidade : MonoBehaviour
{
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
