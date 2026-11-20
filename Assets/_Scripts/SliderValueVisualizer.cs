using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueVisualizer : MonoBehaviour
{
    TMP_Text txt;

    private void Awake()
    {
        txt = GetComponent<TMP_Text>();
        UpdateText(transform.parent.GetComponentInChildren<Slider>()?.value ?? 100);
    }

    public void UpdateText(float value)
    {
        txt.text = value.ToString("0") + "%";
    }
}
