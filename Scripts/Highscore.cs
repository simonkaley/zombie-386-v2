using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class highscore : MonoBehaviour
{

    [SerializeField]
    TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = control.killScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
