using System;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInfos : MonoBehaviour
{
    public TextMeshProUGUI nomeDoAtaque;
    public TextMeshProUGUI GastoMana;
    public GameObject bg;
    public GameObject icon;


    public void Inicializar(AttackData attackData)
    {
        nomeDoAtaque.text = attackData.nomeDoAtaque;
        GastoMana.text = $"Mana: {attackData.custoMana.ToString()}.";
        bg.GetComponent<UnityEngine.UI.Image>().color = attackData.elemento.cor;
        icon.GetComponent<UnityEngine.UI.Image>().sprite = attackData.elemento.icone;
        




    }
     
    
}