using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // To use UI components

public class PickUpScript : MonoBehaviour
{
    [Header("Keybinds")]
    public KeyCode GrabOrPlaceKey;

    [Header("Configuration")]
    public bool enablePostItNotes = false;
    public int postItNoteLimit = 5;
    public GameObject player;
    public Transform holdPos;
    public GameObject postItPrefab;

    [Header("Pick-up UI")]
    public RawImage pickupUIImage; // UI element for pickup interaction
    public float pickUpRange = 2f; // how far the player can pick up the object from
    private GameObject heldObj; // object which we pick up
    private Rigidbody heldObjRb; // Rigidbody of object we pick up
    private int LayerNumber; // layer index

    void Start()
    {
        LayerNumber = LayerMask.NameToLayer("holdLayer"); // if your holdLayer is named differently, make sure to change this
        pickupUIImage.enabled = false; // Make sure the UI is hidden at start
    }

    void Update()
    {

        // Place/grab object
        if (Input.GetKeyDown(GrabOrPlaceKey)) // change E to whichever key you want to press to pick up
        {
            ObjectInteraction();
        }

        if (heldObj != null) // if player is holding an object
        {
            MoveObject(); // keep object position at holdPos
        }

        ShowPickupUI(); // Check to show pickup UI
    }

    private void ShowPickupUI()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
        {
            // Check if we are looking at a pick-upable object
            if (hit.transform.CompareTag("canPickUp"))
            {
                // Show UI if the object is pick-upable and the player isn't holding anything
                if (heldObj == null)
                {
                    pickupUIImage.enabled = true;
                }
                else
                {
                    pickupUIImage.enabled = false;
                }
            }
            else
            {
                pickupUIImage.enabled = false; // Hide UI if not looking at an interactable object
            }
        }
        else
        {
            pickupUIImage.enabled = false; // Hide UI when not looking at anything within range
        }
    }

    private void ObjectInteraction()
    {
        RaycastHit hit;
        if (heldObj == null) // if currently not holding anything
        {
            // perform raycast to check if player is looking at an object within pickuprange
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
            {
                // make sure pickup tag is attached
                if (hit.transform.gameObject.tag == "canPickUp")
                {
                    PickUpObject(hit.transform.gameObject);
                }
            }
        }
        else
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange, ~(1 << LayerNumber)))
            {
                // make sure right tag is attached
                if (hit.transform.gameObject.tag == "canBePlacedOn")
                {
                    PlaceObject(hit.transform.parent.gameObject);
                }
            }
        }
    }

    void PickUpObject(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>()) // make sure the object has a Rigidbody
        {
            heldObj = pickUpObj; // assign heldObj to the object that was hit by the raycast (no longer == null)
            heldObjRb = pickUpObj.GetComponent<Rigidbody>(); // assign Rigidbody
            heldObjRb.isKinematic = true;
            heldObjRb.transform.parent = holdPos.transform; // parent object to hold position
            heldObj.layer = LayerNumber; // change the object layer to the holdLayer
            heldObj.GetComponentInChildren<Collider>().isTrigger = true;

            SetGameLayerRecursive(heldObj, LayerNumber);

            // make sure object doesn't collide with player, it can cause weird bugs
            Physics.IgnoreCollision(heldObj.GetComponentInChildren<Collider>(), player.GetComponent<Collider>(), true);

            // check if the object was placed on a placable object
            if (heldObj.GetComponent<GrabbableObjectScript>().placedOnPlacable)
            {
                if (heldObj.Equals(heldObj.GetComponent<GrabbableObjectScript>().placedOnPlacable.GetComponent<PlacerScript>().heldObject))
                {
                    // resetting placable value for keeping track of placed object
                    heldObj.GetComponent<GrabbableObjectScript>().placedOnPlacable.GetComponent<PlacerScript>().heldObject = null;
                }
            }

            // resetting grabbable parameter for keeping track of the thing it's placed on
            heldObj.GetComponent<GrabbableObjectScript>().placedOnPlacable = null;
        }
    }

    void PlaceObject(GameObject placeOnObj)
    {
        GameObject placerIsHolding = placeOnObj.GetComponent<PlacerScript>().heldObject;

        Physics.IgnoreCollision(heldObj.GetComponentInChildren<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.layer = 0; // object assigned back to default layer

        SetGameLayerRecursive(heldObj, 0);

        heldObj.GetComponentInChildren<Collider>().isTrigger = false;

        if (heldObj.GetComponent<GrabbableObjectScript>().hasPhysics)
        {
            heldObjRb.isKinematic = false;
        }
        else
        {
            // Post-it note
            heldObj.transform.rotation = placeOnObj.transform.rotation;
            heldObj.transform.Rotate(90, 0, 0);
        }
        heldObj.transform.parent = null; // unparent object
        heldObj.transform.position = placeOnObj.transform.position; // placing object in the right place
        // linking object with the thing it's placed on and vice versa
        placeOnObj.GetComponent<PlacerScript>().heldObject = heldObj;
        heldObj.GetComponent<GrabbableObjectScript>().placedOnPlacable = placeOnObj;

        if (placerIsHolding != null)
        {
            PickUpObject(placerIsHolding); // swapping object
        }
        else
        {
            heldObj = null; // undefine game object
        }
    }



    private void SetGameLayerRecursive(GameObject gameObject, int layer)
    {
        gameObject.layer = layer;
        foreach (Transform child in gameObject.transform)
        {
            SetGameLayerRecursive(child.gameObject, layer);
        }
    }

    void MoveObject()
    {
        // keep object position the same as the hold position
        heldObj.transform.position = holdPos.transform.position;
    }

    public string GetHeldObjectID()
    {
        if (heldObj != null)
        {
            if (heldObj.GetComponent<GrabbableObjectScript>())
            {
                return heldObj.GetComponent<GrabbableObjectScript>().objectID; // returns the string object ID
            }
        }
        return "";
    }

    public GameObject GetHeldObj()
    {
        return heldObj;
    }
}
