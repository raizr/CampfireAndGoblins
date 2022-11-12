using DG.Tweening;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using uClicker;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Events;

public class ComboLabel : MonoBehaviour {
	
	public FlyingCounterPool pool;
	public float showY = 100.0f;
	public float showDuration = 0.3f;
	public float hideDuration = 0.1f;
	public UnityAction onShowed;
	public UnityAction onHided;
	private float defaultY = 0.0f;
	private TextMeshProUGUI text;
	private RectTransform rect;
	private bool showed = false;

	public void Awake() {
		text = GetComponent<TextMeshProUGUI>();
		rect = GetComponent<RectTransform>();
		defaultY = rect.anchoredPosition.y;
	}

	public static TweenerCore<Vector2, Vector2, VectorOptions> DOAnchorPosY(RectTransform target, float endValue, float duration, bool snapping = false)
	{
		TweenerCore<Vector2, Vector2, VectorOptions> t = DOTween.To(() => target.anchoredPosition, x => target.anchoredPosition = x, new Vector2(0, endValue), duration);
		t.SetOptions(AxisConstraint.Y, snapping).SetTarget(target);
		return t;
	}

	public void Show(float count) {
		if (showed) {
			return;
		}
		text.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		text.text = "+" + count;
		showed = true;
		DOAnchorPosY(rect, showY, showDuration)
			.SetEase(Ease.OutQuad)
			.OnComplete(ResetY);
		DOTween.To(() => text.color.a,
			x => text.color = new Color(text.color.r, text.color.g, text.color.b, x),
			0.0f,
			showDuration);
	}

	public void ResetY() {
		rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, defaultY);
		text.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		showed = false;
		pool.ReturnToPool(this);
	}

	public bool IsShowed() {
		return showed;
	}
}

