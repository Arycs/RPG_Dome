using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && collision.GetComponent<Enemy>().IsAlive)
        {
            collision.GetComponent<Character>().TakeDamage(5, transform.parent.parent);
            collision.transform.position -= (transform.parent.parent.position - collision.transform.position).normalized / 2;
            collision.GetComponent<Enemy>().Select();
        }
    }
}
