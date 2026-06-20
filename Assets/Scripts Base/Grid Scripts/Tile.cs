using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] public Color corBase, corSecundaria;
    [SerializeField] public SpriteRenderer sp;
    public bool EstaOcupada => UnidadeAtual != null;
    public TileVisual tv;
    [SerializeField] private GameObject preenchimento;
    public bool EstaDestacado { get; private set; }
    public Unidade UnidadeAtual { get; private set; }
    public Vector2Int GridPosition { get; private set; }
    


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

    public void MostrarMovimento()
    {
        if (!EstaOcupada)
        {
            preenchimento.SetActive(true);
        }
        

        
    }
    public void LimparMovimento()
    {
        preenchimento.SetActive(false);
    }

    public void SetVisual(TileVisual tv)
    {
        switch (tv)
        {
            case TileVisual.Normal:
                break;
            case TileVisual.Ocupado:
                break;
            case TileVisual.Hover:
                break;
            case TileVisual.Movimento:
                break;
            case TileVisual.Ataque:
                break;
            case TileVisual.Caminho:
                break;
        }
    }


}
