using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletLifeTime;
    [SerializeField] private float speed;

    public Vector3 direction;

    private void OnEnable()
    {
        StartCoroutine(BulletLifeRoutine());
    }

    private void OnDisable()
    {
        StopCoroutine(BulletLifeRoutine());
    }

    public void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Disactive();
    }

    private IEnumerator BulletLifeRoutine()
    {
        yield return new WaitForSeconds(bulletLifeTime);
        Disactive();
    }

    public void Disactive()
    {
        gameObject.SetActive(false);
    }
}
