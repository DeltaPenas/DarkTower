using UnityEngine;


public class ConditionCongelamento : BattleConditions
{

    public override void AoAplicar(Unidade unidade) //inicio
    {
        Debug.Log("Unidade congelada");
        
        
    }

    public override void InicioDoTurno(Unidade unidade) //meio
    {
        if(unidade.PodeMover == true)
        {
           unidade.PodeMover = false; 
        }
        Debug.Log("Unidade Continua congelada");
        
    }
    public override void AoRemover(Unidade unidade) //fim
    {
        Debug.Log("Unidade Descongelou");
        unidade.PodeMover = true;
    }

  


    
    

}