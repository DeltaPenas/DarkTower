using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitFrameUi : MonoBehaviour
{
    public Unidade unidadeAtual;
    private bool estáVivo = true;

    [SerializeField] private Image unitIcon;
    [SerializeField] private TextMeshProUGUI nomeDaUnidade;
    [SerializeField] private Image barraVida;
    [SerializeField] private Image barraDano;

    [SerializeField] private Image barraMana;
    [SerializeField] private Image barraGasto;


    [SerializeField] private float delay = 0.5f;
    [SerializeField] private float velocidadeDescida = 1.5f;
    private Coroutine animacaoDano;

    public void Inicializar(Unidade unidade)
    {
        unidadeAtual = unidade;

        nomeDaUnidade.text = unidade.unitData.nome;
        unitIcon.sprite = unidade.unitData.icone;   
        Atualizar();
    }



    public void AtivarBarras()
    {
        //Barra De vida
        barraVida.fillAmount = 1;
        barraDano.fillAmount = 1;
        //Barra De Mana
        barraMana.fillAmount =1;
        barraGasto.fillAmount =1;
    }


    public void Atualizar()
    {
        AtualizarVida(unidadeAtual.vidaUnidade.vidaAtual, unidadeAtual.vidaUnidade.vidaMaxima);
        AtualizarMana(unidadeAtual.recursosUnidade.manaAtual, unidadeAtual.recursosUnidade.manaMaxima);
    }

    public void AtualizarVida(float vidaAtual, float vidaMaxima)
    {
        float porcentagem = vidaAtual / vidaMaxima;

    
        barraVida.fillAmount = porcentagem;

        
        if (animacaoDano != null)
            StopCoroutine(animacaoDano);

        animacaoDano = StartCoroutine(AnimarBarraDano(barraVida, barraDano));
    }

    public void AtualizarMana(float manaAtual, float manaMaxima)
    {
        float porcentagem = manaAtual/ manaMaxima;

        barraMana.fillAmount = porcentagem;

        if(animacaoDano != null) StopCoroutine(animacaoDano);

        animacaoDano = StartCoroutine(AnimarBarraDano(barraMana, barraGasto));

    }

    private IEnumerator AnimarBarraDano(Image barraBase, Image barraDeFundo)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("atualizando barra de vida");

        while (barraDeFundo.fillAmount > barraBase.fillAmount)
        {
            barraDeFundo.fillAmount = Mathf.Lerp(
            barraDeFundo.fillAmount,
            barraBase.fillAmount,
            Time.deltaTime * 5f
            );

            yield return null;
        }
    }



}
