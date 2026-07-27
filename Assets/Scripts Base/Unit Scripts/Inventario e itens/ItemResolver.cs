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
            case ItemData.EfeitoItem.LimparEfeitos:
                LimparCondições(alvo);
            break;
        }




    }
    public bool AfetaTodos(Item item)
    {
        if(item.Data.unidadeAfetadas == ItemData.UnidadeAfetadas.Todas)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }


    public void UsarItemCura(Item item, Unidade alvo)
    {
        if (AfetaTodos(item))
        {
            foreach(Unidade unidadesAliada in TurnManager.Instance.unidadesPlayer)
            {
                float valorDaCura = unidadesAliada.GetVidaMaximaAtual() * item.Data.valor;
                unidadesAliada.ReceberCura(valorDaCura);
            }

        }
        else
        {
            float valorDaCura = alvo.GetVidaMaximaAtual() * item.Data.valor;
        
            alvo.ReceberCura(valorDaCura);
        }
        

    }
    public void UsarItemMana(Item item, Unidade alvo)
    {
        if (AfetaTodos(item))
        {
            foreach(Unidade unidadesAliada in TurnManager.Instance.unidadesPlayer)
            {
                float valorDaMana = unidadesAliada.GetManaAtual() * item.Data.valor;
                unidadesAliada.GanharMana(valorDaMana);
            }

        }
        else
        {
            float valorDaMana = alvo.recursosUnidade.manaMaxima * item.Data.valor;
            alvo.GanharMana(valorDaMana);

        }


       
        
    }

    public void UsarItemBuff(Item item, Unidade alvo)
    {
        StatusModifier mod = new StatusModifier();

        mod.nome = item.Data.nome;
        mod.atributo = item.Data.atributo;
        mod.valor = item.Data.valor;
        mod.tipoModificador = TipoModificador.porcentagem;
        mod.duracao = item.Data.duracaoEfeito;

        if (AfetaTodos(item))
        {
            foreach(Unidade unidadesAliada in TurnManager.Instance.unidadesPlayer)
            {
                
                unidadesAliada.AdicionarModificação(mod);
            }
        }
        else
        {
            alvo.AdicionarModificação(mod);
        }

        

    }

    public void LimparCondições(Unidade alvo)
    {
        Debug.Log("Limpou condição");
    }
}