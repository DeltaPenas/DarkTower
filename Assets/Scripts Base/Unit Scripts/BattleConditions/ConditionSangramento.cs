using UnityEngine;


public class ConditionSangramento : BattleConditions
{

    public override bool PodeCurar(Unidade unidade)
    {
        return false;
    }

    public override bool RecebeMaisDano(Unidade unidade)
    {
        return true;
    }
    

}