using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitInfoFrame : MonoBehaviour
{
    [SerializeField] private Image icone;
    [SerializeField] private TextMeshProUGUI nome;
    [SerializeField] private TextMeshProUGUI vidaMaxima;
    [SerializeField] private TextMeshProUGUI mana;
    [SerializeField] private TextMeshProUGUI ataque;
    [SerializeField] private TextMeshProUGUI defesa;
    [SerializeField] private TextMeshProUGUI agilidade;
    [SerializeField] private TextMeshProUGUI movimento;


    public void SetInfo(Unidade unidade)
    {
        icone.sprite = unidade.unitData.icone;
        nome.text = unidade.unitData.nome;
        vidaMaxima.text = "Vida: " + unidade.GetVidaMaximaAtual().ToString();
        mana.text = "Mana: " + unidade.GetVidaMaximaAtual().ToString();
        ataque.text = "Ataque: " + unidade.GetAtaqueAtual().ToString();
        defesa.text = "Defesa: " + unidade.GetDefesaAtual().ToString();
        agilidade.text = "Agilidade: " + unidade.GetAtaqueAtual().ToString();
        movimento.text ="Movimento: " + unidade.GetMovimentoAtual().ToString();



    }
}



