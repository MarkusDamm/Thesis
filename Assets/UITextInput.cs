using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UITextInput : MonoBehaviour
{
    public TextMeshProUGUI textMesh;

    // Start is called before the first frame update
    void Start()
    {
        textMesh.SetText("");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void writeText(string _letter){
        textMesh.SetText(textMesh.text + _letter);
    }
}
