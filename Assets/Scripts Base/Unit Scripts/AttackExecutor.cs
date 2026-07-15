using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class AttackExecutor : MonoBehaviour
{
    [SerializeField] private AttackResolver attackResolver;

    public IEnumerator Executar(Unidade atacante, AttackData ataque, Tile tileAlvo)
    {
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

        }
    }



    public IEnumerator ExecutarProjetil(Unidade atacante, AttackData ataque, Tile tileAlvo)
    {
        GameObject projetil = Instantiate(ataque.prefabVisual, atacante.transform.position, quaternion.identity);

        Vector3 destino = tileAlvo.transform.position;

        while(Vector3.Distance(projetil.transform.position, destino) > 0.001f)
        {
            projetil.transform.position = Vector3.MoveTowards
            (
                projetil.transform.position,
                destino,
                3 * Time.deltaTime

            );
            yield return null;
        }


        Destroy(projetil);
    }

    public IEnumerator ExecutarMelee(Unidade atacante, AttackData ataque, Tile tileAlvo)
    {
    
        yield return null;
        Debug.Log("golpe foi melee");
    }

    public IEnumerator ExecutarArea(Unidade atacante, AttackData ataque, Tile tileAlvo)
    {
        yield return null;
        Debug.Log("ataque foi em area");
    }



}
