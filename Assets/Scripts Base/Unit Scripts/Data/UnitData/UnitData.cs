using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Combat/Unit Data")]
public class UnitData : ScriptableObject
{
    [Header("Informações")]
    public string nome;
    public Sprite icone;
    public Sprite retrato;
    public GameObject prefab;
    public Classe classe;
    public Team Team;

    [Header("Status")]
    public UnitStatus statusBase; 

    [Header("Ataques")]
    public List<AttackData> ataques;

    [Header("Elementos")]
    public List<ElementData> afinidade;
    public List<ElementData> fraquezas;
    public List<ElementData> resistencias;
    public List<ElementData> imunidades;
}