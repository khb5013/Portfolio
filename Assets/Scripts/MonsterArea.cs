using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterArea : MonoBehaviour
{
    BoxCollider2D areaCollider;

    void Start()
    {
        areaCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Monster monster = other.GetComponent<Monster>();
        if (monster != null)
        {

            Vector2 clampedPosition = new Vector2(
                Mathf.Clamp(monster.transform.position.x, areaCollider.bounds.min.x, areaCollider.bounds.max.x),
                Mathf.Clamp(monster.transform.position.y, areaCollider.bounds.min.y, areaCollider.bounds.max.y)
            );
            monster.transform.position = clampedPosition;
             
        }
    }
}
