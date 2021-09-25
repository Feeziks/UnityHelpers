using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolTest : MonoBehaviour
{
  public int testAmount;
  public float spawnRate; //In Hz

  private UnityHelpers.ObjectPool<TestPoolableObject> testObjectPool;
  private List<TestPoolableObject> currentlyHeldObjects;
  private int objectCounter;
  private float lastSpawnTime;
  private bool spawnDespawn;

  // Start is called before the first frame update
  void Start()
  {
    testObjectPool = new UnityHelpers.ObjectPool<TestPoolableObject>();
    currentlyHeldObjects = new List<TestPoolableObject>();
    objectCounter = 0;
    lastSpawnTime = 0f;
    spawnDespawn = true;
  }

  // Update is called once per frame
  void Update()
  {
    if (spawnDespawn)
    {
      if ((objectCounter < testAmount) && (Time.realtimeSinceStartup > (lastSpawnTime + (1f / spawnRate))))
      {
        TestPoolableObject t = testObjectPool.GetObject();
        t.go.SetActive(true);
        t.go.transform.parent = transform;
        t.go.transform.position = new Vector3(Random.Range(-30f, 30f), Random.Range(-30f, 30f), Random.Range(-30f, 30f));
        currentlyHeldObjects.Add(t);
        objectCounter++;
        lastSpawnTime = Time.realtimeSinceStartup;
        if(objectCounter == testAmount)
        {
          spawnDespawn = !spawnDespawn;
        }
      }
    }
    else if(!spawnDespawn)
    {
      if (Time.realtimeSinceStartup > (lastSpawnTime + (1f / spawnRate)))
      {
        TestPoolableObject t = currentlyHeldObjects[0];
        t.go.SetActive(false);
        currentlyHeldObjects.Remove(t);
        testObjectPool.ReturnObject(t);
        objectCounter--;
        lastSpawnTime = Time.realtimeSinceStartup;
        if (objectCounter == 0)
        {
          spawnDespawn = !spawnDespawn;
        }
      }
    }
  }
}
