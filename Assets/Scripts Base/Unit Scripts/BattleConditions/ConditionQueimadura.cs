using UnityEngine;


public class ConditionQueimadura : BattleConditions
{
    public ConditionQueimadura(ConditionData data) : base(data)
    {
    }

    public override void InicioDoTurno(Unidade unidade)
    {
        unidade.ReceberDano(valorEfeito);
    }


}