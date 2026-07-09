using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBarUi : MonoBehaviour
{
    [SerializeField] private Image barraVida;
    [SerializeField] private Image barraDano;

    [SerializeField] private float delay = 0.5f;
    [SerializeField] private float velocidadeDescida = 3f;
    private Coroutine animacaoDano;



    public void AtivarBarraDeVida()
    {
        barraVida.fillAmount = 1;
        barraDano.fillAmount = 1;
    }


    public void AtualizarVida(float vidaAtual, float vidaMaxima)
    {
        float porcentagem = vidaAtual / vidaMaxima;

    
        barraVida.fillAmount = porcentagem;

        
        if (animacaoDano != null)
            StopCoroutine(animacaoDano);

        animacaoDano = StartCoroutine(AnimarBarraDano());
    }

    private IEnumerator AnimarBarraDano()
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("atualizando barra de vida");

        while (Mathf.Abs(barraDano.fillAmount - barraVida.fillAmount) > 0.001f)
        {
            barraDano.fillAmount = Mathf.Lerp(
            barraDano.fillAmount,
            barraVida.fillAmount,
            Time.deltaTime * velocidadeDescida
            );

            yield return null;
        }

        barraDano.fillAmount = barraVida.fillAmount;
    }
}