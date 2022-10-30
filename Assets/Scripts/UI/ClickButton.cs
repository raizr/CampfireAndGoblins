using System.Collections;
using System.Collections.Generic;
using uClicker;
using UnityEngine;
using UnityEngine.UI;

public class ClickButton : MonoBehaviour
{

    public ClickerManager clickerManager;
	public Button button;
	public Clickable clickable;
	public int maxCounter = 5;
	private int clickCounter = 0;
	private IEnumerator gameLoop;
    // Start is called before the first frame update
    void Start() {
		button = GetComponent<Button>();
		button.onClick.AddListener(OnClick);
		gameLoop = StartGame();
		StartCoroutine(gameLoop);
		clickerManager.OnEnable();
		clickCounter = 0;
	}

	void OnClick() {
		clickCounter++;
		if (clickCounter > maxCounter) {
			clickCounter = maxCounter;
		}
		if (clickCounter > 0) {
			clickerManager.OnComboClick.Invoke(clickCounter);
		}
		clickerManager.Click(clickable);
	}

	IEnumerator StartGame()
	{
		clickerManager.LoadProgress();
		while (Application.isPlaying)
		{
			yield return new WaitForSecondsRealtime(1);
			clickCounter = 0;
			clickerManager.OnComboClick.Invoke(clickCounter);
			clickerManager.Tick();
			clickerManager.SaveProgress();
			PlayerPrefs.Save();
		}
	}

	void OnGUI()
	{
		if (GUI.Button(new Rect(10, 70, 50, 30), "Reset")) {
			StopCoroutine(gameLoop);
			clickerManager.ClearProgress();
			clickerManager.ResetState();
			clickerManager.OnEnable();
			StartCoroutine(gameLoop);
		}
	}
}
