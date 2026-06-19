using UnityEngine;

public class SpawnerDeUnidades : MonoBehaviour
{
    public void Spawn(Unidade prefab, Tile tile)
    {
        Unidade unidade = Instantiate(prefab);

        unidade.Spawn(tile);
    }
}