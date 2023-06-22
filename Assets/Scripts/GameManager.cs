using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake() {
        if(GameManager.instance != null) {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(hud);
            Destroy(menu);
            return;
        }

        instance = this;
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //Resources for the game
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices = new List<int> { 50, 100, 200, 400, 500, 1000, 1200, 1400, 1600, 1800, 3000, 3500, 4000, 8000, 16000, 50000};
    public List<int> experienceTable;
    public List<string> keyInInventory;
    

    //References
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;
    public RectTransform hitpointBar;
    public Animator deathMenuAnimator;
    public GameObject hud;
    public GameObject menu;
    public GameObject eventSystem;

    //Logic
    public int coins;
    public int experience;

    //Floating Text
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration) {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    //Upgrade weapon
    public bool TryUpgradeWeapon() {
        //is the weapon max level?
        if (weaponPrices.Count <= weapon.weaponLevel)
            return false;
        if(coins >= weaponPrices[weapon.weaponLevel]) {
            coins -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
    }

    //Health Bar UI
    public void OnHitpointChange() {
        float ratio = (float)player.hitPoint / (float)player.maxHitPoint;

        hitpointBar.localScale = new Vector3(1, ratio, 1);
    }

    //Experience System
    public int GetCurrentLevel() {
        int getLevel = 0;
        int add = 0;

        while(experience >= add) {
            add += experienceTable[getLevel];
            getLevel++;

            //check if we are max level
            if(getLevel == experienceTable.Count) {
                return getLevel;
            }
        }
        return getLevel;
    }

    public int GetExperienceToLevel(int level) {
        int i = 0;
        int xp = 0;

        while(i < level) {
            xp += experienceTable[i];
            i++;
        }
        return xp;
    }

    public void GrantExperience(int xp) {
        int currentLevel = GetCurrentLevel();
        experience += xp;

        if(currentLevel < GetCurrentLevel()) {
            OnLevelUp();
        }
    }

    public void OnLevelUp() {
        player.OnLevelUp();
        OnHitpointChange();
    }

    //On scene loaded, player position
    public void OnSceneLoaded(Scene s, LoadSceneMode mode) {
        player.transform.position = GameObject.Find("SpawnPosition").transform.position;
    }

    //Death Menu and Respawn
    public void Respawn() {
        deathMenuAnimator.SetTrigger("Hide");

        coins = 0;
        experience = 0;
        weapon.SetWeaponLevel(0);

        SceneManager.LoadScene(0);
        player.Respawn();
    }

    //Save state
    /*
     * INT preferedSKin
     * INT coins
     * INT experience
     * INT weaponLEvel
     */
    public void SaveState() {
        string saving = "";

        saving += "0" + "|";
        saving += coins.ToString() + "|";
        saving += experience.ToString() + "|";
        saving += weapon.weaponLevel.ToString();

        PlayerPrefs.SetString("SaveState", saving);
    }

    //Load state
    public void LoadState(Scene s, LoadSceneMode mode) {

        SceneManager.sceneLoaded -= LoadState;
        //if we don't have a save state return
        if (!PlayerPrefs.HasKey("SaveState")) {
            return;
        }

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        //TODO Change player skin
        coins = int.Parse(data[1]);

        //Experience
        experience = int.Parse(data[2]);
        if(GetCurrentLevel() != 1)
            player.SetLevel(GetCurrentLevel());

        //Weapon
        weapon.SetWeaponLevel(int.Parse(data[3]));
    }
}
