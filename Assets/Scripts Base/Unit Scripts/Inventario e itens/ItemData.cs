using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[CreateAssetMenu(menuName = "Combat/ Item Data")]

public class ItemData : ScriptableObject
{
    [Header("Informações")]

    public string nome;
    public string descrição;
    public Sprite icone;
    public float valor;
    public int duracaoEfeito;
    public EfeitoItem efeitoItem;
    public GameObject efeitoVisual;
    public UnitStatus.StatsType atributo;
    public float preço;
    public UnidadeAfetadas unidadeAfetadas;



    public enum UnidadeAfetadas
    {
        Uma,
        Todas,
        
    }
    public enum EfeitoItem
    {
        cura,
        mana,
        buff,
        LimparEfeitos,
    }
    
}