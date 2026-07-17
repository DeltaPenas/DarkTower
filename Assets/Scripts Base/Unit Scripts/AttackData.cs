using System;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[CreateAssetMenu(menuName = "Combat/Attack Data")]
public class AttackData: ScriptableObject
{
    [Header("informações")]
    public string nomeDoAtaque;
    public string descrição;
    public AreaAtaque areaAtaque;
    public TipoAlvo tipoDoAlvo;
    public EfeitoAtaque Efeito;
    public GameObject prefabVisual;
    
    [Header("Combate")]
    public int alcance;
    public int area;
    public float multiplicadorDeDano = 1f; //Uso tanto pra multiplicar o dano quanto pra aplicar buffs/debuffs, acho que tenho q mudar esse nome
    public bool finalizaTurno = true;
    public float custoMana = 0;
    public float velocidadeVisual;

    [Header("Condições")]
    public ConditionData condicao;
    public float chanceDeCondição; 
    public int duracaoDaCondição;
    public float valorEfeito;
    

    [Header("Buffs e Debuffs")]
    public int duracao;
    public float valor;
    public TipoModificador tipoModificador;
    public UnitStatus.StatsType atributo;



    [Header("Tipos")]
    public TipoVisual tipoVisual;
    public ElementData elemento;
    
}


public enum TipoVisual
{
    fisico,
    projetil,
    area,
    direto,
}

public enum AreaAtaque
{
    Single,
    Cruz,
    Quadrado
}
public enum TipoAlvo
{
    Inimigos,
    Aliados,
    Todos,
    Eu
}
public enum EfeitoAtaque
{
    Dano,
    Cura,
    Buff,
    Debuff,
}

public enum Condicao
{
    Nenhuma,
    Sangramento, 
    Queimadura, 
    Congelamento,
    Estatica, 
    Envenenamento, 
    Maldição, 
    Colapso,

}
