using System.Collections.Generic;
using UnityEngine;


public class AttackResolver : MonoBehaviour
{

    public void ExecutarAtaque(Unidade atacante, AttackData ataque, Tile tileAlvo)
    {
        List<Unidade> alvos = EncontrarAlvos(atacante, ataque, tileAlvo);

        foreach(Unidade alvo in alvos)
        {
            AplicarAtaque(atacante, alvo, ataque);
        }

    }

    private void AplicarAtaque(Unidade atacante, Unidade alvo, AttackData attackData )
    {
        float dano = DamageCalculator.Calcular(atacante, alvo, attackData);

        alvo.ReceberDano(dano);

    }

    public List<Unidade> EncontrarAlvos(Unidade unidadeAtacante, AttackData ataqueSelecionado, Tile tileAlvo)
    {
        List<Unidade> alvos = new();
        List<Tile> tiles = EncontrarTilesAfetadas(tileAlvo, ataqueSelecionado);
        foreach(Tile tile in tiles)
        {
            if(tile.UnidadeAtual == null)
                continue;

            if(!ValidarAlvo(unidadeAtacante, tile.UnidadeAtual, ataqueSelecionado))
                continue;

            alvos.Add(tile.UnidadeAtual);
        }
       

        Debug.Log("Alvos:" + alvos);
        return alvos;
    }

    public List<Tile> EncontrarTilesAfetadas(Tile centro, AttackData ataque)
    {
        switch (ataque.areaAtaque)
        {
            case AreaAtaque.Single:
                return ObterAreaSingle(centro);
            case AreaAtaque.Cruz:
                return null; //ObterAreaCruz();
            case AreaAtaque.Quadrado:
                return ObterAreaQuadrado(centro, ataque.area);
            default:
                return new List<Tile>();
        }
        
    }
    private List<Tile> ObterTilesPorOffsets(Tile centro, Vector2Int[] offsets)
{
    List<Tile> tiles = new();

    foreach (Vector2Int offset in offsets)
    {
        Vector2Int posicao = centro.GridPosition + offset;

        Tile tile = GridManager.Instance.GetTilePos(posicao);

        if (tile != null)
        {
            tiles.Add(tile);
        }
    }

    return tiles;
}

        
    
   

    private List<Tile> ObterAreaSingle(Tile centro)
    {
        return new List<Tile>()
        {
            centro
        };
    }

    private List<Tile> ObterAreaQuadrado(Tile centro, int raio) 
    {
        List<Tile> tiles = new();

        for (int x = -raio; x <= raio; x++)
        {
            for (int y = -raio; y <= raio; y++)
            {
                Tile tile = GridManager.Instance.GetTilePos(
                    centro.GridPosition + new Vector2Int(x, y));

                if (tile != null)
                    tiles.Add(tile);
            }
        }

        return tiles;
    }
    public bool ValidarAlvo(Unidade atacante, Unidade alvo, AttackData ataque)
    {
        switch (ataque.tipoDoAlvo)
        {
            case TipoAlvo.Inimigos:
                return atacante.unitData.Team != alvo.unitData.Team;
                
            case TipoAlvo.Aliados:
                return atacante.unitData.Team == alvo.unitData.Team;

            case TipoAlvo.Todos:
                return true;

            case TipoAlvo.Eu:
                return atacante == alvo;

        }


        return false;

    }







    
    
}