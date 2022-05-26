using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DietologistBehevior : EnemyBehevior // INHERITANCE
{
    [SerializeField] private float dietologistSpeed;
    // Update is called once per frame
    void Update()
    {
        EnemyMoving();
    }

    public override void EnemyMoving() // POLYMORPHISM
    {
        float speed = dietologistSpeed;
        enemyRB.AddForce(transform.TransformVector(Vector3.forward) * speed * Time.deltaTime, ForceMode.Impulse); // makes objects move in right direction
        MovementRestrictions();
    }
}