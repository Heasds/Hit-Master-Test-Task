using UnityEngine;

public class CharacterShoot : MonoBehaviour
{
    public LevelManager levelManager;

    [SerializeField] private int poolCount;
    [SerializeField] private bool autoExpand;
    [SerializeField] private Transform container; 
    [SerializeField] private Transform bulletSpawnPos;

    private Pool<Bullet> bulletPool;

    public Bullet bulletPrefab;

    private void Start()
    {
        bulletPool = new Pool<Bullet>(bulletPrefab, poolCount, container);
        bulletPool.autoExpand = autoExpand;
    }

    private void Update()
    {
        if (!levelManager.isGameStarted) return;

        if (Input.GetMouseButtonDown(0))
        {
            CharacterRotation();
            CreateBullet();
        }
    }

    private void CreateBullet()
    {
        Bullet newBullet = bulletPool.GetFreeElement();
        newBullet.transform.position = bulletSpawnPos.position;
        newBullet.direction = GetDirection();
        newBullet.transform.LookAt(newBullet.transform.position + GetDirection());
    }

    private void CharacterRotation()
    {
        transform.LookAt(transform.position + GetDirection());
    }

    private Vector3 GetDirection()
    {
        Vector3 screenCentre = new Vector3(Screen.width * 0.5f, 0, Screen.height * 0.5f);
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = mousePos.y;
        mousePos.y = 0;
        Vector3 direction = mousePos - screenCentre;
        return direction.normalized;
    }
}
