using UnityEngine;
public class PlayerUnit : Unidade
{


    public override void ReceberDano(float dano)
    {
        vidaUnidade.ReceberDano(dano);
        
        spritePisca.Piscar();
    }
    public override void ReceberCura(float cura)
    {
        vidaUnidade.Curar(cura);
        
    }
}