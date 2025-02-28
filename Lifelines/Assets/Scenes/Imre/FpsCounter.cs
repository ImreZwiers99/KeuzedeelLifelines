using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FpsCounter : MonoBehaviour
{

    public float timer, refresh, avgFramerate;
    public string display = "{0}FPS";
    public Text m_text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float timeLapse = Time.smoothDeltaTime;
        timer = timer <= 0 ? refresh : timer -= timeLapse;

        if(timer <= 0) avgFramerate = (int) (1f / timeLapse);
        m_text.text = string.Format(display,avgFramerate.ToString());
    }
}
