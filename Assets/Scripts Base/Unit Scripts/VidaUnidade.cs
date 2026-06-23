using System;
using UnityEngine;

public class VidaUnidade : MonoBehaviour
{
    [SerializeField] private Unidade unidade;
    [SerializeField] private float vidaMaxima;
    [SerializeField] private float vidaAtual;
    public event Action<float> OnDamage;
    public event Action OnDeath;
    public event Action<float> OnHeal;


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
    public void Curar(float cura)
    {
        vidaAtual = MathF.Min(vidaAtual + cura, vidaMaxima);
    }
    
    private void Morrer()
    {
        Debug.Log($"A unidade {unidade} morreu");
        unidade.TileAtual.RemoverUnidade();
        Destroy(gameObject);
    }



}