using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnInterract : MonoBehaviour
{
    public GameObject spawnEnemyOnInterract;
    public GameObject[] objectsToUnlock;
    public NPCDialogue NPC;
    public bool spawned = false;
    private bool unlocked = false;

    // Start is called before the first frame update
    void Start()
    {
        spawnEnemyOnInterract.SetActive(false);
        NPC = GetComponent<NPCDialogue>();
        //crate = GetComponent<GameObject>();
    }

    // Update is called once per frame
    public void Spawn() {
        if (NPC.interracted && !spawned) {
            spawnEnemyOnInterract.SetActive(true);
            spawned = true;
        }
    }

    private void Update() {
        if (spawned && spawnEnemyOnInterract == null && !unlocked) {
            unlocked = true;
            Unlock();
        }
    }

    private void Unlock() {
        for (int i = 0; i < objectsToUnlock.Length; i++) {
            objectsToUnlock[i].tag = "Fighter";
        }
    }
}
