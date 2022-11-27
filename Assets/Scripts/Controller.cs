using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    [SerializeField] GameObject AxisSphereContr;
    public GameObject choosenObjeck = null;
    public GameObject controlerAxis = null;

    [SerializeField] Slider resolutionSlider;
    [SerializeField] MyMesh mesh;
    [SerializeField] GameObject meshObj;


    int resolutionVal = 2;


    private Vector3 mOffset;

    private float mZCoord;

    Vector3 mosuePoint;

    bool ableToCahangeAxis = true;


    // Start is called before the first frame update
    void Start()
    {
        meshObj = GameObject.Find("MyMesh");
        //meshObj.transform.GetChild(0).gameObject.SetActive(false);
        StartCoroutine(MyCorout());


    }

    // Update is called once per frame
    void Update()
    {
        SliderChangeResolution();
        Mouse();
        if (Input.GetKey(KeyCode.LeftControl))
        {
            Debug.Log("CONTROL");
            meshObj.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            meshObj.transform.GetChild(0).gameObject.SetActive(false);
            controlerAxis = null;
        }
        Axises();



        //if (Input.GetButtonDown("Fire1"))
        //{
        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        if (hit.collider.gameObject.tag == "SphereControllerX" && choosenObjeck != null)
        //        {
        //            choosenObjeck.transform.position += new Vector3((float)(Input.mouseScrollDelta.x), 0, 0);
        //            Debug.Log("Controller");
        //        }
        //    }
        //}
    }

    //private void OnMouseDown()
    //{
    //    mZCoord = Camera.main.WorldToScreenPoint(choosenObjeck.transform.position).z;

    //    mOffset = choosenObjeck.transform.position - getMouseWorldPos();
    //}

    private Vector3 getMouseWorldPos()
    {
        mZCoord = Camera.main.WorldToScreenPoint(choosenObjeck.transform.position).z;

        mosuePoint = Input.mousePosition;

        mosuePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mosuePoint);
    }

    //private void OnMouseDrag()
    //{
    //    choosenObjeck.transform.position = getMouseWorldPos() + mOffset;
    //}

    void SliderChangeResolution()
    {
        if (resolutionSlider.value != resolutionVal)
        {
            mesh.m = (int)resolutionSlider.value;
            mesh.n = (int)resolutionSlider.value;

            mesh.ChangeResolution((int)resolutionSlider.value, (int)resolutionSlider.value);
            resolutionVal = (int)resolutionSlider.value;
        }
    }

    private void Mouse()
    {
        AssineAxisObject();

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "Sphere")
                {
                    choosenObjeck = hit.collider.gameObject;

                    AxisSphereContr.transform.position = hit.collider.gameObject.transform.position;
                    
                    
                }
            }
            ShpereController( hit, ray);
        }
    }

    void ShpereController(RaycastHit hit, Ray ray) 
    { 
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "SphereControllerX" && choosenObjeck != null)
            {
                choosenObjeck.transform.position += new Vector3((float)(Input.mouseScrollDelta.x), 0, 0);// (float)(Input.mouseScrollDelta.x);
                Debug.Log("Controller");
            }
        }
        else if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "SphereControllerY")
            {
                //choosenObjeck = hit.collider.gameObject;

                AxisSphereContr.transform.position = hit.collider.gameObject.transform.position;


            }
        }
        else if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "SphereControllerZ")
            {
                choosenObjeck = hit.collider.gameObject;

                AxisSphereContr.transform.position = hit.collider.gameObject.transform.position;


            }
        }
    }

    public void AssineAxisObject()
    {
        //Debug.Log(GameObject.Find("MyMesh").transform.GetChild(0).name);

        AxisSphereContr = GameObject.Find("MyMesh").transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
    }

    void Axises()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (Physics.Raycast(ray, out hit) && (ableToCahangeAxis))
                {
                    if (hit.collider.gameObject.tag == "SphereControllerX" && choosenObjeck != null)
                    {
                        controlerAxis = hit.collider.gameObject.gameObject;
                    }
                    else if (hit.collider.gameObject.tag == "SphereControllerY" && choosenObjeck != null)
                    {
                        controlerAxis = hit.collider.gameObject.gameObject;
                    }
                    else if (hit.collider.gameObject.tag == "SphereControllerZ" && choosenObjeck != null)
                    {
                        controlerAxis = hit.collider.gameObject.gameObject;
                    }
                    else
                    {
                        controlerAxis = null;
                    }
                    ableToCahangeAxis = false;
                }
            }
            //Debug.Log((float)(Input.mouseScrollDelta.x));
            //choosenObjeck.transform.position += new Vector3((float)(Input.GetAxis("Mouse X")), 0, 0);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            ableToCahangeAxis = true;

        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (controlerAxis.tag == "SphereControllerX")
            {
                choosenObjeck.transform.position = new Vector3(getMouseWorldPos().x, choosenObjeck.transform.position.y, choosenObjeck.transform.position.z);
                AxisSphereContr.transform.position = choosenObjeck.transform.position;
            }
            else if (controlerAxis.tag == "SphereControllerY")
            {
                choosenObjeck.transform.position = new Vector3(choosenObjeck.transform.position.x, getMouseWorldPos().y, choosenObjeck.transform.position.z);
                AxisSphereContr.transform.position = choosenObjeck.transform.position;
            }
            else if (controlerAxis.tag == "SphereControllerZ")
            {
                choosenObjeck.transform.position = new Vector3(choosenObjeck.transform.position.x, choosenObjeck.transform.position.y, getMouseWorldPos().z);
                AxisSphereContr.transform.position = choosenObjeck.transform.position;
            }
        }
    }

    IEnumerator MyCorout()
    {
        yield return new WaitForSeconds(0.5f);
        meshObj.transform.GetChild(0).gameObject.SetActive(false);

    }
}
