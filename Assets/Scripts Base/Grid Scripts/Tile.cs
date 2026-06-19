using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color corBase, corSecundaria;
    [SerializeField] private SpriteRenderer sp;
    [SerializeField] private GameObject preenchimento;
    public Unidade UnidadeAtual { get; private set; }
    public Vector2Int GridPosition { get; private set; }
    public bool EstaOcupada => UnidadeAtual != null;


    public void Inicializar(bool ehOffset, Vector2Int pos)
    {
        GridPosition = pos;
        sp.color = ehOffset ? corSecundaria : corBase;
    }
    public void DefinirUnidade(Unidade unidade)
    {
        UnidadeAtual = unidade;
    }

    public void RemoverUnidade()
    {
        UnidadeAtual = null;
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
