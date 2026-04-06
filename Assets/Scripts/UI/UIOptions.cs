using MaiNull.Main_Menu;
using UnityEngine;
using UnityEngine.UI;

namespace MaiNull.UI
{
    public class UIOptions : MonoBehaviour
    {
        [SerializeField] private Slider masterSlider;
        [SerializeField] private Slider musicSlider;

        private void Awake()
        {
            if (!masterSlider || !musicSlider) return;
            
            masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);

            masterSlider.onValueChanged.AddListener(OnMasterVolumeChange);
            musicSlider.onValueChanged.AddListener(OnMusicVolumeChange);
        }

        private static void OnMasterVolumeChange(float value)
        {
            if (MusicManager.Instance != null)
                MusicManager.Instance.SetMasterVolume(value);
        }

        private static void OnMusicVolumeChange(float value)
        {
            if (MusicManager.Instance != null)
                MusicManager.Instance.SetMusicVolume(value);
        }

        public void ExitOptions()
        {
            Destroy(gameObject);
            OptionsManager.Instance.OptionsPanel = null;
        }
    }
}
