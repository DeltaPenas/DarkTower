using UnityEngine;

public class ItemResolver : MonoBehaviour
{
    public void UsarItem(Item item)
    {
        if(item.Quantidade <= 0) return; //Só pra garantir caso tenha algum bug

        switch (item.Data.efeitoItem)
        {
            case ItemData.EfeitoItem.cura:
                UsarItemCura(item, unidadeSelecionada);
            break;
            case ItemData.EfeitoItem.buff:
                UsarItemBuff(item, unidadeSelecionada);
            break;
            case ItemData.EfeitoItem.Reviver:
                UsarItemReviver(item, unidadeSelecionada);
            break;

        }




    }

    public void UsarItemCura(Item item, Unidade alvo)
    {
        
        alvo.ReceberCura(item.Data.valor);

    }

    public void UsarItemBuff(Item item, Unidade alvo)
    {
        
    }

    public void UsarItemReviver(Item item, Unidade alvo)
    {
        
    }
}