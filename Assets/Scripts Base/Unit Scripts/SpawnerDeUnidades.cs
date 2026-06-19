using UnityEngine;

public class SpawnerDeUnidades : MonoBehaviour
{
    [SerializeField] private Unidade unitPrefab;
    [SerializeField] private GridManager grid;


    public void Start()
    {

        Tile tile = grid.GetTilePos(new Vector2Int(0, 0));

        Spawn(unitPrefab, tile);



    }
    public void Spawn(Unidade prefab, Tile tile)
    {
        Unidade unidade = Instantiate(prefab);

        unidade.Spawn(tile);

    }
}