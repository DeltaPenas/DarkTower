using System;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class ActionMenu : MonoBehaviour
{
    [SerializeField] private GameObject painelDeButõesDeAção;
    [SerializeField] private GameObject painelDeButõesDeAtaques;
    [SerializeField] private GameObject painelDeCancelarAtaque; //Apenas um botão que aparece na hora de finalizar o ataque.
    [SerializeField] private GameObject painelDeMovimento;
    [SerializeField] private GameObject PainelDeItens;
    [SerializeField] private UnitManager unitManager;
    


    void Start()
    {
        unitManager = FindAnyObjectByType<UnitManager>();
        EsconderTudo();
    
    }

    private void EsconderTudo()
    {
    painelDeButõesDeAção.SetActive(false);
    painelDeButõesDeAtaques.SetActive(false);
    PainelDeItens.SetActive(false);
    }

    public void MostrarMenuPrincipal()
    {
        painelDeButõesDeAção.SetActive(true);
    }
    public void EsconderMenuPrincipal()
    {
        painelDeButõesDeAção.SetActive(false);
    }
    public void MostrarMenuDeAtaques()
    {
        painelDeButõesDeAtaques.SetActive(true);
        painelDeMovimento.SetActive(false);
    
    }
    public void EsconderMenuDeAtaques()
    {
        painelDeButõesDeAtaques.SetActive(false);
    }
    public void ButtonEntrarModoDeMovimento()
    {
        unitManager.EntrarEmModoMovimento();

        painelDeButõesDeAção.SetActive(false);
        painelDeMovimento.SetActive(true);
    }

    public void ButtonAtacar()
    {
        painelDeButõesDeAção.SetActive(false);
        painelDeButõesDeAtaques.SetActive(true);
    }
    public void AbrirInventario()
    {
        Debug.Log("Abriu Inventario");
    }
   
    public void ButtonCancelarPrincipal()
    {
        unitManager.LimparSelecao();
        EsconderTudo();

    }
    public void ButtonVoltarMenuDeAções()
    {
        EsconderMenuDeAtaques();
        MostrarMenuPrincipal();
    }
    public void ButtonCancelarAtaque()
    {
        unitManager.LimparModos();
        painelDeButõesDeAtaques.SetActive(true);
        painelDeCancelarAtaque.SetActive(false);
    }
    public void ButtonCancelarMovimento()
    {
        painelDeMovimento.SetActive(false);
        unitManager.LimparModos();
        painelDeButõesDeAção.SetActive(true);
    }
    public void FecharPainelDeMovimento()
    {
        painelDeMovimento.SetActive(false);
    }
    public void FecharPainelDeAtaque()
    {
        painelDeCancelarAtaque.SetActive(false);
    }

    public void ButtonEspadada()
    {
        unitManager.EntrarModoAtaque();

        painelDeButõesDeAtaques.SetActive(false);
        painelDeCancelarAtaque.SetActive(true);
    }


    public void ButtonBloquear()
    {
        unitManager.Bloquear();

        EsconderTudo();
    }


    


}
