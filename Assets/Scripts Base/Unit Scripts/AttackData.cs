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
    
    [Header("Combate")]
    public int alcance;
    public int area;
    public float multiplicadorDeDano = 1f;
    public float custoMana = 0;
    public Condicao condicao;
    public bool finalizaTurno = true;

    [Header("Buffs e Debuffs")]
    public int duracao;
    public float valor;
    public TipoModificador tipoModificador;
    public UnitStatus.StatsType atributo;



    [Header("Tipos")]
    public Tipo tipo;
    public ElementData elemento;
    
}


public enum Tipo
{
    fisico,
    magico,
    suporte
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
    Condicionar
}

public enum Condicao
{
    Sangramento,
    Queimadura,
    Congelamento,
    Paralisia,
    Maldição,
    Colapso

}
