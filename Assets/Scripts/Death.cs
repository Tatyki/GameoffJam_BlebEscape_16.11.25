using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Myha"))
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
