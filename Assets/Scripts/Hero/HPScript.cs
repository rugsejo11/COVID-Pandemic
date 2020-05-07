using UnityEngine;
using UnityEngine.UI;

public class HPScript : MonoBehaviour
{
    #region Variables

    [SerializeField] private GameObject heartContainerPrefab = null; // Prefab of heart
    [SerializeField] private Transform heartsParent = null; // Parent object for hearts
    private HeroDataScript hero; // Hero object
    private GameObject[] heartContainers; // Hero's health points
    private Image[] heartFills; // Health points fills

    #endregion

    #region Functions

    /// <summary>
    /// Function is called on the frame when a script is enabled just before any of the Update methods are called the first time
    /// </summary>
    private void Start()
    {
        hero = FindObjectOfType<HeroDataScript>(); // Main character

        heartContainers = new GameObject[(int)hero.GetMaxHealth()]; // Hearts container
        heartFills = new Image[(int)hero.GetMaxHealth()]; // Hearts filling container

        hero.SetDelegate(UpdateHeartsUI); // Set hero delegate

        InitiateHearts(); // Initiate hearts objects
        UpdateHeartsUI(); // Update hearts UI
    }

    /// <summary>
    /// Function to update hearts UI
    /// </summary>
    private void UpdateHeartsUI()
    {
        ActiveHearts(); // Set active hearts objects
        FillHearts(); // Fill hero's hearts
    }

    /// <summary>
    /// Function to set active hearts objects
    /// </summary>
    private void ActiveHearts()
    {
        // Go through hearts container
        for (int i = 0; i < heartContainers.Length; i++)
        {
            // If [i] value less than variable Max health value, activate heart
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
    private void FillHearts()
    {
        // Go through hearts fills container
        for (int i = 0; i < heartFills.Length; i++)
        {
            // If [i] value less than hero's healths amount, activate heart
            if (i < hero.GetHealth())
            {
                heartFills[i].fillAmount = 1;
            }
            else
            {
                heartFills[i].fillAmount = 0;
            }
        }
    }

    /// <summary>
    /// Function to initiate hearts objects
    /// </summary>
    private void InitiateHearts()
    {
        for (int i = 0; i < hero.GetMaxHealth(); i++)
        {
            if (!HeartContainerAndParentExist())
                break;

            GameObject temp = Instantiate(heartContainerPrefab);
            temp.transform.SetParent(heartsParent, false); // Create child object for a heart
            heartContainers[i] = temp;
            heartFills[i] = temp.transform.Find("HeartFill").GetComponent<Image>(); // Fill child object with a heart
        }
    }
    private bool HeartContainerAndParentExist()
    {
        if (heartContainerPrefab == null)
        {
            Debug.LogError("hearts container prefab not found!");
            return false;
        }
        if (heartsParent == null)
        {
            Debug.LogError("Hearts parent not found!");
            return false;
        }
        return true;
    }
    #endregion
}
