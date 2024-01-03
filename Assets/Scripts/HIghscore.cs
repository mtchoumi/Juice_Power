using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Highscore : MonoBehaviour
{
    private List<int> highscores = new List<int>();
    private List<string> highscoresnames = new List<string>();
    private int placement = 5;
    private string newname = "";
    [SerializeField] private GameObject[] Obj;
    // Start is called before the first frame update
    void Start()
    {
        Obj[10].gameObject.GetComponent<InputField>().ActivateInputField();
        for (int i = 0; i < 5; i++)
        {
            using (StreamReader sr = new StreamReader("Highscore.txt"))
            {
                for (int j = 0; j < i; j++)
                {
                    sr.ReadLine();
                }
                highscoresnames.Add(sr.ReadLine().Substring(0,3));
            }
        }
        for (int i = 0; i < 5; i++)
        {
            using (StreamReader sr = new StreamReader("Highscore.txt"))
            {
                for (int j = 0; j < i; j++)
                {
                    sr.ReadLine();
                }
                highscores.Add(int.Parse(sr.ReadLine().Substring(3)));
            }
        }
        for (int i = highscores.Count - 1; i >= 0;i--)
        {
            if (Brain.round > highscores[i])
            {
                placement = i;
            }
        }
        highscores.Insert(placement, Mathf.RoundToInt(Brain.round));
        highscoresnames.Insert(placement, "AAA");
        highscores.RemoveAt(5);
        highscoresnames.RemoveAt(5);
    }

    // Update is called once per frame
    void Update()
    {
        using (StreamWriter sw = new StreamWriter("Highscore.txt"))
        {
            for (int i = 0; i < highscores.Count; i++)
            {
                sw.WriteLine(highscoresnames[i] + " " + highscores[i]);
            }
        }
        if (placement < highscores.Count)
        {
            highscoresnames[placement] = Obj[10].gameObject.GetComponent<InputField>().text.ToUpper();
        }
        for (int i = 0; i < highscores.Count; i++)
        {
            Obj[i].gameObject.GetComponent<Text>().text = highscoresnames[i];
            Obj[i + 5].gameObject.GetComponent<Text>().text = "" + highscores[i];
        }
    }

    public void Menu()
    {
        SceneManager.LoadScene("Start");
    }
}
