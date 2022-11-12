using System.Collections;
using System.Collections.Generic;
using uClicker;
using UnityEngine;

public class FlyingCounterPool : MonoBehaviour {
	
	public ClickerManager clickerManager;
	public uint initPoolSize = 10;
	public ComboLabel poolObject;
	private Stack<ComboLabel> stack;
	public float leftX = -20;
	public float rightX = 20;
	private bool isLeft = false;
	// Start is called before the first frame update
    void Start()
	{
		clickerManager.OnComboClick.AddListener(Show);
		SetupPool();
	}

	private void SetupPool()
	{
		stack = new Stack<ComboLabel>();
		ComboLabel instance = null;
		for (int i = 0; i < initPoolSize; i++)
		{
			instance = Instantiate(poolObject, transform);
			instance.pool = this;
			instance.gameObject.SetActive(false);
			stack.Push(instance);
		}
	}

	public ComboLabel GetPooledObject()
	{
		if (stack.Count == 0)
		{
			ComboLabel newInstance = Instantiate(poolObject, transform);
			newInstance.pool = this;
			return newInstance;
		}
		ComboLabel nextInstance = stack.Pop();
		nextInstance.gameObject.SetActive(true);
		return nextInstance;
	}

	public void ReturnToPool(ComboLabel pooledObject)
	{
		stack.Push(pooledObject);
		pooledObject.gameObject.SetActive(false);
	}

	public void Show(float count)
	{
		var flyingCounter = GetPooledObject();
		var pos = flyingCounter.GetComponent<RectTransform>().anchoredPosition;
		if (isLeft) {
			isLeft = false;
			pos = new Vector2(leftX, pos.y);
		}
		else {
			isLeft = true;
			pos = new Vector2(rightX, pos.y);
		}
		flyingCounter.GetComponent<RectTransform>().anchoredPosition = pos;
		flyingCounter.Show(count);
	}
}
