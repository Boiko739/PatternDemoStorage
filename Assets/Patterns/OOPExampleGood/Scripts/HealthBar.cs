using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject[] _hearts;
    private int curHealth;

    void Start()
    {
        curHealth = _player.Health;
        
        RedrawHearts(curHealth);
    }

    void Update()
    {
        if (curHealth != _player.Health)
        {
            curHealth = _player.Health;
            RedrawHearts(curHealth);
        }
    }

    public void RedrawHearts(int health)
    {
        for (int i = 0; i < _hearts.Length; i++)
        {
            _hearts[i].gameObject.SetActive(i < health);
        }
    }
}