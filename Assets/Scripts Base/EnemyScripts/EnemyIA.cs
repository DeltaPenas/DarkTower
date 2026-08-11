using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyIA : MonoBehaviour
{
    private Unidade unidade;
    [SerializeField] private AttackData ataqueEmDestque;
    [SerializeField] private AttackResolver attackResolver;
    [SerializeField] private AttackExecutor attackExecutor;
    void Awake()
    {
        unidade = GetComponent<Unidade>();
        attackResolver = FindAnyObjectByType<AttackResolver>();
        attackExecutor = FindAnyObjectByType<AttackExecutor>();
    }

    public IEnumerator ExecutarTurno()
    {
        if (unidade.EstaMorta)
            yield break;

        Unidade alvo = EncontrarAlvoMaisProximo();
        EscolherAtaque();

        if (alvo == null)
            yield break;

        
        yield return MoverEmDirecao(alvo);
        if (unidade.EstaMorta)
            yield break;

        if (EstaEmAlcance(alvo, ataqueEmDestque.alcance))
        {
          
           StartCoroutine(ExecutarAtaque());
        }
    }

    public Unidade EncontrarAlvoMaisProximo()
    {
       
        Unidade alvo = null;
        int menorDistancia = int.MaxValue;

   

        foreach (Unidade player in TurnManager.Instance.unidadesPlayer)
        {
            if (player.EstaMorta) continue;

            int distancia = Mathf.Abs(player.GridPosition.x - unidade.GridPosition.x) +
                Mathf.Abs(player.GridPosition.y - unidade.GridPosition.y); //pegar a distancia

            if (distancia < menorDistancia)
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

    private void EscolherAtaque()
    {
        float maiorValor = -1f;

        foreach(AttackData ataque in unidade.unitData.ataques)
        {
            float valor = AvaliarAtaque(ataque, EncontrarAlvoMaisProximo());

            if (valor > maiorValor)
            {
                maiorValor = valor;
                ataqueEmDestque = ataque;
            }
        }


    }
    float AvaliarAtaque(AttackData ataque, Unidade alvo)
    {
        if (alvo.unitData.imunidades.Contains(ataque.elemento))
            return 0;

        float valor = 1f;

        if (alvo.unitData.resistencias.Contains(ataque.elemento))
            valor = 0.5f;
        else if (alvo.unitData.fraquezas.Contains(ataque.elemento))
            valor = 1.5f;

        return valor;
    }

   
    private IEnumerator ExecutarAtaque() {

        if (unidade.EstaMorta) yield return null;
        if(!attackResolver.ValidarAlvo(unidade, EncontrarAlvoMaisProximo(), ataqueEmDestque)) yield return null;
        yield return attackExecutor.Executar(unidade, ataqueEmDestque, EncontrarAlvoMaisProximo().TileAtual);

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
