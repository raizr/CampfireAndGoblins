using System.Collections;
using System.Collections.Generic;
using TMPro;
using uClicker;
using UnityEngine;

public class CurrentLevel : MonoBehaviour
{
	public ClickerManager clickerManager;
	public TextMeshProUGUI text;

    void Start()
    {
		text = GetComponent<TextMeshProUGUI>();
		clickerManager.OnLevelChanged.AddListener(OnLevelChanged);
		OnLevelChanged();
	}
    
    void OnLevelChanged()
    {
		text.text = (clickerManager.State.currentLevel + 1).ToString();
    }
}
