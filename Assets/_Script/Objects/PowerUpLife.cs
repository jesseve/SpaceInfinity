﻿using UnityEngine;
using System.Collections;

public class PowerUpLife : CollidableObject
{
    public int healAmount = 1;

    public override void HitPlayer() {
        base.HitPlayer();
        player.HitObject(-healAmount);
    }
}
