using UnityEngine;
using System.Collections;

public class PowerUpSpeed : CollidableObject {

    public int duration;

    public override void HitPlayer()
    {
        ObjectManager o = GameObject.FindObjectOfType<ObjectManager>();

        o.EnableSlow();

        Invoke("DisableSlow", duration);

        ReturnToPool();
    }

    private void DisableSlow() {
        ObjectManager o = GameObject.FindObjectOfType<ObjectManager>();

        o.DisableSlow();
    }
}
