
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class InventarioController : MonoBehaviour
{
   public Item itemSelecionado { get ;  private set;}
   public UnitManager unitManager;
   public GameObject painelInventario;
   public GameObject painelDeItens;
   public GameObject painelDeInformações;
   public GameObject prefabItemFrame;

   [Header("PainelDeItem")]

    [SerializeField] private Image imagemDoItem;
    [SerializeField] private TextMeshProUGUI nome;
    [SerializeField] private TextMeshProUGUI descrição;

    public void Start()
    {
        unitManager = FindAnyObjectByType<UnitManager>();

    }
    public void UsarItemNaUnidadeSelecionada()
    {
        unitManager.UsarItem(itemSelecionado);
    }


    public void InicializarItensDoInventario()
    {
        Debug.Log("Inicializando inventario");
        foreach(Item item in unitManager.Inventario.itens)
        {
            GameObject itemFrame = Instantiate(prefabItemFrame, painelDeItens.transform);
            itemFrame.GetComponent<ItemFrame>().InicializarItemFrame(item);
        
        }
    }

    public void DefinirItemSelecionado(Item itemDoFrame)
    {
        itemSelecionado = itemDoFrame;
        AtualizarGUI(itemSelecionado);
    }
    



    public void FecharInventario()
    {
        LimparGUI();
        ActionMenu.Instance.FecharInventario();
    }



    public void Selecionar(Item item)
    {
        if(itemSelecionado != null) Deselecionar();

        itemSelecionado = item;

    }


    public void Deselecionar()
    {
        itemSelecionado = null;
    }


    public void AtualizarGUI(Item item)
    {
        imagemDoItem.sprite = item.Data.icone;
        nome.text = item.Data.nome;
        descrição.text = item.Data.descrição;
    }

    public void LimparGUI()
    {
        imagemDoItem.sprite = null;
        nome.text = "";
        descrição.text = "";
        
    }


}
