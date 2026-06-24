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
        if (unidade.Team == Team.Player)
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
        foreach(Unidade unidade in unidadesPlayer)
        {
            unidade.NovoTurno();
        }
        Debug.Log("Turno do player");
        TurnoAtual = Turno.Player;
    }
    public void IniciarTurnoDoInimigo()
    {
        foreach(Unidade unidade in unidadesInimigos)
        {
            unidade.NovoTurno();
        }
        Debug.Log("Turno do inimigo");
        TurnoAtual = Turno.Inimigo;
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

    


}
