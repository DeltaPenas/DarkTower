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

    public List<Unidade> EncontrarAlvos(Unidade atacantes, AttackData ataqueSelecionado, Tile tileAlvo)
    {
        List<Unidade> alvos = new();

        if(tileAlvo.UnidadeAtual == null) return alvos;

        alvos.Add(tileAlvo.UnidadeAtual);
        

        return alvos;
    }

    public List<Tile> EncontrarTilesAfetadas(Tile centro, AttackData ataque)
    {
        List<Unidade> EncontrarAlvos()
        
    }





    
    
}