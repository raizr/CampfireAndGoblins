using System.Collections;
using System.Collections.Generic;
using TMPro;
using uClicker;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

	public ClickerManager clickerManager;
	public Button bossButton;
	public Slider progressBar;
	public Slider bossProgressBar;
	public TextMeshProUGUI comboCounter;

    void Start()
    {
		clickerManager.OnBossStageStart.AddListener(OnBossStageStart);
		clickerManager.OnBossLose.AddListener(OnBossLose);
		clickerManager.OnBossWin.AddListener(OnBossWin);
		bossButton.onClick.AddListener(BossButtonClick);
		clickerManager.OnTick.AddListener(OnTick);
		clickerManager.OnComboClick.AddListener(OnComboClick);
		progressBar.minValue = 0;
		bossProgressBar.minValue = 0;
		OnTick();
		bossButton.gameObject.SetActive(false);
	}

	private void OnComboClick(int count) {
		comboCounter.text = "x" + count;
	}

	private void OnBossWin() {
		bossButton.gameObject.SetActive(false);
		bossProgressBar.gameObject.SetActive(clickerManager.isBossStage);
	}

	private void OnBossLose() {
		bossButton.gameObject.SetActive(true);
		bossProgressBar.gameObject.SetActive(clickerManager.isBossStage);
	}

	private void OnBossStageStart() {
		bossButton.gameObject.SetActive(false);
		bossProgressBar.gameObject.SetActive(clickerManager.isBossStage);
	}

	private void BossButtonClick() {
		clickerManager.CallCurrentBossAgain();
		bossProgressBar.gameObject.SetActive(clickerManager.isBossStage);
	}

	private void OnTick() {
		var currentBoss = clickerManager.GetNextBoss();
		float currencyCurrent = 0.0f;
		clickerManager.State.CurrencyCurrentTotals.TryGetValue(currentBoss.CostToStart.Currency, out currencyCurrent);
		progressBar.maxValue = currentBoss.CostToStart.Amount;
		progressBar.value = currencyCurrent;

		bossProgressBar.value = clickerManager.State.currentBossCurrency;
		bossProgressBar.maxValue = currentBoss.CostToEnd.Amount;
	}
}
