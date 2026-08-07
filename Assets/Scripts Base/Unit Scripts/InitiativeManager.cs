using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InitiativeManager : MonoBehaviour
{
    
    public List<Unidade> fila = new();
    private UnitInitiativeUI unitInitiativeUI;
    private List <Unidade> unidades = new();
    private int indiceAtual = 0;

    public event Action<List<Unidade>> OnFilaAtualizada;
    public event Action<Unidade> OnTurnoIniciado;


    private void Awake()
    {
        unitInitiativeUI = FindAnyObjectByType<UnitInitiativeUI>();
    }



    public void ConstruirFila(List<Unidade> unidadesCombate)
    {
        unidades = new List<Unidade>(unidadesCombate);

        unitInitiativeUI.LimparListaDeIniciativa();

        ReCalcularFila();

        Debug.Log("Fila construida");

        string unidadesNaFila = string.Empty;

        foreach (Unidade unidade in fila)
        {
            unidadesNaFila += unidade.unitData.nome.ToString() + ", ";  
        }
        
        

    }

    private void ReCalcularFila()
    {
        fila = unidades
            .Where(u => !u.EstaMorta)
            .OrderByDescending(u => u.GetAgilidadeAtual())
            .ToList();

        indiceAtual = 0;

        OnFilaAtualizada?.Invoke(fila);


        
        unitInitiativeUI.InicializarListaDaIniciativa(fila);
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