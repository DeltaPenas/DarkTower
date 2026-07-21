using Unity.Mathematics;
using UnityEngine;

public class InventarioController : MonoBehaviour
{
   public Item itemSelecionado { get ;  private set;}
   public UnitManager unitManager;
   public GameObject painelDeItens;
   public GameObject painelDeInformações;
   public GameObject prefabItemFrame;

    public void Start()
    {
        unitManager = FindAnyObjectByType<UnitManager>();
    }

    public void Update(){
        if (Input.GetKeyDown(KeyCode.A))
        {
            InicializarItensDoInventario();
        }
        
    }


    public void InicializarItensDoInventario()
    {
        foreach(Item item in unitManager.Inventario.itens)
        {
            GameObject itemFrame = Instantiate(prefabItemFrame, painelDeItens.transform);
            itemFrame.GetComponent<ItemFrame>().InicializarItemFrame(item);
        }
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


}
