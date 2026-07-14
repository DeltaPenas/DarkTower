using UnityEngine;

[CreateAssetMenu(fileName = "Nova Condição", menuName = "Battle/Condition Data")]

public class ConditionData : ScriptableObject
{
    public string nome;
    public Condicao tipo;
    public int duracaoBase;
    public float valorEfeito;

    [Header("Visual")]
    public GameObject prefabVisual;



}