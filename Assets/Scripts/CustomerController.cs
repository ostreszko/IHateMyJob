using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerController : MonoBehaviour
{
    GameObject nearShelf;
    ShelfController nearShelfController;
    public NavMeshGameObj navMeshGameObj;
    GameMaster gm;
    Quaternion rotation;
    AudioSource coinsSound;
    string takenItemTag;
    Vector3 currentPosition;
    Animator anim;
    void Start()
    {
        gameObject.transform.Rotate(Vector3.left,-90f);
        gm = GameMaster.GM;
        rotation = transform.rotation;
        currentPosition = transform.position;
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (currentPosition == transform.position)
        {
            anim.SetBool("Idle", true);
        }
        else
        {
            anim.SetBool("Idle", false);
        }
        currentPosition = transform.position;
    }
    private void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Shelf"))
        {
            nearShelf = collision.gameObject;
            nearShelfController = nearShelf.GetComponent<ShelfController>();
        }
        else if (collision.CompareTag("Exit") && (navMeshGameObj.customerState == (int)GameMaster.customerState.HasNotFoundProduct || navMeshGameObj.customerState == (int)GameMaster.customerState.HasFoundProduct) )
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Shelf"))
        {
            nearShelf = null;
            nearShelfController = null;
        }
    }

    private void OnDestroy()
    {
        gm.currentCustomersNumber--;
    }

    public bool GetItemFromShelf()
    {
        if (nearShelfController != null)
        {
            return nearShelfController.RemoveItemFromShelf();
        }
        else
        {
            return false;
        }

    }
}
