using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] public SpriteRenderer sp;
    public bool EstaOcupada => UnidadeAtual != null;
    public TileVisual tv;
    public bool EstaDestacado { get; private set; }
    public Unidade UnidadeAtual { get; private set; }
    public Vector2Int GridPosition { get; private set; }

    public void Inicializar(bool ehOffset, Vector2Int pos)
    {
        GridPosition = pos;
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
            SetVisual(TileVisual.Movimento);
            EstaDestacado = true;

        }
        

        
    }
    public void LimparMovimento()
    {
        if (!EstaOcupada)
        {
            SetVisual(TileVisual.Normal);  
        }
        
    
        EstaDestacado = false;
    }

    public void SetVisual(TileVisual tv)
    {
        switch (tv)
        {
            case TileVisual.Normal:
                sp.color = Color.white;
                break;
            case TileVisual.Ocupado:
                sp.color = Color.green;
                break;
            case TileVisual.Hover:
                sp.color = Color.purple;
                break;
            case TileVisual.Movimento:
                sp.color = Color.yellow;
                break;
            case TileVisual.Ataque:
                sp.color = Color.darkGreen;
                break;
            case TileVisual.Caminho:
                sp.color = Color.blue;
                break;
            case TileVisual.OcupadoInimigo:
                sp.color = Color.red;
                break;
        }
    }


}
