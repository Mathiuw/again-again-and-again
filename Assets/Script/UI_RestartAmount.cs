using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UI_RestartAmount : MonoBehaviour
{
    private void Awake()
    {
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        text.text = PlayerPrefs.GetInt("RestartCount").ToString();
    }
}
