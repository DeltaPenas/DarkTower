using System.Collections;
using System.Collections.Generic;
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

    public IEnumerator ExecutarTurno()
    {
        Unidade alvo = EncontrarAlvoMaisProximo();

        if (alvo == null)
            yield break;

        if (EstaEmAlcance(alvo, 1))
        {
            Atacar(alvo);
            yield break;
        }

        yield return MoverEmDirecao(alvo);

        if (EstaEmAlcance(alvo, ataqueBasico.alcance))
        {
            Atacar(alvo);
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

    private IEnumerator MoverEmDirecao(Unidade alvo)
    {
    List<Tile> caminho = GridManager.Instance.EncontrarCaminho(
        unidade.TileAtual,
        alvo.TileAtual);

    if (caminho.Count <= 1)
    yield break;

    caminho.RemoveAt(caminho.Count - 1);

    if (caminho.Count > unidade.currentStatus.movimento)
    {
        caminho = caminho.GetRange(
            0,
            unidade.currentStatus.movimento);
    }

    yield return unidade.MoverCoroutine(caminho);
    }

}
