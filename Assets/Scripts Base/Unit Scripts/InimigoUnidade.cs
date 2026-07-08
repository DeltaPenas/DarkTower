using UnityEngine;

public class InimigoUnidade : Unidade
{
    public HealthBarUi barraDeVida;

    public void Start()
    {
        barraDeVida.AtivarBarraDeVida();

    }

    public override void ReceberDano(float dano)
    {
        vidaUnidade.ReceberDano(dano);
        barraDeVida.AtualizarVida(vidaUnidade.vidaAtual, vidaUnidade.vidaMaxima);
        spritePisca.Piscar();
    }
    public override void ReceberCura(float cura)
    {
        vidaUnidade.Curar(cura);
        barraDeVida.AtualizarVida(vidaUnidade.vidaAtual, vidaUnidade.vidaMaxima);
    }
    
}