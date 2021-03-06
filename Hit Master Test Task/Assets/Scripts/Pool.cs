using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : MonoBehaviour
{
    private List<T> pool;
    public T prefab { get; }
    public bool autoExpand { get; set; }
    public Transform container { get; }

    public Pool(T prefab, int count)
    {
        this.prefab = prefab;
        this.container = null;

        this.CreatePool(count);
    }

    public Pool(T prefab, int count, Transform container)
    {
        this.prefab = prefab;
        this.container = container;

        this.CreatePool(count);
    }

    private void CreatePool(int count)
    {
        this.pool = new List<T>();

        for (int i = 0; i < count; i++)
        {
            this.CreateBullet();
        }
    }

    private T CreateBullet(bool isActiveByDefault = false)
    {
        var createdObject = Object.Instantiate(this.prefab, this.container);
        createdObject.gameObject.SetActive(isActiveByDefault);
        this.pool.Add(createdObject);
        return createdObject;
    }

    public bool HasFreeElement(out T element)
    {
        foreach (var mono in pool)
        {
            if (!mono.gameObject.activeInHierarchy)
            {
                element = mono;
                mono.gameObject.SetActive(true);
                return true;
            }
        }

        element = null;
        return false;
    }

    public T GetFreeElement()
    {
        if (this.HasFreeElement(out var element)) return element;

        if (this.autoExpand) return CreateBullet(true);

        throw new System.Exception("No free elements");
    }
}
