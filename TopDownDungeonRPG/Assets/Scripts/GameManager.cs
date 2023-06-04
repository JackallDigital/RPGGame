using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake() {
        if(GameManager.instance != null) {
            Destroy(gameObject);
            return;
        }

        PlayerPrefs.DeleteAll();


        instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
    }

    //Resources for the game
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> experienceTable;

    //References
    public Player player;
    //public Weapon weapon //and so on
    public FloatingTextManager floatingTextManager;

    //Logic
    public int coins;
    public int experience;

    //Floating Text
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration) {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
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
        saving += "0";

        PlayerPrefs.SetString("SaveState", saving);
    }

    //Load state
    public void LoadState(Scene s, LoadSceneMode mode) {
        //if we don't have a save state return
        if (!PlayerPrefs.HasKey("SaveState")) {
            return;
        }

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        //TODO Change player skin

        coins = int.Parse(data[1]);
        experience = int.Parse(data[2]);

        //TODO Change player experience

        Debug.Log("Load state");
    }
}
