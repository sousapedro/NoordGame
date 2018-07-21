using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	public static GameController instance;
	public ResearchBuilding colonyResearchBuilding;
	public ResearchBuilding metropolyResearchBuilding;
	private ResearchData researchData;

	private float ResearchTime = 60;

	private float colonyCurrentTime = 0;
	private float metropolyCurrentTime = 0;
    
	private GameController() {
		GameController.instance = this;
	}
    // Use this for initialization
    void Start()
    {
		researchData = new ResearchData();


		SetColonyResearch(researchData.GetNextColonyResearch());
		SetMetropolyResearch(researchData.GetNextMetropolyResearch());
    }

    // Update is called once per frame
    void Update()
    {
		//print(colonyCurrentTime);
		UpdateColonyTime();
		UpdateMetropolyTime();
    }
	public void OnResearchCompleted(Research research) {
		
	}
	public void OnMetropolyResearchCompleted() {
		SetMetropolyResearch(researchData.GetNextMetropolyResearch());
	}
    public void OnColonyResearchCompleted()
    {
		SetColonyResearch(researchData.GetNextColonyResearch());
    }
	void UpdateColonyTime() {
		colonyCurrentTime += Time.deltaTime;
		if(colonyCurrentTime > ResearchTime) {
			penality();
			SetColonyResearch(researchData.GetNextColonyResearch());
		}
	}
    void UpdateMetropolyTime()
    {
		metropolyCurrentTime += Time.deltaTime;
		if (metropolyCurrentTime > ResearchTime)
        {
            penality();
			SetMetropolyResearch(researchData.GetNextMetropolyResearch());
        }
    }

	void SetColonyResearch(Research research) {
		colonyCurrentTime = 0;
		if(research == null) {
			endGame();
		} else {
			colonyResearchBuilding.SetResearch(research, OnColonyResearchCompleted);
        }
	}
	void SetMetropolyResearch(Research research) {
		metropolyCurrentTime = 0;
		if (research == null)
		{
			endGame();
		}
		else
		{
			metropolyResearchBuilding.SetResearch(research, OnMetropolyResearchCompleted);
		}
	}
	void penality() {
		
	}
	void endGame() {
		
	}
}
