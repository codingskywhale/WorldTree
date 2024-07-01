using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUnitMoveController : MonoBehaviour
{
    Rigidbody2D rb;
    CombatUnit unit;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        unit = GetComponent<CombatUnit>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        if (!unit.isMeetEnemy || unit.nowTarget == null)
        {
            rb.velocity = (unit.combatUnitData.unitType == UnitType.Player) ? Vector2.left : Vector2.right;
            rb.velocity *= unit.combatUnitData.moveSpeed;
            return;
        }
        rb.velocity = Vector2.zero;
    }
}
