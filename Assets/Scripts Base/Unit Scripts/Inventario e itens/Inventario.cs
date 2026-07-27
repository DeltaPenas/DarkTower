using System.Collections.Generic;
using UnityEngine;


public class Inventario
{
    public List<Item> itens = new List<Item>();

    public void Adicionar(ItemData data, int quantidade)
    {

        Item existente = itens.Find(i => i.Data == data);
        if(existente != null)
        {
            existente.Quantidade += quantidade;
            existente.itemFrame.qtd.text = existente.Quantidade.ToString();
            return;
        }

        Item novoItem = new Item();
        novoItem.Quantidade = quantidade;
        novoItem.Data = data;


        itens.Add(novoItem);
    }

    public void Remover(Item item, int quantidade)
    {
        item.Quantidade -= quantidade;

        if(item.Quantidade <= 0)
        {
            itens.Remove(item);
        }
    }
    
    public bool Contem(ItemData itemData)
    {
        foreach (var item in itens)
        {
            if (item.Data == itemData)
            {
                return true;
            }
        }
        return false;
        
    }

    public Item Buscar(ItemData itemData)
    {
        foreach (var item in itens)
        {
            if (item.Data == itemData)
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