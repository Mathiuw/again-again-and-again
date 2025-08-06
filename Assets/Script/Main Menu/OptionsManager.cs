using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public static OptionsManager Instance { get; private set; }

    [SerializeField] private UI_Options _optionsPrefab;
    public UI_Options OptionsPanel { get; set; }
    [SerializeField] private bool _inputActivate = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (_inputActivate && Input.GetKeyDown(KeyCode.Escape))
        {
            EnterOptions();
        }
    }

    public void EnterOptions() 
    {
        OptionsPanel = Instantiate(_optionsPrefab);
    }
}