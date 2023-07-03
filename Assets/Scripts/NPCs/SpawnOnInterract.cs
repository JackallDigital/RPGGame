using UnityEngine;

public class SpawnOnInterract : MonoBehaviour
{
    public GameObject spawnEnemyOnInterract;
    public GameObject[] objectsToUnlock;
    public NPCDialogue NPC;
    public bool spawned = false;
    private bool unlocked = false;
    private bool pickedUp = false;

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
            GameManager.instance.ShowText("Thanks lad! The <color=#F3F222>key</color> is in the <color=#00D4CE>crate</color>, you know the drill!", 15, Color.white, transform.position + new Vector3(0, 0.16f, 0), Vector3.zero, 5.0f);
        }
        if (objectsToUnlock[1] == null && !pickedUp) {
            pickedUp = true;
            GameManager.instance.ShowText("Use the <color=#EC8787>Healing fountain</color><br>before leaving adventurer!", 15, Color.white, transform.position + new Vector3(0, 0.16f, 0), Vector3.zero, 5.0f);
        }
    }

    private void Unlock() {
        for (int i = 0; i < objectsToUnlock.Length; i++) {
            objectsToUnlock[i].tag = "Fighter";
        }
    }
}
