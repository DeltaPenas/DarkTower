using Unity.Mathematics;
using UnityEngine;

public class ProjetilFireball : Projetil
{
    public GameObject PrefabExplosão;
    public override void AoConcluir()
    {
        if(PrefabExplosão != null)
        {
            GameObject explosao = Instantiate(PrefabExplosão, transform.position, quaternion.identity);
        }
        
        Destroy(gameObject);
    }
}