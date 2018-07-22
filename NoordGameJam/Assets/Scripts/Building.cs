using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public abstract class Building : MonoBehaviour {

    public enum BuildingTypes
    {
        Madeira = 0,
        Documentação,
        Açucar,
        Ouro,
        Armas,
        Tecnologia,
        Galpão,
        Research
    }

    public enum BuildingState
    {
        Idle = 0,
        UnderAttack,
        SavingFromAttack,
        Loading,
        Generating,//Generating the resource
        Ready// Ready to be collected
    }

    public BuildingTypes Type;
    public List<Resource> ResourceList;
    public BuildingState State = BuildingState.Idle;

    public float collectRate = 0.5F;
    protected float nextCollect = 0.0F;

    public float attackRate = 0.5F;
    protected float nextAttack = 0.0F;

	public float currentCollect = 0.0f;
	public float startCollect = 0.0f;
	public bool interacting = false;

	public float attackCurrentTime = 0.0f;
	public float attackSaveTime = 1.5f;
	public delegate void AttackFinished();
	public AttackFinished onAttackFinished;

    public Image AttackIcon;

	public Action<bool> attackFinishedCallbacks;

	public bool isUnderAttack
	{
		get
		{
			return (State == BuildingState.UnderAttack || State == BuildingState.SavingFromAttack);
		}
		private set
		{

		}
	}
    
	// Use this for initialization
	public void Start () {
        HideAttackIcon();
	}
	
	// Update is called once per frame
	public void FixedUpdate () {      
        
	}

	abstract public void Interact(Player player);
	abstract public void WhileInteracting(Player player);
	abstract public void EndInteraction(Player player);

	public void SetUnderAttack(AttackFinished attackFinished) {
		State = BuildingState.UnderAttack;
        ShowAttackIcon();
		//GetComponent<SpriteRenderer>().color = Color.red;
		attackCurrentTime = 0;
		onAttackFinished = attackFinished;
	}

	public void UpdateAttackState() {
		if(interacting) {
			if (State == BuildingState.UnderAttack)
			{
				State = BuildingState.SavingFromAttack;
				attackCurrentTime += Time.deltaTime;
				GetComponent<SpriteRenderer>().color = Color.grey;
				TryRemoveAttack();
			} else if (State == BuildingState.SavingFromAttack) {
                attackCurrentTime += Time.deltaTime;
				TryRemoveAttack();
			}
		} else if(State == BuildingState.SavingFromAttack) {
			State = BuildingState.UnderAttack;
            ShowAttackIcon();
			GetComponent<SpriteRenderer>().color = Color.red;
		}
    }
	public void TryRemoveAttack() {
		if(attackCurrentTime >= attackSaveTime) {
            State = BuildingState.Idle;
            HideAttackIcon();
			GetComponent<SpriteRenderer>().color = Color.white;
			onAttackFinished();
			if(attackFinishedCallbacks != null) {
				attackFinishedCallbacks(true);
			}
		}
	}
	public void RegisterOnAttackFinished(Action<bool> action) {
		attackFinishedCallbacks += action;
	}
	public void RemoveOnAttackFinished(Action<bool> action) {
		attackFinishedCallbacks -= action;
	}

    public void ShowAttackIcon()
    {
        if (AttackIcon != null)
        {
            var tempColor = AttackIcon.color;
            tempColor.a = 1f;
            AttackIcon.color = tempColor;
        }
    }

    public void HideAttackIcon()
    {
        if (AttackIcon != null)
        {
            var tempColor = AttackIcon.color;
            tempColor.a = 0f;
            AttackIcon.color = tempColor;
        }
        else
            print("QQ HOUVE??");
    }

}
