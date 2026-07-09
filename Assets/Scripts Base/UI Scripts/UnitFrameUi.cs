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
    [SerializeField] private TextMeshProUGUI textoVida;
    [SerializeField] private TextMeshProUGUI textoMana;

    [SerializeField] private Image barraVida;
    [SerializeField] private Image barraDano;

    [SerializeField] private Image barraMana;
    [SerializeField] private Image barraGasto;


    [SerializeField] private float delay = 0.5f;
    [SerializeField] private float velocidadeDescida = 3f;
    private Coroutine animacaoVida;
    private Coroutine animacaoMana;

    public void Inicializar(Unidade unidade)
    {
        unidadeAtual = unidade;

        nomeDaUnidade.text = unidade.unitData.nome;
        unitIcon.sprite = unidade.unitData.icone;   
        AtivarBarras();
        
    }

    void Start()
    {
        AtualizarTextoMana(unidadeAtual);
        AtualizarTextoVidas(unidadeAtual);
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
    public void AtualizarTextoVidas(Unidade unidade)
    {
       textoVida.text = $"HP {(int)unidade.vidaUnidade.vidaAtual}/{(int)unidade.vidaUnidade.vidaMaxima}"; 
    }
    public void AtualizarTextoMana(Unidade unidade)
    {
       textoMana.text = $"MP {(int)unidade.recursosUnidade.manaAtual}/{(int)unidade.recursosUnidade.manaMaxima}"; 
    }


    public void AtualizarVida(float vidaAtual, float vidaMaxima)
    {
        float porcentagem = vidaMaxima > 0 ? vidaAtual / vidaMaxima : 0;
        AtualizarTextoVidas(unidadeAtual);

        barraVida.fillAmount = porcentagem;

        
        if (animacaoVida != null)
            StopCoroutine(animacaoVida);

        animacaoVida = StartCoroutine(AnimarBarraDano(barraVida, barraDano));
    }

    public void AtualizarMana(float manaAtual, float manaMaxima)
    {
        float porcentagem = manaMaxima > 0 ? manaAtual / manaMaxima : 0;
        AtualizarTextoMana(unidadeAtual);

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
