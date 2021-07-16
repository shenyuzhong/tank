using UnityEngine;
using System.Collections;

public class ShellController : MonoBehaviour {

    public GameObject explosionPrefab;
    public GameObject smokePrefab;
    bool exploded = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter(Collider other)
    {
        if (!exploded)
        {
            GetComponent<AudioSource>().Play();
            exploded = true;
        }

        this.gameObject.GetComponent<Renderer>().enabled = false;
        Destroy(this.gameObject, 2);

        if (other.gameObject.tag == "Bunker")
            Destroy(other.gameObject);

        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as GameObject;
        GameObject smoke = Instantiate(smokePrefab, transform.position, Quaternion.identity) as GameObject;
        Destroy(explosion, 2);
        Destroy(smoke, 2);
    }
}
