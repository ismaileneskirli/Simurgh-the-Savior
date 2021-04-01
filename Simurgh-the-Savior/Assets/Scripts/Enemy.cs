using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Script for destroying enemies when they hit by particles.
    private void OnParticleCollision(GameObject other) {
        Debug.Log($"Enemy hit by {other.gameObject.name}");
        Destroy(gameObject);
    }
}
