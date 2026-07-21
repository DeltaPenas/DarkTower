using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class ItemResolver : MonoBehaviour
{
    public void ValidarEfeitoDoItem(Item item, Unidade alvo)
    {
        if(item.Quantidade <= 0) return; //Só pra garantir caso tenha algum bug

        switch (item.Data.efeitoItem)
        {
            case ItemData.EfeitoItem.cura:
                UsarItemCura(item, alvo);
            break;
            case ItemData.EfeitoItem.mana:
                UsarItemMana(item, alvo);
            break;
            case ItemData.EfeitoItem.buff:
                UsarItemBuff(item, alvo);
            break;
            case ItemData.EfeitoItem.Reviver:
                UsarItemReviver(item, alvo);
            break;
            case ItemData.EfeitoItem.LimparEfeitos:
                LimparCondições(alvo);
            break;


        }




    }

    public void UsarItemCura(Item item, Unidade alvo)
    {
        float valorDaCura = alvo.vidaUnidade.vidaMaxima * item.Data.valor;
        
        alvo.ReceberCura(valorDaCura);

    }
    public void UsarItemMana(Item item, Unidade alvo)
    {
        float valorDaMana = alvo.recursosUnidade.manaMaxima * item.Data.valor;
        alvo.recursosUnidade.RecuperarMana(valorDaMana);
    }

    public void UsarItemBuff(Item item, Unidade alvo)
    {
        Debug.Log($"o {alvo} foi buffado pelo item {item.Data.nome}");
    }

    public void UsarItemReviver(Item item, Unidade alvo)
    {
        Debug.Log($"o {alvo} foi Revivido pelo item {item.Data.nome}");
    }

    public void LimparCondições(Unidade alvo)
    {
        Debug.Log("Limpou condição");
    }
}