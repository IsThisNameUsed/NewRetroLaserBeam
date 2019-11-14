using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Store en stand by
public class Store : MonoBehaviour {

    public Pickable[] pickable;

    IEnumerator nextObject(int time)
    {
        yield return new WaitForSeconds(time);
        StartNewSequence();
    }

    void Start() {
        Component[] components = GetComponents(typeof(Pickable));
        pickable = new Pickable[components.Length];

        for (int i = 0; i < components.Length; i++)
        {
            pickable[i] = components[i] as Pickable;
        }
        StartNewSequence();
    }

    // Update is called once per frame
    void Update() {


    }

    void StartNewSequence()
    {
        StopAllCoroutines();
        StartCoroutine(nextObject(Random.Range(2, 5)));
    }

    void CreateNewPickable()
    {
        int type = Random.Range(1, 3);
        GameObject item = null;
        Pickable itemScript;
        switch(type)
        {
            case 1: item = Resources.Load("Items/item") as GameObject;
                    itemScript = item.AddComponent<PersonnalHeal>();
            break;
            case 2: item = Resources.Load("Items/item") as GameObject;
                    itemScript = item.AddComponent<GroupHeal>(); 
            break;
        }

        item.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 5;
    }
}
