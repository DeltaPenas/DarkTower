using System.Collections.Generic;
using UnityEngine;


public class Inventario
{
    private List<Item> itens = new List<Item>();

    public void Adicionar(ItemData itemData, int quantidade)
    {

    }

    public void Remover(Item item, int quantidade)
    {

    }
    
    public bool Contem(ItemData itemData)
    {
        foreach (var item in itens)
        {
            if (item.data == itemData)
            {
                return true;
                break;
            }
        }
        return false;
        
    }

    public Item Buscar(ItemData itemData)
    {
        foreach (var item in itens)
        {
            if (item.data == itemData)
            {
                return item;
            }
        }
        return null;
    }
    

    public List<Item> ObterItens()
    {
        return itens;
    }
}