using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor;
using Unity.VisualScripting;
using System;

public class ActionMenu : MonoBehaviour
{
    [SerializeField] private GameObject painelDeButõesDeAção;
    [SerializeField] private GameObject painelDeButõesDeAtaques;
    [SerializeField] private GameObject painelDeCancelarAtaque; 
    [SerializeField] private GameObject painelDeMovimento;
    [SerializeField] private GameObject PainelDeItens;
    [SerializeField] private UnitManager unitManager;
    [SerializeField] private GameObject buttonMove;
    [SerializeField] private Button[] botoesAtaque;
    [SerializeField] public static ActionMenu Instance;

    [Header("Info Menu")]
    [SerializeField] private GameObject painelDeInfoDasUnidades;
    [SerializeField] private UnitInfoPainelController unitInfoPainelController;
    


    void Awake()
    {
        unitInfoPainelController = FindAnyObjectByType<UnitInfoPainelController>();
        unitManager = FindAnyObjectByType<UnitManager>();
        Instance = this;
    }

    public void EsconderTudo()
    {
    painelDeButõesDeAção.SetActive(false);
    painelDeButõesDeAtaques.SetActive(false);
    painelDeCancelarAtaque.SetActive(false);
    painelDeMovimento.SetActive(false);
    PainelDeItens.SetActive(false);
    painelDeInfoDasUnidades.SetActive(false);

    }
   
    
    public void ChamarMenuDeInfos()
    {
        painelDeInfoDasUnidades.SetActive(true);
        unitInfoPainelController.Incializar();
    }
    public void FecharMenuInfos()
    {
        unitInfoPainelController.Limpar();
        painelDeInfoDasUnidades.SetActive(false);
        
    }
    public void MostrarInventario()
    {
        PainelDeItens.SetActive(true);
        painelDeButõesDeAção.SetActive(false);
    }
    public void FecharInventario()
    {
        PainelDeItens.SetActive(false);
        painelDeButõesDeAção.SetActive(true);
    }



    public void ConfigurarMenuDeInformaçõesDasUnidades()
    {
        
        foreach(Unidade unidade in TurnManager.Instance.unidadesPlayer)
        {
            Debug.Log(unidade.unitData.nome);
        }
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
        if (unitManager.unidadeSelecionada == null) return;
        painelDeButõesDeAtaques.SetActive(true);
        painelDeMovimento.SetActive(false);
        var ataques = unitManager.unidadeSelecionada.Ataques; //armazena os ataques da unidade selecionada

        Debug.Log(unitManager);
        Debug.Log(unitManager.unidadeSelecionada);

        for(int i =0; i < botoesAtaque.Length; i++)
        {
            if(i < ataques.Count)
            {
                botoesAtaque[i].gameObject.SetActive(true);
                ButtonInfos infos = botoesAtaque[i].GetComponentInChildren<ButtonInfos>();
                infos.Inicializar(ataques[i]);

                AttackData ataqueAtual = ataques[i];
                botoesAtaque[i].onClick.RemoveAllListeners();
                botoesAtaque[i].onClick.AddListener(() =>
                {
                    
                unitManager.SelecionarAtaque(ataqueAtual);
                painelDeButõesDeAtaques.SetActive(false);
                EsconderMenuPrincipal();
                painelDeCancelarAtaque.SetActive(true);
            
                });

            }
            else
            {
                botoesAtaque[i].gameObject.SetActive(false);
            }
        }
    
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
    public void ButtonBloquear()
    {
        unitManager.Bloquear();

    }

    public void DesabilitarButtonMove()
    {

        buttonMove.SetActive(false);
    }
    public void HabilitarButtonMove()
    {

        buttonMove.SetActive(true);
    }


    


}
