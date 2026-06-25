using Unity.VisualScripting;
using UnityEngine;

public class EnemyIA : MonoBehaviour
{
    private Unidade unidade;
    [SerializeField] private AttackData ataqueBasico;
    void Awake()
    {
        unidade = GetComponent<Unidade>();
    }

    public void ExecutarTurno()
    {
        Unidade alvo = EncontrarAlvoMaisProximo();

        if(alvo == null) return; // n tem alvo > termina ação

        Debug.Log($"Alvo encontrado: {alvo.name}");

        if (EstaEmAlcance(alvo, 1))
        {
            Debug.Log("Alvo em alcance antes de mover");
            Atacar(alvo);
            Debug.Log("Sem alvo");
            return; // atacou o alvo > termina a ação
        }

        Debug.Log("Movendo");
        MoverEmDirecao(alvo); //se não está no range, move em direçao a ele

        Debug.Log($"Posição após mover: {unidade.GridPosition}");
        Debug.Log($"Posição alvo: {alvo.GridPosition}");

        if (EstaEmAlcance(alvo, 1)) //agora se está no range, ataca
        {
            Atacar(alvo);
        }
        else
        {
            Debug.Log("Ainda fora de alcance");
        }

        


    }

    public Unidade EncontrarAlvoMaisProximo()
    {
        Unidade alvo = null;
        int menorDistancia = int.MaxValue;

        foreach(Unidade player in TurnManager.Instance.unidadesPlayer)
        {
            int distancia = Mathf.Abs(player.GridPosition.x - unidade.GridPosition.x) + Mathf.Abs(player.GridPosition.y - unidade.GridPosition.y); //pegar a distancia

            if (distancia <= menorDistancia)
            {
                menorDistancia = distancia;
                alvo = player;
            }

        }

        return alvo;
    }

    private bool EstaEmAlcance(Unidade alvo, int alcance)
    {
        int distancia = Mathf.Abs(alvo.GridPosition.x - unidade.GridPosition.x) + Mathf.Abs(alvo.GridPosition.y - unidade.GridPosition.y);

        return distancia <= alcance;
    }

    private void Atacar(Unidade alvo)
    {
    float dano =
        DamageCalculator.Calcular(
            unidade,
            alvo,
            ataqueBasico);

    alvo.ReceberDano(dano);
    Debug.Log($"Unidadee: {unidade} atacou o alvo {alvo}");

    unidade.SetEstado(EstadoUnidade.FinalizouTurno);
    }

    private void MoverEmDirecao(Unidade alvo)
    {
        Vector2Int posAtual = unidade.GridPosition;

        for(int i = 0; i < unidade.currentStatus.movimento; i++)
        {
            Vector2Int proximoPasso = EscolherProximoPasso(posAtual, alvo.GridPosition);

            Tile tile = GridManager.Instance.GetTilePos(proximoPasso);

            if(tile == null || tile.EstaOcupada)
                break;

            posAtual = proximoPasso;
        }

        Tile destinoFinal = GridManager.Instance.GetTilePos(posAtual);

        if(destinoFinal != null &&
        destinoFinal != unidade.TileAtual)
        {
            unidade.Mover(destinoFinal);
        }
    }

    private Vector2Int EscolherProximoPasso( Vector2Int atual, Vector2Int alvo)
    {
        int deltaX = alvo.x - atual.x;
        int deltaY = alvo.y - atual.y;

        if (Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
        {
            return atual + new Vector2Int( deltaX > 0 ? 1 : -1, 0);
        }

        if (deltaY != 0)
        {
            return atual + new Vector2Int(0, deltaY > 0 ? 1 : -1);
        }

        return atual;
    }



}
