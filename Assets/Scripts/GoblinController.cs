using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class GoblinController : MonoBehaviour
{
	public ClickStageManager stageManager;
	[SpineAnimation]
	public string[] animationName;
	private SkeletonAnimation skeleton;
	private int currentAnimation = 0;

    void Awake() {
		skeleton = GetComponent<SkeletonAnimation>();
		stageManager.OnIncrementStage.AddListener(OnIncrementStage);
		stageManager.OnDecrementStage.AddListener(OnIncrementStage);
	}


	void StartPlayCurrentStage() {
		skeleton.AnimationState.SetAnimation(0, animationName[currentAnimation], true);
	}

	void OnIncrementStage(int currentStage) {
		currentAnimation = Math.Max(0, currentStage - 1);
		StartPlayCurrentStage();
	}
}
