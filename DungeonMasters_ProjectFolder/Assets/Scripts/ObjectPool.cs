using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

    /// HOW TO CALL FUNCTION
    /// ObjectPool.Instance.SpawnFromPool(tag, Vector3, Quaternion)

    [System.Serializable]
    public class Blueprint
    {
        public string tag;
        public GameObject prefab;
        public GameObject display;
        public int count;
    }

    #region Singleton
    public static ObjectPool Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public List<Blueprint> blueprints = new List<Blueprint>();
    public Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();

	// Use this for initialization
	void Start () {
        //For every Pool in Pools list
        foreach (Blueprint blueprint in blueprints)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            //Instantiate 
            for (int i = 0; i < blueprint.count; i++)
            {
                GameObject obj = Instantiate(blueprint.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(blueprint.tag, objectPool);
        }

	}

    public void SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {

        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return;
        }

        //Removes object from Que
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        //return objectToSpawn;
    }
	
}
