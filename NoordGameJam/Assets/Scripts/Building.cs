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
	public float attackSaveTime = 6f;
	public delegate void AttackFinished();
	public AttackFinished onAttackFinished;

	public GameObject attackIcon;
	public TimeBarAttack attackBar;

	private BuildingState lastState;

    public AudioClip myAudioClip;
    public AudioSource myAudioSource;
    public AudioClip myAtkAudioClip;

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
		hideAttackBar();

	}
	
	// Update is called once per frame
	public void FixedUpdate () {      
        
	}

	abstract public void Interact(Player player);
	abstract public void WhileInteracting(Player player);
	abstract public void EndInteraction(Player player);

	public void SetUnderAttack(AttackFinished attackFinished) {
		lastState = State;
		State = BuildingState.UnderAttack;
        ShowAttackIcon();
        if (myAudioSource != null)
        {
            myAudioSource.clip = myAtkAudioClip;
            myAudioSource.Play();
        }
		//GetComponent<SpriteRenderer>().color = Color.red;
		attackCurrentTime = 0;
		onAttackFinished = attackFinished;
	}

	public void UpdateAttackState() {
		if(interacting) {
			if (State == BuildingState.UnderAttack)
			{
                Player player = GameController.instance.GetMetropolyPlayer();
                player.myAudioSource.volume = 100;
				State = BuildingState.SavingFromAttack;
				attackBar.SetTimerBarAttack(attackSaveTime);
				showAttackBar();
				attackCurrentTime += Time.deltaTime;
				attackBar.UpdateBar();
				TryRemoveAttack();
			} else if (State == BuildingState.SavingFromAttack) {
                attackCurrentTime += Time.deltaTime;
				attackBar.UpdateBar();
				TryRemoveAttack();
			}
		} else if(State == BuildingState.SavingFromAttack) {
            GameController.instance.GetMetropolyPlayer().myAudioSource.volume = 0;
            State = BuildingState.UnderAttack;
            ShowAttackIcon();
		}
    }
	public void TryRemoveAttack() {
		if(attackCurrentTime >= attackSaveTime) {
            GameController.instance.GetMetropolyPlayer().myAudioSource.volume = 0;
            State = lastState;
            HideAttackIcon();
			onAttackFinished();
			hideAttackBar();
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
        if (attackIcon != null)
        {
			attackIcon.gameObject.SetActive(true);
        }
    }

    public void HideAttackIcon()
    {
        if (attackIcon != null)
        {
			attackIcon.gameObject.SetActive(false);
        }
		else {
			//print("QQ HOUVE??");
            
        }
    }
    
	public void hideAttackBar() {
		if(attackBar != null) {
			attackBar.gameObject.SetActive(false);
			attackBar.Restart();
        }
	}
	public void showAttackBar() {
		if(attackBar != null) {
			attackBar.gameObject.SetActive(true);
        }
	}
}
