using System.Collections;
using UnityEngine;

public class AttackExecutor : MonoBehaviour
{
    [SerializeField] private AttackResolver attackResolver;

    public IEnumerator Executar(Unidade atacante, AttackData ataque, Tile tileAlvo)
    {
        
        yield break;


    }

    public IEnumerator ExecutarVisual(Unidade atacante, AttackData ataque, Tile tileAlvo)
    {
        switch (ataque.tipoVisual)
        {
            case TipoVisual.projetil:
            yield return ExecutarProjetil(atacante, ataque, tileAlvo);
            break;

        }
    }



    public IEnumerator ExecutarProjetil(Unidade atacante, AttackData ataque, Tile tileAlvo)
    {
        yield break;
    }

    public IEnumerator ExecutarMelee(Unidade atacante, AttackData ataque, Tile tileAlvo)
    {
        yield break;
    }

    public IEnumerator ExecutarArea(Unidade atacante, AttackData ataque, Tile tileAlvo)
    {
        yield break;
    }



}
