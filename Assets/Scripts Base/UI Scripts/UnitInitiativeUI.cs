using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class UnitInitiativeUI : MonoBehaviour
{
    public GameObject icone;
    public List<UnitInitiativeIcon> icones = new List<UnitInitiativeIcon>();
    public InitiativeManager initiativeManager;


    private void Start()
    {
        initiativeManager = FindAnyObjectByType<InitiativeManager>();
        initiativeManager.OnTurnoIniciado += DestacarUnidade;
    }

    public void InicializarListaDaIniciativa(List<Unidade> listaDeUnidades)
    {
        
        Debug.Log("INICIALIZANDO ICONES");
        foreach (Unidade unidade in listaDeUnidades)
        {
            GameObject iconeObj = Instantiate(icone, transform);
            UnitInitiativeIcon icon = iconeObj.GetComponent<UnitInitiativeIcon>();
            icon.SetUnitData(unidade);

            icones.Add(icon);

         

        }

        DestacarUnidade(listaDeUnidades[0]);
    }
    private void DestacarUnidade(Unidade unidade)
    {
        foreach(UnitInitiativeIcon icon in icones)
        {
            if (icon.Unidade == unidade)
            {
                icon.Destacar();
            }
            else
            {
                icon.RemoverDestaque();
            }
        }
    }


    public void LimparListaDeIniciativa()
    {
        foreach (UnitInitiativeIcon icon in icones)
        {
            Destroy(icon.gameObject);
            Debug.Log("DESTROY ICON");
        }
        icones.Clear();
    }


    private void OnDestroy()
    {
        if (initiativeManager != null)
            initiativeManager.OnTurnoIniciado -= DestacarUnidade;
    }

}