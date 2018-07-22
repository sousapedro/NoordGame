using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class ResearchBuilding : Building
{
    public enum LandType
    {
        Colonia,
        Metropole
    }

    public LandType landType;

    public Text CurQuestRes1;
    public Text CurQuestRes2;
    public Text CurQuestRes3;

    public int[] curQuest = {0,0,0};

    public TimeBar curTimeBar;

	public Research research;
	public delegate void ResearchCompleted();
	private ResearchCompleted onResearchCompleted;
    // Use this for initialization
    public new void Start()
    {
		base.Start();

        ResourceList.Add(new Resource("Madeira"));
        ResourceList.Add(new Resource("Documentação"));
        ResourceList.Add(new Resource("Açucar"));
        ResourceList.Add(new Resource("Ouro"));
        ResourceList.Add(new Resource("Armas"));
        ResourceList.Add(new Resource("Tecnologia"));

        if (landType == LandType.Metropole)
        {
            Resource res1 = ResourceList.Find(p => p.name == "Madeira");
            res1.registerOnUpdateCb(changeResource1);

            Resource res2 = ResourceList.Find(p => p.name == "Documentação");
            res2.registerOnUpdateCb(changeResource2);

            Resource res3 = ResourceList.Find(p => p.name == "Açucar");
            res3.registerOnUpdateCb(changeResource3);
        }
        else
        {
            Resource res1 = ResourceList.Find(p => p.name == "Ouro");
            res1.registerOnUpdateCb(changeResource1);

            Resource res2 = ResourceList.Find(p => p.name == "Armas");
            res2.registerOnUpdateCb(changeResource2);

            Resource res3 = ResourceList.Find(p => p.name == "Tecnologia");
            res3.registerOnUpdateCb(changeResource3);
        }

        //print("adding resources");
        //foreach (Resource myRes in ResourceList)
        //{
        //    myRes.modifyResource(5);
        //}
    }
	private void Reset()
	{
        State = BuildingState.Idle;
		currentCollect = 0;
	}

	// Update is called once per frame
	public new void FixedUpdate()
    {
		ChangeState();
    }
	public void SetResearch(Research research, ResearchCompleted callback) {
		onResearchCompleted = callback;
		this.research = research;

        for (int i = 0; i < 3; i++)
        {
            curQuest[i] =  research.ResourceList[i].value;
        }

        curTimeBar.Restart();

		Reset();
	}

	override public void Interact(Player player)
    {
		interacting = true;
        ChangeState();
	}
    public override void WhileInteracting(Player player)
    {
        ChangeState();
    }   
	public override void EndInteraction(Player player) {
		interacting = false;
        ChangeState();
	}
    
	void ChangeState() {
		if(interacting) {
			if(CheckResources()) {
				UpdateResearch();
            }
		} else {
			StopResearch();
		}
	}
    void UpdateResearch()
	{
        if (State == BuildingState.Idle)
        {
            State = BuildingState.Loading;

			currentCollect += Time.deltaTime;

            GetComponent<SpriteRenderer>().color = Color.blue;
        }
        if (State == BuildingState.Loading)
        {
			currentCollect += Time.deltaTime;
			if(currentCollect >= collectRate) {
				EndResearch();
			}
        }
    }
	void StopResearch() {
        if (State == BuildingState.Loading)
        {
			print("research stoped");
			State = BuildingState.Idle;
			GetComponent<SpriteRenderer>().color = Color.white;
        }
	}
    
	void EndResearch() {
		State = BuildingState.Ready;
		GetComponent<SpriteRenderer>().color = Color.white;
        RemoveResources();
		onResearchCompleted();
	}

	bool CheckResources() {
		bool ret = true;
		foreach (Resource otherRes in research.ResourceList)
        {
			foreach (Resource myRes in ResourceList)
            {
				if(myRes.name == otherRes.name) {
					ret = ret && myRes.value >= otherRes.value;
					if(!ret) {
						break;
					}
				}
            }
			if(!ret) {
				break;
			}
        }
		return ret;
	}

	void RemoveResources() {

        foreach (Resource otherRes in research.ResourceList)
        {
            foreach (Resource myRes in ResourceList)
            {
                if (myRes.name == otherRes.name)
                {
					myRes.modifyResource(-otherRes.value);
                }
            }
        }
	}

    public void changeResource1(Resource res)
    {
        CurQuestRes1.text = res.value + "/" + curQuest[0];
    }

    public void changeResource2(Resource res)
    {
        CurQuestRes2.text = res.value + "/" + curQuest[1];
    }

    public void changeResource3(Resource res)
    {
        CurQuestRes3.text = res.value + "/" + curQuest[2];
    }
}
