using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfoPainelController : MonoBehaviour
{
    [SerializeField] private GameObject frameDeInfo;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject pontoDeSpawn;
    [SerializeField] private List<UnitInfoFrame> frames = new();





    public void Incializar()
    {
        background.SetActive(true);
        foreach (Unidade unidade in TurnManager.Instance.unidadesPlayer)
        {
            GameObject novoFrame = Instantiate(frameDeInfo, pontoDeSpawn.transform);
            UnitInfoFrame unitInfoFrame = novoFrame.GetComponent<UnitInfoFrame>();
            if (unitInfoFrame != null) { 
                unitInfoFrame.SetInfo(unidade);
                frames.Add(unitInfoFrame);
            }
            
           
        }
    }

    public void Limpar()
    {
        foreach(UnitInfoFrame info in frames)
        {
            Destroy(info.gameObject);
        }

        frames.Clear();
        background.SetActive(false);
    }


}
