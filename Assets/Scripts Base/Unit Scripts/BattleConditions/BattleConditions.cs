using UnityEngine;


public abstract class BattleConditions
{
    [Header("infos")]
    public string nome;
    public int duração;
    public float valorEfeito;
    public ConditionData data;


    public BattleConditions(ConditionData data)
    {
        this.data = data;
        duração = data.duracaoBase;
    }



    public virtual void AoAplicar(Unidade unidade){}
    public virtual void InicioDoTurno(Unidade unidade) { }
    public virtual void AoRemover(Unidade unidade) {}
    public virtual void AplicarVisual(Unidade unidade){}
    

    

}