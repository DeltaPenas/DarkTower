using UnityEngine;

public class EnemyIA : MonoBehaviour
{
    private Unidade unidade;
    void Awake()
    {
        unidade = GetComponent<Unidade>();
    }

    public void ExecutarTurno()
    {
        
    }

    public Unidade EncontrarAlvoMaisProximo()
    {
        Unidade alvo = null;
        int menorDistancia = int.MaxValue;

        foreach(Unidade player in TurnManager.Instance.unidadesPlayer)
        {
            int distancia = Mathf.Abs(player.GridPosition.x - unidade.GridPosition.x) + Mathf.Abs(player.GridPosition.y - unidade.GridPosition.y); //pegar a distancia

            if (distancia < menorDistancia)
            {
                menorDistancia = distancia;
                alvo = player
            }

        }

        return alvo;
    }







}
