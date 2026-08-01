using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InitiativeManager : MonoBehaviour
{
    private List<Unidade> fila = new();
    private List <Unidade> unidades = new();
    private int indiceAtual = 0;

    public event Action<List<Unidade>> OnFilaAtualizada;
    public event Action<Unidade> OnTurnoIniciado;


    public void ConstruirFila(List<Unidade> unidadesCombate)
    {
        unidades = new List<Unidade>(unidadesCombate);

        CalcularFila();
                
        
    }

    private void CalcularFila()
    {
        fila = unidades
            .Where(u => !u.EstaMorta)
            .OrderByDescending(u => u.GetAgilidadeAtual())
            .ToList();

        indiceAtual = 0;

        OnFilaAtualizada?.Invoke(fila);
    }


    public Unidade GetUnidadeAtual()
    {
        return fila[indiceAtual];
    }

    public Unidade ProximaUnidade()
    {
        indiceAtual++;

        if (indiceAtual >= fila.Count)
        {
            NovaRodada();
        }

        Unidade atual = fila[indiceAtual];

        OnTurnoIniciado?.Invoke(atual);

        return atual;
    }

    public void NovaRodada()
    {
        CalcularFila();
    }
    


}