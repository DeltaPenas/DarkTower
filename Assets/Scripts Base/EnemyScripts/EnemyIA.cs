using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyIA : MonoBehaviour
{
    private Unidade unidade;
    [SerializeField] private AttackData ataqueEmDestque;
    [SerializeField] private float valorAtaqueEmDestaque;

    [SerializeField] private AttackData curaEmDestaque;
    [SerializeField] private Unidade alvoCuraEmDestaque;
    [SerializeField] private float valorCuraEmDestaque;

    [SerializeField] private AttackResolver attackResolver;
    [SerializeField] private AttackExecutor attackExecutor;

    private enum AcaoInimigo
    {
        Atacar,
        Curar,
        Mover

    }


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

        if (alvo == null)
            yield break;

        Debug.Log(
            $"A {unidade.unitData.nome} escolheu {alvo.unitData.nome} como alvo"
        );

        EscolherAtaque();

        AttackData melhorCura = EncontrarMelhorCura();

        AcaoInimigo acao = EscolherAcao();

        switch (acao)
        {
            case AcaoInimigo.Atacar:
                yield return ExecutarAtaqueOuMover(alvo);
                break;

            case AcaoInimigo.Curar:
                yield return ExecutarCuraOuMover();
                break;

        }
    }

    private AcaoInimigo EscolherAcao()
    {
        if (valorCuraEmDestaque > valorAtaqueEmDestaque)
            return AcaoInimigo.Curar;

        return AcaoInimigo.Atacar;
    }



    private Unidade EncontrarAlvoMaisProximo()
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

    private IEnumerator ExecutarAtaqueOuMover(Unidade alvo)
    {
        if (EstaEmAlcance(alvo, ataqueEmDestque.alcance))
        {
            yield return ExecutarAtaque();
            yield break;
        }

        Debug.Log($"distancia até o alvo: {GetDistancia(alvo)}");

        yield return MoverEmDirecao(alvo);

        if (unidade.EstaMorta)
            yield break;

        if (EstaEmAlcance(alvo, ataqueEmDestque.alcance))
        {
            yield return ExecutarAtaque();
        }
    }
    private IEnumerator ExecutarCuraOuMover()
    {
        if (EstaEmAlcance(alvoCuraEmDestaque, curaEmDestaque.alcance)) {
            yield return ExecutarCura();
            yield break;
        }
        yield return MoverEmDirecao(alvoCuraEmDestaque);

        if (unidade.EstaMorta) yield break;

        if(EstaEmAlcance(alvoCuraEmDestaque, curaEmDestaque.alcance)) {  yield return ExecutarCura();}


    }



    public int GetDistancia(Unidade alvo)
    {
        int distancia = Mathf.Abs(alvo.GridPosition.x - unidade.GridPosition.x) + Mathf.Abs(alvo.GridPosition.y - unidade.GridPosition.y);

        return distancia;
    }

    private bool EstaEmAlcance(Unidade alvo, int alcance)
    {
        int distancia = GetDistancia(alvo);

        return distancia <= alcance;
    }

    private void EscolherAtaque()
    {
        ataqueEmDestque = null;
        valorAtaqueEmDestaque = -1f;

        Unidade alvo = EncontrarAlvoMaisProximo();

        if (alvo == null)
            return;

        foreach (AttackData ataque in unidade.unitData.ataques)
        {
            if (ataque.Efeito == EfeitoAtaque.Cura)
                continue;

            float valor = AvaliarAtaque(ataque, alvo);

            if (valor > valorAtaqueEmDestaque)
            {
                valorAtaqueEmDestaque = valor;
                ataqueEmDestque = ataque;
            }
        }
    }

    private AttackData EncontrarMelhorCura()
    {
        curaEmDestaque = null;
        AttackData cura = null;
        valorCuraEmDestaque = 0;

        foreach(AttackData ataque in unidade.unitData.ataques)
        {
            if (ataque.Efeito != EfeitoAtaque.Cura) continue;

            Unidade alvo = EncontrarMelhorAlvoCura(ataque);

            if (alvo == null) continue;


            float valor = AvaliarCura(alvo, ataque);

            if(valor > valorCuraEmDestaque)
            {
                valorCuraEmDestaque = valor;
                curaEmDestaque = ataque;
                alvoCuraEmDestaque = alvo;
            }
         

         

        }


        return cura;
    }

    private Unidade EncontrarMelhorAlvoCura(AttackData ataque)
    {
        Unidade melhorAlvo = null;
        float maiorValor = 0;

        foreach (Unidade aliado in TurnManager.Instance.unidadesInimigos)
        {
            float valor = AvaliarCura(aliado, ataque);

            if (valor > maiorValor)
            {
                maiorValor = valor;
                melhorAlvo = aliado;
            }
        }

        return melhorAlvo;
    }


    float AvaliarCura(Unidade aliado, AttackData ataque)
    {
        if (aliado.GetVidaAtual() == 0) return 0;
        if (aliado.EstaMorta) return 0;

        float vidaPerdida = aliado.GetVidaMaximaAtual() - aliado.GetVidaAtual();

        if (vidaPerdida <= 0) return 0;

        float cura = DamageCalculator.CalcularCura(unidade, ataque);

        return Mathf.Min(cura, vidaPerdida);

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

    private IEnumerator ExecutarCura()
    {
        if (unidade.EstaMorta) yield break;

        if (!attackResolver.ValidarAlvo(unidade,alvoCuraEmDestaque,curaEmDestaque)){ yield break;}

        yield return attackExecutor.Executar(
            unidade,
            curaEmDestaque,
            alvoCuraEmDestaque.TileAtual);
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
