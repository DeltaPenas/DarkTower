using UnityEngine;

public abstract class Projetil : MonoBehaviour
{

    public virtual void AoInicializar(Vector3 posiçãoAlvo)
    {
        Vector2 direcao = posiçãoAlvo - transform.position;
        float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angulo);

    }

    public virtual void AoConcluir()
    {
        
    }


    
    
}
