using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int hp;

    private Waypoint waypoint;

    public TextMeshProUGUI hpCounter;
    public Slider hpSlider;
    public GameObject ragdollPrefab;

    private void Start()
    {
        waypoint = GetComponentInParent<Waypoint>();
        hpSlider.maxValue = hp;
        UpdateHpValue();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Bullet>() != null)
        {
            hp -= 1;
            UpdateHpValue();

            if (hp <= 0) OnDead();
        }
    }

    private void UpdateHpValue()
    {
        hpCounter.text = hp.ToString();
        hpSlider.value = hp;
    }

    private void OnDead()
    {
        Instantiate(ragdollPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        waypoint.enemies.Remove(this);
    }
}
