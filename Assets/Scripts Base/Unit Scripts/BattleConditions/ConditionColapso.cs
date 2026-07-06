using UnityEngine;


public class ConditionColapso : BattleConditions
{
    

    public override void InicioDoTurno(Unidade unidade)
    {
        unidade.vidaUnidade.Colapsar(valorEfeito);
    }
    public override bool PodeCurar(Unidade unidade)
    {
        return false;
    }
    public override bool RecebeMaisDano(Unidade unidade)
    {
        return true;
    }
    public override bool Colapsando(Unidade unidade)
    {
        return true;
    }
    
}