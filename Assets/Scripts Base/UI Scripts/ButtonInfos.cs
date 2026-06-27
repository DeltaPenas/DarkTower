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

        switch(attackData.elemento)
        {
            case Elemento.fisico:
                bg.GetComponent<UnityEngine.UI.Image>().color = Color.gray;
                icon.GetComponent<UnityEngine.UI.Image>().sprite = attackData.icone;
                break;
            case Elemento.magico:
                bg.GetComponent<UnityEngine.UI.Image>().color = Color.purple;
                icon.GetComponent<UnityEngine.UI.Image>().sprite = attackData.icone;
                break;
            case Elemento.fogo:
                bg.GetComponent<UnityEngine.UI.Image>().color = Color.orange;
                icon.GetComponent<UnityEngine.UI.Image>().sprite = attackData.icone;
                break;
            case Elemento.veneno:
                bg.GetComponent<UnityEngine.UI.Image>().color = Color.green;
                icon.GetComponent<UnityEngine.UI.Image>().sprite = attackData.icone;
                break;
            case Elemento.raio:
                bg.GetComponent<UnityEngine.UI.Image>().color = Color.yellow;
                icon.GetComponent<UnityEngine.UI.Image>().sprite = attackData.icone;
                break;
            case Elemento.gelo:
                bg.GetComponent<UnityEngine.UI.Image>().color = Color.cyan;
                icon.GetComponent<UnityEngine.UI.Image>().sprite = attackData.icone;
                break;
            case Elemento.vazio:
                bg.GetComponent<UnityEngine.UI.Image>().color = Color.rebeccaPurple;
                icon.GetComponent<UnityEngine.UI.Image>().sprite = attackData.icone;
                break;
        }
    }
     
    
}