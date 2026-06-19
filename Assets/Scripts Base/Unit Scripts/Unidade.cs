using UnityEngine;

public class Unidade : MonoBehaviour
{
    public int Vida;
    public int Movimento;
    public int Ataque;
    public Team Team;

    public Tile TileAtual {get; private set;}



    public virtual void Spawn(Tile tile)
    {
        TileAtual = tile;
        transform.position = tile.transform.position;
        tile.DefinirUnidade(this);
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
