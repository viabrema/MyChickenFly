using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Die : MonoBehaviour
{

    GameObject chicken;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chicken"))
        {
            // Acessar o script Chicken e modificar a variável die
            var chickenScript = chicken.GetComponent<Arrow>();
            Debug.Log("Die");
            Debug.Log(chickenScript);
            if (chickenScript != null)
            {
                chickenScript.die = true;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        chicken = GameObject.Find("Pivo");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
