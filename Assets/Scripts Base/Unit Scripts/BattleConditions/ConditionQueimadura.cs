using UnityEngine;


public class ConditionQueimadura : BattleConditions
{
    
    public override void InicioDoTurno(Unidade unidade)
    {
        unidade.ReceberDano(valorEfeito);
    }


}