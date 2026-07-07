using UnityEngine;


public class ConditionColapso : BattleConditions
{
    

    public override void InicioDoTurno(Unidade unidade)
    {
        unidade.vidaUnidade.Colapsar(valorEfeito);
    }
   
    
}