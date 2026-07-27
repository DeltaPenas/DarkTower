using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RecursosUnidade: MonoBehaviour
{
    [SerializeField] private Unidade unidade;
    [SerializeField] public float manaMaxima;
    [SerializeField] public float manaAtual;

    void Start()
    {
        unidade = GetComponent<Unidade>();

        Inicializar(unidade.currentStatus.mana);
    }

    private void Inicializar(float mana)
    {
        manaMaxima = mana;
        manaAtual = manaMaxima;
    }

    public void RecuperarMana(float cura)
    {
        manaAtual = MathF.Min(manaAtual + cura, manaMaxima);
    }
    public void PerderMana(float perda)
    {
        manaAtual -=perda;
    }


}