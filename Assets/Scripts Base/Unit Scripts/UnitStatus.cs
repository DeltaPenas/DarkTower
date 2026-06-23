using System;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[System.Serializable]
public class UnitStatus
{
    [Header("Status Base")]
    public float vida; // vida maxima, algumas habiliades podem escalar com a vida da unidade.
    public float ataque; //as habilidades ofensivas vão ter um dano base que vai ser multiplicado pelo ataque da unidade.
    public int movimento; // quantidade de casas que uma unidade pode mover.
    public float mana; // recurso pra usar habilidades magicas.
    public float defesa; //talvez uma % pra bloqueio de danos.


    public  UnitStatus Clone()
    {
        return new UnitStatus
        {
            vida = vida,
            ataque = ataque,
            movimento = movimento,
            mana = mana,
            defesa = defesa

        };
    }

    public enum StatsType
    {
        vida,
        ataque,
        movimento,
        mana,
        defesa
    }

}