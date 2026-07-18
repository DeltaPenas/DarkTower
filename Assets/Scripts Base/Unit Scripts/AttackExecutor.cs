using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class AttackExecutor : MonoBehaviour
{
    [SerializeField] private AttackResolver attackResolver;

    public IEnumerator Executar(Unidade atacante, AttackData ataque, Tile tileAlvo)
    {
        if(!attackResolver.ValidarAlvo(atacante, tileAlvo.UnidadeAtual, ataque))  yield break;

        if (atacante.Estado == EstadoUnidade.FinalizouTurno)
        {
            Debug.Log("essa unidade ja atacou");
            yield break;
        }

        atacante.SetEstado(EstadoUnidade.FinalizouTurno);
        ActionMenu.Instance.EsconderTudo();
        
        yield return ExecutarVisual(atacante, ataque, tileAlvo);

        attackResolver.ExecutarAtaque(atacante, ataque, tileAlvo);
        
        

    }

    public IEnumerator ExecutarVisual(Unidade atacante, AttackData ataque, Tile tileAlvo)
    {
        switch (ataque.tipoVisual)
        {
            case TipoVisual.projetil:
            yield return ExecutarProjetil(atacante, ataque, tileAlvo);
            break;
            
            case TipoVisual.fisico:
            yield return ExecutarMelee(atacante, ataque, tileAlvo);
            break;

            case TipoVisual.area:
            yield return ExecutarArea(atacante, ataque, tileAlvo);
            break;

            case TipoVisual.direto:
            yield return ExecutarDireto(atacante, ataque, tileAlvo);
            break;

        }
    }



    public IEnumerator ExecutarProjetil(Unidade atacante, AttackData ataque, Tile tileAlvo)
    {
        GameObject projetil = Instantiate(ataque.prefabVisual, atacante.transform.position, quaternion.identity);
        Projetil proj = projetil.GetComponent<Projetil>();

        proj.AoInicializar(tileAlvo.transform.position);

        Vector3 destino = tileAlvo.transform.position;

        while(Vector3.Distance(projetil.transform.position, destino) > 0.001f)
        {
            projetil.transform.position = Vector3.MoveTowards
            (
                projetil.transform.position,
                destino,
                ataque.velocidadeVisual * Time.deltaTime

            );
            yield return null;
        }


        proj.AoConcluir();
    }

    public IEnumerator ExecutarMelee(Unidade atacante, AttackData ataque, Tile tileAlvo)
    {
        Vector3 posInicial = atacante.transform.position;
        Vector3 posFinal = tileAlvo.transform.position;
        Vector3 posCentro = (posInicial + posFinal)/2;

        GameObject efeitoVisual = Instantiate(ataque.prefabVisual, posCentro, quaternion.identity);  

        yield return new WaitForSeconds(0.1f); 
        
    }
    public IEnumerator ExecutarDireto(Unidade atacante, AttackData ataque, Tile tileAlvo)
    {
        GameObject efeitoVisual = Instantiate(ataque.prefabVisual, tileAlvo.transform.position, quaternion.identity);

        yield return new WaitForSeconds(0.1f); 

       
        
    }

    public IEnumerator ExecutarArea(Unidade atacante, AttackData ataque, Tile tileAlvo)
    {
        yield return null;
        Debug.Log("ataque foi em area");
    }



}
