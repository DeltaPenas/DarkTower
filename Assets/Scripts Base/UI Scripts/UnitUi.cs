using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitUi : MonoBehaviour
{
    [SerializeField] private GameObject framePrefab;
    public static UnitUi Instance;
    public List <Unidade> unidadesDoPlayer;
    public List <UnitFrameUi> frames;

    public void Awake()
    {
        Instance = this;
    }


    public void Inicializar(List <Unidade> unidadesSalvas)
    {
        unidadesDoPlayer = unidadesSalvas;
        
        foreach (Unidade unidade in unidadesSalvas)
        {
            GameObject unitFrame = Instantiate(framePrefab, transform);

            UnitFrameUi frame = unitFrame.GetComponent<UnitFrameUi>();

            frame.Inicializar(unidade);
            frames.Add(frame);
        }
    }
    public void AtualizarUi()
    {
        foreach (UnitFrameUi frame in frames)
        {
            frame.Atualizar();
        }
    }
}