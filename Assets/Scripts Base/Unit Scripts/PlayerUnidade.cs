using UnityEngine;
public class PlayerUnit : Unidade
{


    public override void ReceberDano(float dano)
    {
        vidaUnidade.ReceberDano(dano);
        
        spritePisca.Piscar();
        UnitUi.Instance.AtualizarUi();
    }
    public override void ReceberCura(float cura)
    {
        vidaUnidade.Curar(cura);
        UnitUi.Instance.AtualizarUi();
        
    }
}