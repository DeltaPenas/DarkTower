using UnityEngine;


public class ConditionMaldição : BattleConditions
{
    public ConditionMaldição(ConditionData data) : base(data)
    {
    }

    public override void InicioDoTurno(Unidade unidade)
    {
        unidade.PerderMana(ValorEfeito);
    }

    
    
}