using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Factions
{
    //public string factionName;

    float _approval;
    public float approval
    {
        set
        {
            _approval = Mathf.Clamp(value, -1, 1);
        }
        get
        {
            return _approval;
        }
    }
}

public class FactionManager : MonoBehaviour
{

    
   // [SerializeField]
    public Dictionary<string, Factions> factions;

    public static FactionManager instance;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        factions = new Dictionary<string, Factions>();
        factions.Add("Orange you glad", new Factions());
    }


    public float? FactionsApproval(string factionName, float value)
    {
        if (factions.ContainsKey(factionName))
        {
            factions[factionName].approval += value;
            return factions[factionName].approval;
        }
        return null;
    }
    public float? GetFactionsApproval(string factionName)
    {
        if (factions.ContainsKey(factionName))
        {
            
            return factions[factionName].approval;
        }
        return null;
    }
}
