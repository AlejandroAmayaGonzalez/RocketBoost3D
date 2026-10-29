using System.Collections.Generic;
using UnityEngine;

public class ObjectPull : MonoBehaviour {

    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private int poolSize = 10;

    private List<GameObject> objectsList = new List<GameObject>();

    void Start(){ CreatePool(); }

    private void CreatePool(){
        for (int i = 0; i < poolSize; i++){ CreateNewObject(); }
    }

    /// <summary>
    /// Instantiate new object and add it to the pool
    /// </summary>
    /// <returns> GameObject </returns>
    private GameObject CreateNewObject(){
        // Instantiate a new object
        GameObject newObject = Instantiate(objectPrefab);

        // Set it inactive and add it to the list
        newObject.SetActive(false);
        objectsList.Add(newObject);

        return newObject;
    }

    /// <summary>
    /// Get an object from the pool
    /// </summary>
    /// <returns></returns>
    public GameObject GetObjectFromPool(){
        
        // Find an inactive object int the pool
        GameObject theObject = objectsList.Find(obj => !obj.activeInHierarchy);

        if (theObject == null) { theObject = CreateNewObject(); }

        theObject.SetActive(true);
        return theObject;
    }
}
