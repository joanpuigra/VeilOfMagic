using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private GameObject[] heartLive;
    [SerializeField] private GameObject[] heartDmg;
    [SerializeField] private GameObject[] heartLoss;
    [SerializeField] private TextMeshProUGUI ammoText;

    private Player player;
    private int maxHearts;
    private float health;
    private int ammo;
    
    private void Start()
    {
        player = FindObjectOfType<Player>();
        maxHearts = heartLive.Length;
        ammoText.text = player.ammo.ToString();
        UpdateHearts();
    }

    private void Update()
    { 
        UpdateHearts();
        UpdateAmmo();
    }

    private void UpdateAmmo()
    {
        if (player == null) return;

        ammo = player.ammo;
        ammoText.text = ammo.ToString();
    }

    public void SetPlayer(Player newPlayer)
    {
        player = newPlayer;
    }
    
    private void UpdateHearts()
    {
        if (player == null) return;

        float playerHealth = player.health;
        int fullHearts = Mathf.FloorToInt(playerHealth / 30f);
        float remainder = playerHealth % 30f;

        for (int i = 0; i < maxHearts; i++)
        {
            heartLive[i].SetActive(false);
            heartDmg[i].SetActive(false);
            heartLoss[i].SetActive(false);

            if (i < fullHearts)
            {
                heartLive[i].SetActive(true);
            }
            else if (i == fullHearts && remainder > 0)
            {
                heartDmg[i].SetActive(true);
            }
            else
            {
                heartLoss[i].SetActive(true);
            }
        }
    }
}