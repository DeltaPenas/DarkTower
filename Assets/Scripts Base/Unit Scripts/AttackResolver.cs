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
            if(tile.UnidadeAtual != null)
            {
                if (tile.UnidadeAtual.unitData.Team != unidadeAtacante.unitData.Team)
                {
                    alvos.Add(tileAlvo.UnidadeAtual);
                } 
            }
            
        }
       


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
                return null;  //ObterAreaQuadrado();
            default:
                return new List<Tile>();
        }
        
    }

    private List<Tile> ObterAreaSingle(Tile centro)
    {
        return new List<Tile>()
        {
            centro
        };
    }





    
    
}