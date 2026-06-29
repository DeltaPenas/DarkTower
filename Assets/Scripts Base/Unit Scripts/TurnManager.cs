using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;
    public Turno TurnoAtual;

    public List<Unidade> unidadesPlayer = new();
    public List<Unidade> unidadesInimigos = new();

    public void Awake()
    {
        Instance = this;
        IniciarTurnoDoPlayer();
    }


    public void RegistrarUnidade(Unidade unidade)
    {
        if (unidade.unitData.Team == Team.Player)
        {
            unidadesPlayer.Add(unidade);
        }
        else
        {
            unidadesInimigos.Add(unidade);
        }
    }

    public void IniciarTurnoDoPlayer()
    {
        Debug.Log("Turno do player");
        TurnoAtual = Turno.Player;

        foreach(Unidade unidade in unidadesPlayer)
        {
            unidade.NovoTurno();
        }
        
    }
    public void IniciarTurnoDoInimigo()
    {
        TurnoAtual = Turno.Inimigo;
        Debug.Log("Turno do inimigo");

        foreach(Unidade unidade in unidadesInimigos)
        {
            unidade.NovoTurno();
        }
        
        StartCoroutine(ExecutarTurnoInimigo());

    }



    public void VerificarFimDoTurno()
    {
        List<Unidade> lista = TurnoAtual == Turno.Player ? unidadesPlayer : unidadesInimigos; 

        foreach (Unidade unidade in lista)
    {
        if(unidade.Estado != EstadoUnidade.FinalizouTurno) return;    
        
    }
        Debug.Log("Verificando fim do turno");
        PassarTurno();

    }

    public void PassarTurno()
    {
        if(TurnoAtual == Turno.Player)
        {
            IniciarTurnoDoInimigo();
        }
        else
        {
            IniciarTurnoDoPlayer();
        }
    }

    public void VerificarFimDeJogo()
    {
        if (unidadesPlayer.Count == 0)
        {
            Debug.Log("Derrota!");
            return;
        }

        if (unidadesInimigos.Count == 0)
        {
            Debug.Log("Vitória!");
            return;
        }
    }

    public void RemoverUnidade(Unidade unidade)
    {
        if(unidade.unitData.Team == Team.Player)
        {
            unidadesPlayer.Remove(unidade);
        }
        else
        {
            unidadesInimigos.Remove(unidade);
        }
    }

    private IEnumerator ExecutarTurnoInimigo()
    {
        foreach (Unidade unidade in unidadesInimigos)
        {
            EnemyIA ia = unidade.GetComponent<EnemyIA>();

            if (ia != null)
            {
                yield return new WaitForSeconds(0.5f);

                unidade.indicadorSelecao.SetActive(true);

                yield return new WaitForSeconds(1f);

                // Espera a IA terminar o turno
                yield return ia.ExecutarTurno();

                yield return new WaitForSeconds(0.5f);

                unidade.indicadorSelecao.SetActive(false);
            }
        }

        IniciarTurnoDoPlayer();
    }
        
        
       
    }

    
 


