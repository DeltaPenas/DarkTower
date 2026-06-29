using System;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[CreateAssetMenu(menuName = "Combat/Attack Data")]
public class AttackData: ScriptableObject
{
    [Header("informações")]
    public string nomeDoAtaque;
    public string descrição;
    
    [Header("Combate")]
    public int alcance;
    public float multiplicadorDeDano = 1f;
    public float custoMana = 0;
    public bool finalizaTurno = true;

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
