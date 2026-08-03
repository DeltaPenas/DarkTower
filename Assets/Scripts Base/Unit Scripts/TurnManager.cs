using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;
    public InitiativeManager initiativeManager;
    public UnitUi unitUi;
  

    public List<Unidade> unidadesPlayer = new();
    public List<Unidade> unidadesInimigos = new();
    public Unidade unidadeAtual {get; private set;}

    public void Awake()
    {
        Instance = this;
        unitUi = FindAnyObjectByType<UnitUi>();
        initiativeManager = FindAnyObjectByType<InitiativeManager>();
        

    }

    public void CarregarUiDeUnidades()
    {
      unitUi.Inicializar(unidadesPlayer);  
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


        unidade.OnMorreu += UnidadeMorreu;
    }


    private void UnidadeMorreu(Unidade unidade)
    {
        Debug.Log($"A unidade {unidade.unitData.nome} foi de base!");
        VerificarFimDeJogo();
    }

    public void IniciarCombate()
    {
        List<Unidade> todasUnidades = new();



        
        todasUnidades.AddRange(unidadesPlayer);
        todasUnidades.AddRange(unidadesInimigos);

        initiativeManager.ConstruirFila(todasUnidades);

        Unidade primeira = initiativeManager.GetUnidadeAtual();

        if(todasUnidades.Count <= 0)
        {
            Debug.Log("Não há unidades para iniciar o combate.");
            return;
        }

        IniciarTurno(primeira);
        
    }

    private void IniciarTurno(Unidade unidade)
    {
        unidadeAtual = unidade;
        initiativeManager.DispararInicioTurno(unidade);
        

        if (unidade.EstaMorta)
        {
            FinalizarTurnoDaUnidade();
            return;
        }

        unidade.NovoTurno();

        if (unidade.unitData.Team == Team.Player)
        {
            Debug.Log($"Turno do Player, unidade Atual{unidade.unitData.nome}");
        }
        else
        {
            StartCoroutine(ExecutarTurnoInimigo(unidade));
        }


    }


 
    private void FinalizarTurnoDaUnidade()
    {
        if(unidadeAtual == null) return;
        unidadeAtual.SetEstado(EstadoUnidade.FinalizouTurno);

        Unidade proxima = initiativeManager.ProximaUnidade();


        if (proxima == null)
        {
            VerificarFimDeJogo();
            return;
        }

        IniciarTurno(proxima);



    }
    
    public void VerificarFimDoTurno()
    {
        FinalizarTurnoDaUnidade();

    }

    public void VerificarFimDeJogo()
    {
        bool existePlayerVivo = unidadesPlayer.Exists(u => !u.EstaMorta);
        bool existeInimigoVivo = unidadesInimigos.Exists(u => !u.EstaMorta);


        if (!existePlayerVivo)
        {
            Debug.Log("Derrota!");
        }else if (!existeInimigoVivo)
        {
            Debug.Log("Vitoria!");
        }
    }
    private IEnumerator ExecutarTurnoInimigo(Unidade unidade)
    {
        EnemyIA ia = unidade.GetComponent<EnemyIA>();

        if (ia != null)
        {
            yield return new WaitForSeconds(0.5f);

            unidade.indicadorSelecao.SetActive(true);

            yield return new WaitForSeconds(1f);

            yield return ia.ExecutarTurno();

            yield return new WaitForSeconds(0.5f);

            unidade.indicadorSelecao.SetActive(false);
        }

        FinalizarTurnoDaUnidade();
    }
        
        
       
    }

    
 


