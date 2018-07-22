using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
	public static GameController instance;
	public ResearchBuilding colonyResearchBuilding;
	public ResearchBuilding metropolyResearchBuilding;
	private ResearchData researchData;

	private float ResearchTime = 60;

	private float colonyCurrentTime = 0;
	private float metropolyCurrentTime = 0;

	[SerializeField]
	private float colonyCurrentAttackTime = 0;
	[SerializeField]
	private float metropolyCurrentAttackTime = 0;
	[SerializeField]
	private float ColonyAttackTime = 2;
	[SerializeField]
	private float MetropolyAttackTime = 2;
	private bool colonyUnderAttack = false;
	private bool metropolyUnderAttack = false;

	public List<Building> colonyAttackList;
	public List<Building> metropolyAttackList;
    
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
		UpdateColonyTime();
		UpdateColonyAttack();
		UpdateMetropolyTime();
		UpdateMetropolyAttack();
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
    public void OnResearchCompleted(Research research)
    {

    }

	void UpdateColonyAttack() {
		if(!colonyUnderAttack && colonyAttackList.Count > 0) {
			colonyCurrentAttackTime += Time.deltaTime;
			if(colonyCurrentAttackTime >= ColonyAttackTime) {
				int rand = Random.Range(0, colonyAttackList.Count);
				Building colonyBuilding = colonyAttackList[rand];
				colonyBuilding.SetUnderAttack(OnColonyAttackFinished);
				colonyUnderAttack = true;
			}
        }
	}
	void OnColonyAttackFinished() {
		colonyUnderAttack = false;
		colonyCurrentAttackTime = 0;
	}

    void UpdateMetropolyAttack()
    {
		if (!metropolyUnderAttack && metropolyAttackList.Count > 0)
        {
			metropolyCurrentAttackTime += Time.deltaTime;
			if (metropolyCurrentAttackTime >= MetropolyAttackTime)
            {
				int rand = Random.Range(0, metropolyAttackList.Count);
                
				Building metropolyBuilding = metropolyAttackList[rand];
				metropolyBuilding.SetUnderAttack(OnMetropolyAttackFinished);
				metropolyUnderAttack = true;
            }
        }
	}
    void OnMetropolyAttackFinished()
    {
		metropolyUnderAttack = false;
		metropolyCurrentAttackTime = 0;
    }

}
