using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static Unity.Burst.Intrinsics.X86.Avx;

public class TypeWriterEffect : MonoBehaviour
{
    public float textSpeed = 1f;
    public string script;
    public List<String> lines;
    public string text;
    public string currentLine;
    public TextMeshProUGUI textMeshProUGUI;
    public TextMeshProUGUI speaker;
    public int index = 0;
    public int letterIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        string filepath = Path.Combine(Application.dataPath, "BeginningDialogue.txt");
        if (File.Exists(filepath))
        {
            script = File.ReadAllText(filepath);
            lines = new List<string>(script.Split('\n'));
            
            //textMeshProUGUI.text = lines[0];
            StartCoroutine(PrintText());
        }
        else
        {
            Debug.LogError("File not found: " + filepath);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") )
        {
            SceneManager.LoadScene("Overworld");

        }
    }
   
    IEnumerator PrintText()
    {
        
        while(index < lines.Count)
        {
            if (lines[index].Substring(0,1).Equals("B"))
            {
                speaker.text = "Boss: ";
            }
            else if (lines[index].Substring(0,1).Equals("D"))
            {
                speaker.text = "Dr.Whiskers: "; 
            }

            for(int i = 2; i < lines[index].Length; i++) 
            {
                text = lines[index].Substring(1, i);
                textMeshProUGUI.text = text;
                yield return new WaitForSeconds(textSpeed);

            }
            index++;
            yield return new WaitForSeconds(1f);
        }
        SceneManager.LoadScene("Overworld");


    }
}
