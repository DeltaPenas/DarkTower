using UnityEngine;


public class ConditionEstatica : BattleConditions
{

    public override void AoAplicar(Unidade unidade) //inicio
    {
        
        Debug.Log("Unidade Está com estatica");
        
        
    }

    public override void InicioDoTurno(Unidade unidade) //meio
    {
        int valor = Random.Range(1,2);

        if(valor == 1)
        {   
            unidade.ReceberDano(valorEfeito);
            unidade.PodeMover = false;
            Debug.Log("Unidade Sofreu Estatica"); 
        }
        else
        {
            unidade.PodeMover = true;
             Debug.Log("Unidade Não Sofreu Estatica");  
        }

        
        
        
    }
    public override void AoRemover(Unidade unidade) //fim
    {
        Debug.Log("Unidade Desparalizou");
        unidade.PodeMover = true;
    }

    
    

}