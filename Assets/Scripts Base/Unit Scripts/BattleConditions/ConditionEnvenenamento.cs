using UnityEngine;


public class ConditionEnvenenamento : BattleConditions
{
    public ConditionEnvenenamento(ConditionData data) : base(data)
    {
    }

    public override void InicioDoTurno(Unidade unidade)
    {
        unidade.ReceberDano(valorEfeito);
    }


}