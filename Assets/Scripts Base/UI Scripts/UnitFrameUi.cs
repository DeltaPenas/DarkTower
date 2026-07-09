using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitFrameUi : MonoBehaviour
{
    public Unidade unidadeAtual;

    [SerializeField] private Image unitIcon;
    [SerializeField] private TextMeshProUGUI nomeDaUnidade;
    [SerializeField] private Image barraVida;
    [SerializeField] private Image barraDano;

    [SerializeField] private Image barraMana;
    [SerializeField] private Image barraGasto;


    [SerializeField] private float delay = 0.5f;
    [SerializeField] private float velocidadeDescida = 1.5f;
    private Coroutine animacaoVida;
    private Coroutine animacaoMana;

    public void Inicializar(Unidade unidade)
    {
        unidadeAtual = unidade;

        nomeDaUnidade.text = unidade.unitData.nome;
        unitIcon.sprite = unidade.unitData.icone;   
        AtivarBarras();
        
    }



    public void AtivarBarras()
    {
        //Barra De vida
        barraVida.fillAmount = 1;
        barraDano.fillAmount = 1;
        //Barra De Mana
        barraMana.fillAmount = 1;
        barraGasto.fillAmount = 1;
    }


    public void AtualizarVida(float vidaAtual, float vidaMaxima)
    {
        float porcentagem = vidaMaxima > 0 ? vidaAtual / vidaMaxima : 0;

    
        barraVida.fillAmount = porcentagem;

        
        if (animacaoVida != null)
            StopCoroutine(animacaoVida);

        animacaoVida = StartCoroutine(AnimarBarraDano(barraVida, barraDano));
    }

    public void AtualizarMana(float manaAtual, float manaMaxima)
    {
        float porcentagem = manaMaxima > 0 ? manaAtual / manaMaxima : 0;

        barraMana.fillAmount = porcentagem;

        if(animacaoMana != null) StopCoroutine(animacaoMana);

        animacaoMana = StartCoroutine(AnimarBarraDano(barraMana, barraGasto));

    }

    private IEnumerator AnimarBarraDano(Image barraBase, Image barraDeFundo)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("atualizando barra de vida");

        while (Mathf.Abs(barraDeFundo.fillAmount - barraBase.fillAmount) > 0.001f)
        {
            barraDeFundo.fillAmount = Mathf.Lerp(
            barraDeFundo.fillAmount,
            barraBase.fillAmount,
            Time.deltaTime * velocidadeDescida
            );

            yield return null;
        }

        barraDeFundo.fillAmount = barraBase.fillAmount;
    }



}
