using UnityEngine;
using System.Collections;

public class PowerUpSpeed : CollidableObject {

    public int duration;
    [Range(0,1)]
    public float amount;

    void OnDisable() {
        CancelInvoke();
    }

    public override void HitPlayer() {

        spawner.EnableSlow(amount);
        Invoke("DisableSlow", duration);

        base.HitPlayer();
    }

    private void DisableSlow() {
        spawner.DisableSlow(amount);
    }
}
