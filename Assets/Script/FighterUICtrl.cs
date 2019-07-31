using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FighterUICtrl : MonoBehaviour {
    static private Canvas canvas;
    static private RectTransform canvasTransform;
    static private GameObject prefab;

    static private int count = 0;

    private Fighter fighter;

    private GameObject healthBar;
    private RectTransform healthBarTransform;
    private RectTransform healthBarFill;

    void Awake() {
        if (canvas == null) {
            canvas = FindObjectOfType<Canvas>();
            canvasTransform = canvas.GetComponent<RectTransform>();
        }
        if (prefab == null) { prefab = Resources.Load<GameObject>("HealthBar"); }

        healthBar = Instantiate(prefab, canvasTransform);
        healthBarTransform = healthBar.GetComponent<RectTransform>();
        healthBarFill = healthBarTransform.GetChild(0).GetComponent<RectTransform>();
        healthBarTransform.GetComponentInChildren<Text>().text = gameObject.name;

        float _y = -10;
        if (count > 0) { _y -= 40 * count; }
        healthBarTransform.anchoredPosition = new Vector2(10, _y);
        healthBarTransform.sizeDelta = new Vector2(canvasTransform.sizeDelta.x - 20, healthBarTransform.sizeDelta.y);
        healthBarFill.anchoredPosition = Vector2.zero;

        count++;

        fighter = GetComponent<Fighter>();
    }

    void Start() {
        UpdateHealth();
    }

    private void LogRectTransform(RectTransform _t) {
        Debug.Log(_t.anchoredPosition);
        Debug.Log(_t.anchorMax);
        Debug.Log(_t.anchorMin);
        Debug.Log(_t.offsetMax);
        Debug.Log(_t.offsetMin);
        Debug.Log(_t.pivot);
        Debug.Log(_t.rect);
        Debug.Log(_t.sizeDelta);
    }

    public void UpdateHealth() {
        healthBarFill.sizeDelta = new Vector2(healthBarTransform.sizeDelta.x * fighter.HealthPercentage, healthBarFill.sizeDelta.y);
    }
}
