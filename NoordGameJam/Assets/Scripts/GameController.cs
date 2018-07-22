using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	public static GameController instance;
	public ResearchBuilding colonyResearchBuilding;
	public ResearchBuilding metropolyResearchBuilding;

	public GameProgressBar progressBar;

	private ResearchData researchData;

	private float ResearchTime = 10;

	private float colonyCurrentTime = 0;
	private float metropolyCurrentTime = 0;

	private float colonyCurrentAttackTime = 0;
	private float metropolyCurrentAttackTime = 0;
	public float ColonyAttackTime = 2;
	public float MetropolyAttackTime = 2;
	private bool colonyUnderAttack = false;
	private bool metropolyUnderAttack = false;

	public List<Building> colonyAttackList;
	public List<Building> metropolyAttackList;

	private int lifeNumber = 3;
	private int lifesLost = 0;
	private bool hasEnded = false;

	private int researchGoals = 0;
	private int researchsCompleted = 0;
    
	private GameController() {
		GameController.instance = this;
	}
    // Use this for initialization
    void Start()
    {
		researchData = new ResearchData();

		SetColonyResearch(researchData.GetNextColonyResearch());
		SetMetropolyResearch(researchData.GetNextMetropolyResearch());
		progressBar.SetStep(researchData.getSize()/2);
		researchGoals = researchData.getSize() / 2;
    }
	public float GetResearchTime() {
		return ResearchTime;
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
		Research research = metropolyResearchBuilding.research;
		SetMetropolyResearch(researchData.GetNextMetropolyResearch());
		OnResearchCompleted(research);
	}
    public void OnColonyResearchCompleted()
    {
		Research research = colonyResearchBuilding.research;
		SetColonyResearch(researchData.GetNextColonyResearch());
		OnResearchCompleted(research);
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
			endGame(true);
		} else {
			colonyResearchBuilding.SetResearch(research, OnColonyResearchCompleted);
        }
	}
	void SetMetropolyResearch(Research research) {
		metropolyCurrentTime = 0;
		if (research == null)
		{
			endGame(true);
		}
		else
		{
			metropolyResearchBuilding.SetResearch(research, OnMetropolyResearchCompleted);
		}
	}
	void penality() {
		progressBar.loseLife();
		lifesLost++;
		if(lifesLost >= lifeNumber) {
			endGame(false);
		}
	}
	void endGame(bool success) {
		if(hasEnded) {
			return;
		}
        
		hasEnded = true;
		if(success) {
			print("end game");
			SceneManager.LoadScene("EndGame");
        } else {
			print("game over");
			SceneManager.LoadScene("GameOver");
		}
		print("game ended!");
	}
    public void OnResearchCompleted(Research research)
    {
		progressBar.nextProgressStep();
		researchsCompleted++;
		if (researchsCompleted >= researchGoals) {
			endGame(true);
		}
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
