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

        ReCalcularFila();
                
        
    }

    private void ReCalcularFila()
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
        while (true)
        {
            indiceAtual++;

            if (indiceAtual >= fila.Count)
            {
                NovaRodada();

                if (fila.Count == 0)
                    return null;
            }

            Unidade unidade = fila[indiceAtual];

            if (!unidade.EstaMorta)
            {
                OnTurnoIniciado?.Invoke(unidade);
                return unidade;
            }
        }
    }

    public void NovaRodada()
    {
        ReCalcularFila();
    }

    public void DispararInicioTurno(Unidade unidade)
{
    OnTurnoIniciado?.Invoke(unidade);
}
    


}