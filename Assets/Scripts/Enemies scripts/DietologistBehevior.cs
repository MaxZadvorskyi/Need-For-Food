using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DietologistBehevior : EnemyBehevior
{
    [SerializeField] private float dietologistSpeed;
    // Update is called once per frame
    void Update()
    {
        EnemyMoving();
    }

    public override void EnemyMoving()
    {
        float speed = dietologistSpeed;
        enemyRB.AddForce(transform.TransformVector(Vector3.forward) * speed * Time.deltaTime, ForceMode.Impulse); // making objects move in right direction
        MovementRestrictions();
    }
}