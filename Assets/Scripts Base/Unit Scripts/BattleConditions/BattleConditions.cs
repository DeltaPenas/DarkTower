using UnityEngine;


public abstract class BattleConditions
{
    public string nome;
    public int duração;
    public float valorEfeito;
    public virtual void AoAplicar(){}
    public virtual void InicioDoTurno(Unidade unidade) { }

    public virtual void FinalDoTurno(Unidade unidade) { }

    public virtual bool PodeMover(Unidade unidade){return true;}
    public virtual bool PodeCurar(Unidade unidade){return true;}
    public virtual bool RecebeMaisDano(Unidade unidade){return false;}
    public virtual bool Paralisado(Unidade unidade){return false;}
    public virtual bool PodeAtacar(Unidade unidade){return true;}
    public virtual bool SobMaldição(Unidade unidade){return false;}
    public virtual bool Colapsando(Unidade unidade){return false;}

    public virtual void AoRemover(Unidade unidade) {}

    

}