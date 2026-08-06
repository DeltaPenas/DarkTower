using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;
    public InitiativeManager initiativeManager;
    public UnitUi unitUi;
    public UnitManager unitManager;


    public List<Unidade> unidadesPlayer = new();
    public List<Unidade> unidadesInimigos = new();
    public Unidade unidadeAtual {get; private set;}

    public void Awake()
    {
        Instance = this;
        unitUi = FindAnyObjectByType<UnitUi>();
        unitManager = FindAnyObjectByType<UnitManager>();
        initiativeManager = FindAnyObjectByType<InitiativeManager>();
        Debug.Log(unitManager);


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
        
        Debug.Log($"A unidade {unidade.unitData.nome} morreu!");
        if (unidade == unidadeAtual)
        {
            StopAllCoroutines();
            FinalizarTurnoDaUnidade();
        }

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

        if (unidade == null)
        {
            VerificarFimDeJogo();
            return;
        }

        if (unidade.EstaMorta)
        {
  
            FinalizarTurnoDaUnidade();
            return;
        }

        unidade.NovoTurno();

        if (unidade.EstaMorta || unidade == null)
        {
            FinalizarTurnoDaUnidade();
            return;
        }


        if (unidade.unitData.Team == Team.Player)
        {


            unitManager.LimparSelecao();
            unitManager.Selecionar(unidade);

        }
        else
        { 
            StartCoroutine(ExecutarTurnoInimigo(unidade));
        }
    }



    private void FinalizarTurnoDaUnidade()
    {
        if (unidadeAtual == null) {
            Debug.Log("Unidade com turno a ser finalizado está nulo");
            return;
        } 
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
        if(unidade.EstaMorta || unidade == null)
        {
            FinalizarTurnoDaUnidade();
        }
            

        EnemyIA ia = unidade.GetComponent<EnemyIA>();

        if (ia != null)
        {
            yield return new WaitForSeconds(0.5f);



            if (unidade != null)
            {
                unidade.indicadorSelecao.SetActive(true);
            }
            else {
                FinalizarTurnoDaUnidade();
                yield break;
            }

            yield return new WaitForSeconds(1f);

            yield return ia.ExecutarTurno();

            yield return new WaitForSeconds(0.5f);

            unidade.indicadorSelecao.SetActive(false);
        }

        FinalizarTurnoDaUnidade();
    }

    public bool UnidadeValida(Unidade unidade)
    {
        return unidade != null && !unidade.EstaMorta && unidade == unidadeAtual && unidade.unitData.Team == Team.Player;
    }
        
        
       
    }

    
 


