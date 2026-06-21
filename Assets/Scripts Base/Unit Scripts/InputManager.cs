using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    [SerializeField] private UnitManager unitManager;
    
    


    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (hit.collider == null)
                return;

            Unidade unidade = hit.collider.GetComponent<Unidade>();

            if (unidade != null)
            {
                unitManager.Selecionar(unidade);
                return;
            }

            Tile tile = hit.collider.GetComponent<Tile>();

            if (tile != null)
            {
                unitManager.ClicarTile(tile);
                return;
            }
            unitManager.LimparSelecao();

        }
        //temporario para debugs;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            unitManager.Bloquear();
        }
    }



}