using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemFrame : MonoBehaviour
{
    public Item itemAtual;
    public Image itemIcone;
    public TextMeshProUGUI qtd;
    public InventarioController inventarioController;


    void Start()
    {
        inventarioController = GetComponentInParent<InventarioController>();
    }



    public void InicializarItemFrame(Item item)
    {
        itemAtual = item;
        itemIcone.sprite = item.Data.icone;
        qtd.text = item.Quantidade.ToString();
        
    }

    public void DefinirItemEmDestaque()
    {
        inventarioController.DefinirItemSelecionado(itemAtual);
    }







}