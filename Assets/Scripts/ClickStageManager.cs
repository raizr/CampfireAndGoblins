using System;
using System.Collections;
using System.Collections.Generic;
using uClicker;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public struct ClickStage {
	public int counter;
	public int time;
}

public class ClickStageManager : MonoBehaviour 
{
	public ClickerManager clickerManager;
	public ClickStage[] stages = new ClickStage[5];
	[ReadOnly]
	public ClickStage changeable;
	public UnityEvent<int> OnIncrementStage = new UnityEvent<int>();
	public UnityEvent<int> OnDecrementStage = new UnityEvent<int>();
    private int currentStage = 0;

    void Start() {
		changeable = stages[currentStage];
		StartCoroutine(SecondTick());
		clickerManager.OnClick.AddListener(OnClick);
	}
	
	public void OnClick() {
		changeable.counter--;
		changeable.time = stages[currentStage].time;
		if (changeable.counter <= 0) {
			changeable.counter = 0;
			if (currentStage < stages.Length - 1) {
				IncrementStage();
			}
		}
	}

	IEnumerator SecondTick() {
		while (true) {
			yield return new WaitForSecondsRealtime(1);
			if (currentStage > 0) {
				changeable.time--;
				changeable.counter = stages[currentStage].counter;
				if (changeable.time <= 0
					&& currentStage > 0) {
					DecrementStage();
				}
			}
		}
	}
	
	public void IncrementStage() {
		currentStage++;
		if (currentStage >= stages.Length - 1) {
			currentStage = stages.Length - 1;
		}
		changeable = stages[currentStage];
		OnIncrementStage.Invoke(currentStage);
	}

	public void DecrementStage() {
		currentStage--;
		if (currentStage < 0) {
			currentStage = 0;
		}
		changeable = stages[currentStage];
		OnDecrementStage.Invoke(currentStage);
	}

	public ClickStage getCurrentStage() {
		return stages[currentStage];
	}
}
