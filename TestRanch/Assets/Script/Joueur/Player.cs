using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject playerCam;

    [SerializeField] AudioSource item_AS;
    [SerializeField] MeshFilter equiped;
    [SerializeField] GameObject lampe;
    [SerializeField] private Transform offset;

    private CameraControl camControl;


    [SerializeField] Text interactableMsg;

    [SerializeField] private int inventaireTaille = 5;
    [SerializeField] private int interactionDistance = 5;

    [SerializeField] private int creatureThrowSpeed = 10;

    private PlayerInventory barreInventaire;
    private Coffre openChest;
    private AbstractInventoryUI openedNonChestInventory;
    private Slot selected;
    private GameObject creature;

    #region getset
    public int InventaireTaille { get => inventaireTaille; }
    public PlayerInventory BarreInventaire { get => barreInventaire; set => barreInventaire = value; }//assigné dans le script playerinventoryhub
    public Slot Selected { get => selected; set => selected = value; }
    public Coffre OpenChest { get => openChest; set => openChest = value; }
    public Transform Offset { get => offset; set => offset = value; }
    public AbstractInventoryUI OpenedNonChestInventory { get => openedNonChestInventory; set => openedNonChestInventory = value; }
    public MeshFilter Equiped { get => equiped; set => equiped = value; }
    public AudioSource Item_AS { get => item_AS; set => item_AS = value; }
    public CameraControl CamControl { get => camControl;}

    #endregion

    public void CaptureCreature(CreatureBehavior creature)
    {
        this.creature = creature.gameObject;
    }

    private void Start()
    {
        camControl = this.GetComponentInChildren<CameraControl>();

    }

    private void ThrowCreature()
    {
        creature.transform.position = offset.position;
        creature.transform.rotation = offset.rotation;
        creature.GetComponent<Rigidbody>().AddForce(transform.TransformDirection(Vector3.forward) * creatureThrowSpeed, ForceMode.Impulse);
        this.creature = null;
    }

    private void UseItem()
    {
        if (!camControl.IsCameraLocked())
        {
            selected.ItemStack.UseItem(this);
            item_AS.Play();
        }
    }

    private void Update()
    {
        RaycastHit hitUpdate;

        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.TransformDirection(Vector3.forward), out hitUpdate, interactionDistance))
        {

            if (hitUpdate.collider.GetComponent<IInteractible>() != null && !camControl.IsCameraLocked())
            {

                interactableMsg.gameObject.SetActive(true);
            }
            else
            {
                interactableMsg.gameObject.SetActive(false);
            }
        }
        else
        {
            interactableMsg.gameObject.SetActive(false);
        }

        #region inputs
        if (Input.GetButtonDown("Fire1"))
        {
            if (!camControl.IsCameraLocked())
                UseItem();
        }
        if (Input.GetButtonDown("Interact"))
        {
            //Debug.Log("e");
            if (!camControl.IsCameraLocked())
            {
                if (HitScan(out RaycastHit hit))
                {
                    if (DistanceCheck(hit))
                    {
                        Interaction(hit);
                    }
                }
            }
        }
        if (Input.GetButtonDown("Fire2"))
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = !Cursor.visible;
        }
        float b = Input.GetAxis("Mouse ScrollWheel") * 10;
        int a = Mathf.RoundToInt(b);
        barreInventaire.ScrollItembar(-a);


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            barreInventaire.SelectItem(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            barreInventaire.SelectItem(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            barreInventaire.SelectItem(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            barreInventaire.SelectItem(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            barreInventaire.SelectItem(4);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            barreInventaire.SelectItem(4);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            barreInventaire.SelectItem(5);
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            barreInventaire.SelectItem(6);
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            barreInventaire.SelectItem(7);
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            barreInventaire.SelectItem(8);
        }

        if (Input.GetButtonDown("Lampe"))
        {
            lampe.SetActive(!lampe.activeSelf);
        }

        if (Input.GetButtonDown("Map"))
        {
            MapButtonAction();
        }
        /* if (Input.GetButtonDown("Creature") && creature != null)
         {
             ThrowCreature();
         }*/

        #endregion
    }


    private void MapButtonAction()
    {
        UIManager.Instance.MapOpenClose();
    }

    public bool DistanceCheck(RaycastHit hit) =>
        hit.distance <= (interactionDistance + selected.ItemStack.GetInteractionRangeBonus());

    private void Interaction(RaycastHit hit)
    {
        IInteractible inter = hit.collider.GetComponent<IInteractible>();
        if (inter != null)
        {
            inter.Interact(this);
        }
    }

    public bool HitScan(out RaycastHit hit)
    {

        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.TransformDirection(Vector3.forward), out hit))
        {
            return true;
        }
        else
        {
            Debug.DrawRay(playerCam.transform.position, playerCam.transform.TransformDirection(Vector3.forward) * 1000, Color.red, 5f);
            Debug.Log("no hit");
            return false;
        }
    }

    public void CloseChest()
    {
        openChest = null;
    }


    public void IncreaseInventorySize(int newSize)
    {
        inventaireTaille = newSize;
        BarreInventaire.IncreaseSize(newSize);
    }
}
