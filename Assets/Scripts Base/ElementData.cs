using System;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[CreateAssetMenu(menuName = "RPG/Element Data")]
public class ElementData: ScriptableObject
{
    [Header("informações")]
    public string nome;
    public string descrição;
    public Sprite icone;
    public Color cor;

}


