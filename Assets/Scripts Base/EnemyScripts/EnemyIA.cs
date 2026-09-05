using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyIA : MonoBehaviour
{
    [Header("Config interna")]
    private Unidade unidade;
    [SerializeField] private AttackExecutor attackExecutor;
    [SerializeField] private AttackResolver attackResolver;

    [Header("Ataques em Destque")]
    [SerializeField] private AttackData ataqueEmDestque;
    [SerializeField] private AttackData curaEmDestaque;
    [SerializeField] private AttackData buffEmDestaque;

    [Header("Alvos em Destaque")]
    [SerializeField] private Unidade alvoCuraEmDestaque;
    [SerializeField] private Unidade alvoBuffEmDestaque;



    [Header("valores")]
    [SerializeField] private float valorAtaqueEmDestaque;
    [SerializeField] private float valorCuraEmDestaque;
    [SerializeField] private float valorBuffEmDestaque;


    private enum AcaoInimigo
    {
        Atacar,
        Curar,
        Buffar,
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
        List<Unidade> alvos = new List<Unidade>();

        foreach(Unidade inimigo in TurnManager.Instance.unidadesPlayer)
        {
            if (inimigo.EstaMorta) continue;

            alvos.Add(inimigo);
        }

        alvos.Sort((a, b) => GetDistancia(a).CompareTo(GetDistancia(b)));

        foreach(Unidade alvo in alvos)
        {
            List<Tile> caminho = GridManager.Instance.EncontrarCaminho(unidade.TileAtual, alvo.TileAtual);

            if(caminho.Count > 0)
            {
                return alvo;
            }
        }

        return null;

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
    float AvaliarBuff(AttackData ataque)
    {
        return ataque.valorEfeito;
    }
   
    private IEnumerator ExecutarAtaque() {

        if (unidade.EstaMorta) yield return null;
        if(!attackResolver.ValidarAlvo(unidade, EncontrarAlvoMaisProximo(), ataqueEmDestque)) yield return null;
        yield return attackExecutor.Executar(unidade, ataqueEmDestque, EncontrarAlvoMaisProximo().TileAtual);

    }

    private IEnumerator ExecutarCura()
    {
        if (unidade.EstaMorta) yield break;

        if (!attackResolver.ValidarAlvo(unidade,alvoCuraEmDestaque,curaEmDestaque)) yield break;

        yield return attackExecutor.Executar(
            unidade,
            curaEmDestaque,
            alvoCuraEmDestaque.TileAtual);
    }
    private IEnumerator ExecutarBuff()
    {
        if (unidade.EstaMorta) yield break;
        if (!attackResolver.ValidarAlvo(unidade, alvoBuffEmDestaque, buffEmDestaque)) yield break;

        yield return attackExecutor.Executar(unidade, buffEmDestaque, alvoBuffEmDestaque.TileAtual);
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
