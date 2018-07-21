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
    public Text ResourceAmount1;
    public Text ResourceAmount2;
    public Text ResourceAmount3;

    public Text CurQuestRes1;
    public Text CurQuestRes2;
    public Text CurQuestRes3;

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
        
        //if (Input.GetKeyDown(KeyCode.G))
        //{
        //    //foreach (Resource res in ResourceList)
        //    //{
        //    //    print(res.name + ":" + res.value);
        //    //}
        //    if (landType == LandType.Colonia)
        //        print(research.ResourceList[0].name);

        //}
    }
	public void SetResearch(Research research, ResearchCompleted callback) {
		onResearchCompleted = callback;
		this.research = research;

        CurQuestRes1.text = "x" + research.ResourceList[0].value;
        CurQuestRes2.text = "x" + research.ResourceList[1].value;
        CurQuestRes3.text = "x" + research.ResourceList[2].value;

		Reset();
	}
    //public void SetCurrentQuestDisplay(Research research)
    //{

    //}
	override public void Interact(Player player)
    {
		interacting = true;
	}
    public override void WhileInteracting(Player player)
    {
		
    }   
	public override void EndInteraction(Player player) {
		interacting = false;
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
			print("research  started");
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
			print("research  stoped");
			State = BuildingState.Idle;
			GetComponent<SpriteRenderer>().color = Color.white;
        }
	}
    
	void EndResearch() {
		State = BuildingState.Ready;
		GetComponent<SpriteRenderer>().color = Color.white;
		print("research  ended");
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
        ResourceAmount1.text = "x" + res.value;
    }

    public void changeResource2(Resource res)
    {
        ResourceAmount2.text = "x" + res.value;
    }

    public void changeResource3(Resource res)
    {
        ResourceAmount3.text = "x" + res.value;
    }
}
