using UnityEngine;
public static class DamageCalculator
{
    public static float Calcular(
        Unidade atacante,
        Unidade defensor,
        AttackData ataque)
    {
        float dano = atacante.currentStatus.ataque * ataque.multiplicadorDeDano;

    dano *= defensor.ModificadorElemento(ataque.elemento);

    if (defensor.Bloqueando)
        dano *= 0.6f;

    dano -= defensor.currentStatus.defesa;
    
    dano = Mathf.Max(0.1f, dano);
    
    //verificação de imunidade
    if (defensor.imunidades.Contains(ataque.elemento))
    {
        dano = 0f;
    }


    return dano;
    }
}