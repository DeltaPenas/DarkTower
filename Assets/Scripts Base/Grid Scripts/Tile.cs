using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color corBase, corSecundaria;
    [SerializeField] private SpriteRenderer sp;
    [SerializeField] private GameObject preenchimento;


    public void Inicializar(bool ehOffset)
    {
        sp.color = ehOffset ? corSecundaria : corBase;
    }

    void OnMouseEnter()
    {
        preenchimento.SetActive(true);
    }
    void OnMouseExit()
    {
       preenchimento.SetActive(false); 
    }

}
