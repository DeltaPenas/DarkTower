using System;
using UnityEngine;

public class RecursosUnidade: MonoBehaviour
{
    [SerializeField] private Unidade unidade;
    [SerializeField] private float manaMaxima;
    [SerializeField] private float manaAtual;

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