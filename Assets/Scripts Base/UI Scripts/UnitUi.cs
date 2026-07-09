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
    public void AtualizarVida()
    {
        foreach (UnitFrameUi frame in frames)
        {
            frame.AtualizarVida(frame.unidadeAtual.vidaUnidade.vidaAtual,frame.unidadeAtual.vidaUnidade.vidaMaxima );
        }
    }
    public void AtualizarMana()
    {
        foreach (UnitFrameUi frame in frames)
        {
            frame.AtualizarMana(frame.unidadeAtual.recursosUnidade.manaAtual, frame.unidadeAtual.recursosUnidade.manaMaxima);
        }
        
    }
}