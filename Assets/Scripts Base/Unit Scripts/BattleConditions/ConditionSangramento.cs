using UnityEngine;


public class ConditionSangramento : BattleConditions
{
    public ConditionSangramento(ConditionData data) : base(data)
    {
    }

    public override void AoAplicar(Unidade unidade) //inicio
    {
        Debug.Log("Unidade está sangrando");
        
        
    }

    public override void InicioDoTurno(Unidade unidade) //meio
    {
        if(unidade.PodeCurar == true)
        {
           unidade.PodeCurar = false; 
        }
        Debug.Log("Unidade Continua Sangrando");
        
    }
    public override void AoRemover(Unidade unidade) //fim
    {
        Debug.Log("Unidade parou de sangrar");
        unidade.PodeCurar = true;
    }

    

}