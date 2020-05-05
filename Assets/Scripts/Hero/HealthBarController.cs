using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public GameObject heartContainerPrefab; // Prefab of heart
    private HeroInteractive hero; // Hero object
    private GameObject[] heartContainers; // Hero's health points
    private Image[] heartFills; // Health points fills
    public Transform heartsParent; // Parent object for hearts

    private void Start()
    {
        hero = FindObjectOfType<HeroInteractive>();

        heartContainers = new GameObject[(int)hero.GetMaxHealth()];
        heartFills = new Image[(int)hero.GetMaxHealth()];

        hero.onHPChangeCallback += UpdateHeartsUI; 

        InitiateHearts();
        UpdateHeartsUI();
    }

    /// <summary>
    /// Function to update hearts UI
    /// </summary>
    public void UpdateHeartsUI()
    {
        ActiveHearts();
        FillHearts();
    }

    /// <summary>
    /// Function to set active hearts objects
    /// </summary>
    void ActiveHearts()
    {
        for (int i = 0; i < heartContainers.Length; i++)
        {
            if (i < hero.GetMaxHealth())
            {
                heartContainers[i].SetActive(true);
            }
            else
            {
                heartContainers[i].SetActive(false);
            }
        }
    }

    /// <summary>
    /// Function to fill hero's hearts
    /// </summary>
    void FillHearts()
    {
        for (int i = 0; i < heartFills.Length; i++)
        {
            if (i < hero.GetHealth())
            {
                heartFills[i].fillAmount = 1;
            }
            else
            {
                heartFills[i].fillAmount = 0;
            }
        }

        if (hero.GetHealth() % 1 != 0)
        {
            int lastPos = Mathf.FloorToInt(hero.GetHealth());
            heartFills[lastPos].fillAmount = hero.GetHealth() % 1;
        }
    }

    /// <summary>
    /// Function initiate hearts objects
    /// </summary>
    void InitiateHearts()
    {
        for (int i = 0; i < hero.GetMaxHealth(); i++)
        {
            GameObject temp = Instantiate(heartContainerPrefab);
            temp.transform.SetParent(heartsParent, false);
            heartContainers[i] = temp;
            heartFills[i] = temp.transform.Find("HeartFill").GetComponent<Image>();
        }
    }
}
