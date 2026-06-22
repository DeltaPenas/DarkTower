using System;
using UnityEngine;

public class ActionMenu : MonoBehaviour
{
    [SerializeField] private GameObject painelDeButõesDeAção;
    [SerializeField] private GameObject painelDeButõesDeAtaques;
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
    }
    public void EsconderMenuDeAtaques()
    {
        painelDeButõesDeAtaques.SetActive(false);
    }
    public void ButtonEntrarModoDeMovimento()
    {
        unitManager.EntrarEmModoMovimento();

        painelDeButõesDeAção.SetActive(false);
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
    public void ButtonCancelarAtaque()
    {
        EsconderMenuDeAtaques();
        MostrarMenuPrincipal();
    }
    public void ButtonEspadada()
    {
        unitManager.EntrarModoAtaque();

        painelDeButõesDeAtaques.SetActive(false);
    }


    public void ButtonBloquear()
    {
        unitManager.Bloquear();

        EsconderTudo();
    }


    


}
