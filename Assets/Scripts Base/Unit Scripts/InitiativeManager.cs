using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InitiativeManager : MonoBehaviour
{
    private List<Unidade> fila = new();
    private int indiceAtual = 0;

    public event Action<List<Unidade>> OnFilaAtualizada;
    public event Action<Unidade> OnTurnoIniciado;


    public void ConstruirFila(List<Unidade> unidades)
    {
        fila = unidades
                .OrderByDescending(u => u.GetAgilidadeAtual())
                .ToList();

                indiceAtual = 0;
                
        
    }
    public Unidade GetUnidadeAtual()
    {
        return fila[indiceAtual];
    }

    public Unidade ProximaUnidade()
    {
        indiceAtual++;

        if(indiceAtual >= fila.Count)
        {
            NovaRodada();
        }

        return fila[indiceAtual];
    }

    public void NovaRodada()
    {
        fila = fila
        .OrderByDescending(u => u.GetAgilidadeAtual())
        .ToList();

        indiceAtual = 0;
    }
    


}