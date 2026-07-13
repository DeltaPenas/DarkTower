using System;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[CreateAssetMenu(fileName = "Nova Condição", menuName = "Battle/Condition Data")]

public class ConditionData : ScriptableObject
{
    public string nome;
    public Condicao tipo;
    public Sprite icone;
    public Sprite efeito;
    public string descrição;

    public int duracaoBase;

}