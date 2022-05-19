using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] Vector3 nextAngle;
    [SerializeField] Player player;
    [SerializeField] Key key;
    bool isOpen = false;

    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            open();
        }
    }
    void open()
    {
        if (!isOpen)
        {
            if ((key == Key.Red && player.getHaveRedKey() == true) || (key == Key.Blue && player.getHaveBlueKey() == true))
            {
                StartCoroutine(doorEnum(transform.rotation.eulerAngles, nextAngle, 2f));
            }
        }     
    }

    IEnumerator doorEnum(Vector3 from, Vector3 to, float time)
    {
        float percent = 0;
        float speed = 1 / time;
        Vector3 initialPos = from;
        while(percent <= 1)
        {
            percent += Time.deltaTime * speed;
            Vector3 newRotation = Vector3.Lerp(initialPos, to, percent);
            transform.rotation = Quaternion.Euler(newRotation);
            yield return null;
        }
        isOpen = true;
    }

    enum Key
    {
        Red,Blue
    }
}
