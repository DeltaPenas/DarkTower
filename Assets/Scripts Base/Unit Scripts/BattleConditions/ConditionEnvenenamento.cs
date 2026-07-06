using UnityEngine;


public class ConditionEnvenenamento : BattleConditions
{
    
    public override void InicioDoTurno(Unidade unidade)
    {
        unidade.ReceberDano(valorEfeito);
    }


}