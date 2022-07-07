using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_DestroyPlataform : MonoBehaviour
{
    public GameObject player;
    public GameObject plataform;

    private GameObject myPlat;
    private int distanciaDoPlayer;


    // Start is called before the first frame update
    void Start()
    {
      distanciaDoPlayer = 15;
      // plataform_y = Random.RandomRange(player.transform.position.y,player.transform.position.y);
    }

    void Update()
    {
      transform.position = new Vector3(transform.position.x, player.transform.position.y - distanciaDoPlayer);
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col) {

        if(col.gameObject.tag == "Ground")
        Instantiate(plataform, new Vector2(Random.RandomRange(-5f,5f),player.transform.position.y + Random.RandomRange(0.5f,1f)),Quaternion.identity);
        Destroy(col.gameObject);
    }
}
