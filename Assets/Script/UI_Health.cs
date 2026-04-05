using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UI_Health : MonoBehaviour
{
    [SerializeField] private Sprite[] _tilemap;
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Start()
    {
        Transform player = GameObject.FindWithTag("Player").transform;

        if (player)
        {
            Health health = player.GetComponent<Health>();

            if (health)
            {
                health.OnHealthChange += OnHealthChange;
                OnHealthChange(health.HitCount);
            }
        }        
    }

    private void OnHealthChange(int healthAmount)
    {
        switch (healthAmount) 
        {
            case 0:
                _image.sprite = _tilemap[5];
                break;
            case 1:
                _image.sprite = _tilemap[4];
                break;
            case 2:
                _image.sprite = _tilemap[3];
                break;
            case 3:
                _image.sprite = _tilemap[2];
                break;
            case 4:
                _image.sprite = _tilemap[1];
                break;
            case 5:
                _image.sprite = _tilemap[0];
                break;
            default:
                Debug.LogError("Error loading health value");
                break;
        }
    }
}
