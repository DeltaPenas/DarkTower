using UnityEngine;


public class ConditionParalisado : BattleConditions
{

    public override bool Paralisado(Unidade unidade)
    {
        int i = Random.Range(1,2);


        if(i == 1)
        {
            return true;
        }
        else
        {
            return false;
        }

        //50% De chance de paralisar
        
    }
    

}