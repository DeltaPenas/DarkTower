using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class UnitInitiativeUI : MonoBehaviour
{
    public GameObject icone;
    public List<UnitInitiativeIcon> icones = new List<UnitInitiativeIcon>();

    public void InicializarListaDaIniciativa(List<Unidade> listaDeUnidades)
    {
        LimparListaDeIniciativa();
        Debug.Log("INICIALIZANDO ICONES");
        foreach (Unidade unidade in listaDeUnidades)
        {
            GameObject iconeObj = Instantiate(icone, transform);
            UnitInitiativeIcon icon = iconeObj.GetComponent<UnitInitiativeIcon>();
            icon.SetUnitData(unidade);

            icones.Add(icon);

        }
    }

    public void LimparListaDeIniciativa()
    {
        foreach (UnitInitiativeIcon icon in icones)
        {
            Destroy(icon.gameObject);
        }
        icones.Clear();
    }

}