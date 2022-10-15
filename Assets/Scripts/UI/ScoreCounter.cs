using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using uClicker;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
	public ClickerManager clickerManager;

	public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start() {
		text = GetComponent<TextMeshProUGUI>();
        clickerManager.OnTick.AddListener(OnTick);
		OnTick();
	}

	private void OnTick() {
		text.text =  clickerManager.State.CurrencyCurrentTotals.Select((kvp) => $"{kvp.Value}").ToArray()[0];
	}

	// Update is called once per frame
    void Update()
    {
        
    }
}
