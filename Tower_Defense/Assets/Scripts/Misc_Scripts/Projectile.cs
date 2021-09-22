using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10.0f;
    public int damage;
    public GameObject target;
    public Vector3 startPosition;
    public Vector3 targetPosition;

    private float distance;
    private float startTime;

    private Game_Manager game_Manager;

    private void Start()
    {
        startTime = Time.time;
        distance = Vector2.Distance(startPosition, targetPosition);
        GameObject gm = GameObject.Find("Game Manager");
        game_Manager = gm.GetComponent<Game_Manager>();
        Vector3 direction = startPosition - target.transform.position;
        gameObject.transform.rotation = Quaternion.AngleAxis(
            Mathf.Atan2(direction.y, direction.x) * 180 / Mathf.PI,
            new Vector3(0, 0, 1));
    }
    private void Update()
    {
        float timeInterval = Time.time - startTime;
        gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, timeInterval * speed / distance);

        

        if (gameObject.transform.position.Equals(targetPosition))
        {
            if (target != null)
            {
                Transform healthBarTransform = target.transform.Find("HealthBar");
                Health_Bar healthBar =
                    healthBarTransform.gameObject.GetComponent<Health_Bar>();
                healthBar.currentHealth -= Mathf.Max(damage, 0);
                if (healthBar.currentHealth <= 0)
                {
                    Destroy(target);

                    game_Manager.Gold += target.GetComponent<Enemy_Data>().reward;
                }
            }
            Destroy(gameObject);
        }
    }
}
