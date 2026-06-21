using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public Turno turnoAtual {get ; private set;}
    public bool MoveuNoTurno { get; private set; }

    public void Start()
    {
        IniciarTurnoPlayer();
    }





    public void IniciarTurnoPlayer()
    {
        Debug.Log("Turno do Player");
    }
    public void IniciarTurnoInimigo()
    {
        Debug.Log("Turno do Inimigo");
    }
}
