using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class FireController : MonoBehaviour {
	
	public ClickStageManager stageManager;
	public ParticleSystem[] particles;

	private int currentParticle = 0;

	void Start() {
		stageManager.OnIncrementStage.AddListener(OnIncrementStage);
		stageManager.OnDecrementStage.AddListener(OnIncrementStage);
		StartPlayCurrentStage();
	}

	void StopPlayAllStagesExceptCurrent() {
		for (int i = 0; i < particles.Length; i++) {
			if (i != currentParticle) {
				particles[i].Stop();
			}
		}
	}

	void StartPlayCurrentStage() {
		StopPlayAllStagesExceptCurrent();
		particles[currentParticle].Play();
	}

	void OnIncrementStage(int currentStage) {
		currentParticle = currentStage;
		StartPlayCurrentStage();
	}
}
