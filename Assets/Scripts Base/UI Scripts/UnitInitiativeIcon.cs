using UnityEngine;
using UnityEngine.UI;

public class UnitInitiativeIcon : MonoBehaviour
{
    public Unidade Unidade { get; private set; }
    [SerializeField] private Image unitIconImage;
    [SerializeField] private UnitInitiativeUI unitInitiativeUI;
   

    public void Awake()
    {
        unitInitiativeUI = GetComponentInParent<UnitInitiativeUI>();
        
    }


    public void SetUnitData(Unidade unidade)
    {
        if (unidade != null && unidade.unitData != null)
        {
            unitIconImage.sprite = unidade.unitData.icone;
            Unidade = unidade;
            Unidade.OnMorreu += RemoverIcone;

            RemoverDestaque();


        }
        else
        {
            Debug.LogWarning("Unidade ou unitData nulo");
        }
    }

    public void RemoverIcone(Unidade unidade)
    {
        unitInitiativeUI.icones.Remove(this);
        Destroy(gameObject);
    }

    public void Destacar()
    {
        Color transparencia = unitIconImage.color;
        transparencia.a = 1f;

        unitIconImage.color = transparencia;
    }
    public void RemoverDestaque()
    {
        Color transparencia = unitIconImage.color;
        transparencia.a = 0.2f;

        unitIconImage.color = transparencia;
    }

}