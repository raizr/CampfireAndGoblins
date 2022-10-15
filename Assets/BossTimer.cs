using System.Collections;
using System.Collections.Generic;
using TMPro;
using uClicker;
using UnityEngine;

public class BossTimer : MonoBehaviour
{
	public ClickerManager clickerManager;
	public TextMeshProUGUI text;

	void Awake()
    {
		text = GetComponent<TextMeshProUGUI>();
		clickerManager.OnBossStageStart.AddListener(OnStartStage);
		clickerManager.OnBossStageTimeChanged.AddListener(OnBossStageTimeChanged);
		clickerManager.OnBossWin.AddListener(OnStopStage);
		clickerManager.OnBossLose.AddListener(OnStopStage);
    }

	void OnStartStage() {
		text.gameObject.SetActive(true);
	}

	void OnStopStage() {
		text.gameObject.SetActive(false);
		text.text = "";
	}

	void OnBossStageTimeChanged() {
		text.text = "Time left: " + clickerManager.currentBossSeconds;
	}
}
