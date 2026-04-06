using TMPro;
using UnityEngine;

namespace MaiNull.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class UIRestartAmount : MonoBehaviour
    {
        private void Awake()
        {
            TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
            text.text = PlayerPrefs.GetInt("RestartCount").ToString();
        }
    }
}
