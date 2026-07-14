using System;
using System.Collections.Generic;
using UnityEngine;


public class ConditionVisual : MonoBehaviour
{
    private Dictionary<ConditionData, GameObject> visuaisAtivos = new();

    public void AdicionarVisual(ConditionData data)
    {
        if(data.prefabVisual == null)return;
        if(visuaisAtivos.ContainsKey(data)) return;

        GameObject visual = Instantiate(data.prefabVisual, transform);

        visuaisAtivos.Add(data, visual);
        Debug.Log("aplicando visual");
      
    }

    public void RemoverVisual(ConditionData data)
    {
        if(!visuaisAtivos.TryGetValue(data, out GameObject visual)) return;
        Destroy(visual);
        visuaisAtivos.Remove(data);

    }


    


    

}