using Unity.Mathematics;
using UnityEngine;

public class ProjetilQueGira : Projetil
{

    void Update()
    {
        transform.Rotate(0, 0, -600 * Time.deltaTime);
    }
    public override void AoConcluir()
    {
        
        Destroy(gameObject);
    }
}