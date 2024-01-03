using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Brain : MonoBehaviour
{
	public enum WaveStates {spawning, counting, paused};
	public static WaveStates wstates = WaveStates.counting;
	[SerializeField] private GameObject[] enes;
	[SerializeField] private GameObject[] UIElements;
	public static float juice = 45f;
	public static float basehp = 30f;
	[SerializeField] private float[] percent;
	private float[] intpercent;
	public static float round = 0;
	private float enVal = 1;
	private int numEn = 2;
	public static float numD = 1f;
	private float time = 7f;
	public static int intp = 1;
	//[SerializeField] private Text[] txts;
	public static Dictionary <string, float[]> Towers = new Dictionary<string, float[]>();
    // Start is called before the first frame update
    void Start()
    {
		Towers.Clear();
		Towers.Add("Sgt. Citrus", new float[] {20,5,100,1.5f});
		Towers.Add("Ananasus", new float[] {35,10,175,1.25f});
		Towers.Add("Aristcado", new float[] {30,7,95,2f});
		Towers.Add("Juicerator", new float[] {50,20,200,1.5f});
		Towers.Add("Sprinkler", new float[] {45,17,250,2f});
		Towers.Add("Fridge", new float[] {40,15,225,2f});
		Towers.Add("BombTower", new float[] {20,13,125,1.5f});
		intpercent = new float[percent.Length];
		juice = 45f;
		Juice.maxjuice = 150f;
		basehp = 30f;
		numEn = 2;
		round = 0;
		time = 7f;
		enVal = 1;
		intp = 1;
		numD = 1f;
		wstates = WaveStates.counting;
		UIElements[2].gameObject.GetComponent<Button>().onClick.AddListener( delegate {pauseBut();});
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		UIElements[0].gameObject.GetComponent<Text>().text = System.Math.Round(juice,2) + "/" + System.Math.Round(Juice.maxjuice,2);
		UIElements[1].gameObject.GetComponent<Text>().text = "Wave: " + round + " Next Wave In: " + (int) time + " Health: " + basehp;
		if (wstates == WaveStates.paused)
		{
			UIElements[3].gameObject.GetComponent<Text>().text = "Play";
		}
		else
		{
			UIElements[3].gameObject.GetComponent<Text>().text = "Paused";
		}
		if (wstates == WaveStates.counting)
		{
			time -= Time.deltaTime;
			if (basehp > 0 && time <= 0)
			{
				juice -= Mathf.Min(juice, Mathf.Min(round * 0.5f, 50));
				StartCoroutine(spawnWave());
				time = 7f;
			}
		}
		if (basehp <= 0)
		{
			SceneManager.LoadScene("End");
		}
    }
	IEnumerator spawnWave()
	{
		wstates = WaveStates.spawning;
		for (int i = 0; i < numEn;)
		{
			for (int j = 0; j < percent.Length; j++)
			{
				if ((int) (Random.Range(1,Mathf.Round((100/percent[j]) + 1) * Mathf.Min(percent[j],1))) == 1 && i < numEn)
				{
					GameObject Enemy = Instantiate(enes[j], new Vector3(-10f, -2f, 0f), new Quaternion(0f, 0f, 0f, 1));
					Enemy.name = enes[j].name + i;
					i++;
				}
			}
			yield return new WaitForSeconds(1f/1f);
		}
		waveComp();
		yield break;
	}
	
	void waveComp()
	{
		round++;
		if(round % 5 == 0)
		{
			enVal++;
			for (int i = 0; i < intpercent.Length; i++)
			{
				intpercent[i] = percent[i];
			}
		}
		for (int i = 0; i < percent.Length; i++)
		{
			if (percent[i] > 100f/enVal && i < enVal)
			{
				percent[i] -= (intpercent[i] - (100f/enVal))/5f;
			}
			else if (percent[i] < 100f/enVal && i < enVal)
			{
				percent[i] += ((100f/enVal) - intpercent[i])/5f;
			}
		}
		wstates = WaveStates.counting;
		numEn += 2;
	}

	public void pauseBut()
	{
		if(wstates == WaveStates.paused)
		{
			wstates = WaveStates.counting;
			intp = 1;
			Time.timeScale = 1.0f;
		}
		else
		{
			wstates = WaveStates.paused;
			intp = 0;
			Time.timeScale = 0f;
		
		}
	}
}
