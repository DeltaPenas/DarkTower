using UnityEngine;


public abstract class BattleConditions
{
    public string nome;
    public int duração;
    public float valorEfeito;
    public virtual void AoAplicar(Unidade unidade){}
    public virtual void InicioDoTurno(Unidade unidade) { }
    public virtual void AoRemover(Unidade unidade) {}

    

}