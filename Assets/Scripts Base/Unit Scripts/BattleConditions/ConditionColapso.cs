using UnityEngine;


public class ConditionColapso : BattleConditions
{
    public ConditionColapso(ConditionData data) : base(data)
    {
    }

    public override void InicioDoTurno(Unidade unidade)
    {
        unidade.Colapsar(ValorEfeito);
    }
   
    
}