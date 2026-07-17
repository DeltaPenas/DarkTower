using System;
using UnityEngine;

public class VidaUnidade : MonoBehaviour
{
    [SerializeField] private Unidade unidade;
    [SerializeField] public float vidaMaxima;
    [SerializeField] public float vidaAtual;
    
    void Start()
    {
        unidade = GetComponent<Unidade>();
        Inicializar(unidade.currentStatus.vida);
        
    }


    private void Inicializar(float vida)
    {
        vidaMaxima = vida;
        vidaAtual = vidaMaxima;
    }

    public void ReceberDano(float dano)
    {
        vidaAtual -=dano;
        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }
    public void Colapsar(float dano)
    {
        vidaMaxima -=dano;
        vidaAtual -=dano;
        Debug.Log($"colapsou, dano: {dano}");

        if (vidaAtual <= 0 || vidaMaxima <= 0)
        {
            Morrer();
        }

    }


    public void Curar(float cura)
    {
        vidaAtual = MathF.Min(vidaAtual + cura, vidaMaxima);
    }
    
    private void Morrer()
    {
        unidade.Morrer();
    }



}