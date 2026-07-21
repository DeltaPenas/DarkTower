using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemFrame : MonoBehaviour
{
    public Item itemAtual;
    public Image itemIcone;
    public TextMeshProUGUI qtd;
    


    public void InicializarItemFrame(Item item)
    {
        itemAtual = item;
        itemIcone.sprite = item.Data.icone;
        qtd.text = item.Quantidade.ToString();
        
    }







}