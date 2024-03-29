using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class KillTracker : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI killsText;
    private int kills=0;

    // Start is called before the first frame update
    void Start()
    {
        killsText.text = kills.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addKill()
    {
        if (PlayerData.curHealth>0)
        {
            kills++;
            killsText.text = kills.ToString();

            if (kills>control.killScore)
            {
                control.killScore=kills;
            }
        }
        
    }
    public int getKills()
    {
        return kills;
    }
}
