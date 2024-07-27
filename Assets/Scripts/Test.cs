using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Test : MonoBehaviour
{
    public List<GameObject> allObjects = new List<GameObject>();
    public List<GameObject> objects = new List<GameObject>();
    public List<GameObject> objects1 = new List<GameObject>();
    public Transform testTrans;

    private void Awake()
    {
        for (int i = 0; i < testTrans.childCount; i++)
        {
            allObjects.Add(testTrans.GetChild(i).gameObject);
        }
    }

    private void Update()
    {
        Tests();
    }

    private void Tests()
    {
        objects.Clear();
        objects1.Clear();
        foreach (var obj in allObjects)
        {
            // Vector3.Distance(obj.transform.position,transform.position);
            // Vector3.Distance(obj.transform.position,transform.position);
            objects.Add(obj);
            objects1.Add(obj);
        }
    }
}
