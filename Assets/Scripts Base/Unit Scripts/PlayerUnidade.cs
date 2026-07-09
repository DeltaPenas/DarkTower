using UnityEngine;
public class PlayerUnit : Unidade
{


    public override void ReceberDano(float dano)
    {
        vidaUnidade.ReceberDano(dano);
        
        spritePisca.Piscar();
        UnitUi.Instance.AtualizarVida();
    }
    public override void ReceberCura(float cura)
    {
        vidaUnidade.Curar(cura);
        UnitUi.Instance.AtualizarVida();
        
    }

    public override void PerderMana(float dano)
    {
        recursosUnidade.PerderMana(dano);
        UnitUi.Instance.AtualizarMana();
    }
    public override void GanharMana(float cura)
    {
        recursosUnidade.RecuperarMana(cura);
        UnitUi.Instance.AtualizarMana();
    }
}