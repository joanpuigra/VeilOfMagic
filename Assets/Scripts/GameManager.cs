using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float respawnDelay = 2f;
    
    public void RespawnPlayer()
    {
        StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        yield return new WaitForSeconds(respawnDelay);

        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.Respawn();
            HUD hud = FindObjectOfType<HUD>();
            if (hud != null)
            {
                hud.SetPlayer(player);
            }
        }

        EnemyAttack[] enemies = FindObjectsOfType<EnemyAttack>();
        foreach (EnemyAttack enemy in enemies)
        {
            enemy.enabled = true;
        }
    }
}
