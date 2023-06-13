using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class FloatingTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefab;

    private List<FloatingText> floatingTexts = new List<FloatingText>();

    private void Start() {
        DontDestroyOnLoad(gameObject);
    }

    private void Update() {
        foreach(FloatingText text in floatingTexts) {
            text.UpdateFloatingText();
        }
    }

    public void Show(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration) {
        FloatingText floatingText = GetFloatingText();

        floatingText.floatText.text = msg;
        floatingText.floatText.fontSize = fontSize;
        floatingText.floatText.color = color;

        floatingText.go.transform.position = Camera.main.WorldToScreenPoint(position); //transfer worldspace to screen space so we can use the Text in our UI
        floatingText.motion = motion;
        floatingText.duration = duration;

        floatingText.Show();
    }

    private FloatingText GetFloatingText() {
        FloatingText txt = floatingTexts.Find(t => !t.active);

        if(txt == null) {
            txt = new FloatingText();
            txt.go = Instantiate(textPrefab);
            txt.go.transform.SetParent(textContainer.transform);
            txt.floatText = txt.go.GetComponent<TextMeshProUGUI>();

            floatingTexts.Add(txt);
        }

        return txt;
    }
}
