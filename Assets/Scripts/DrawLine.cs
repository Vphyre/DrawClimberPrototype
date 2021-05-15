using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public class DrawLine : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject legR;

    public GameObject legL;

    private GameObject rightObj;

    private GameObject leftObj;

    GameObject lineGo;

    bool startDrawing;
    bool destroyLegs;

    Vector3 mousePos;

    LineRenderer lr;

    [SerializeField]
    Material lineMat;

    public static int currentIndex;

    [SerializeField]
    Camera cam;

    [SerializeField]
    Transform colliderPrefab;

    Transform lastInstantiatedCollider;

    public void OnPointerDown(PointerEventData eventData)
    {
        NewObj();
        PlayerBehaviour.changeSpeed = true;
        startDrawing = true;

        mousePos = Input.mousePosition;

        lr = lineGo.AddComponent<LineRenderer>();

        lr.startWidth = 0.1f;

        lr.material = lineMat;

        lr.useWorldSpace = false;
    }
    public void OnPointerUp(PointerEventData eventData)
    {   
        leftObj = Instantiate(lineGo, new Vector3(legL.transform.position.x-0.240f, legL.transform.position.y, legL.transform.position.z), Quaternion.Euler(180, 0, 0), legL.transform);
        rightObj = Instantiate(lineGo, new Vector3(legR.transform.position.x, legR.transform.position.y, legR.transform.position.z),Quaternion.identity, legR.transform);

        PlayerBehaviour.changeSpeed = false;
        currentIndex = 0;
        startDrawing = false;
        destroyLegs = true;
    }
    void Start()
    {
        destroyLegs = false;
    }

    void Update()
    {
        DrawLegs();
    }
    void NewObj()
    {
        if(destroyLegs)
        {
           Destroy(rightObj);
           Destroy(leftObj);
           Destroy(lineGo);
           destroyLegs = false;  
        }

        lineGo = new GameObject();
        rightObj = new GameObject();
        leftObj = new GameObject();
    }
    void DrawLegs()
    {
        if(startDrawing)
        {
            Vector3 dist = mousePos - Input.mousePosition;
            
            float distanceSqrMag = dist.sqrMagnitude;

            if(dist.sqrMagnitude > 5f)
            {
                lr.SetPosition(currentIndex, cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y, Input.mousePosition.z + 10f)));

                if(lastInstantiatedCollider!= null)
                {
                    Vector3 currLinePos = lr.GetPosition(currentIndex);

                    lastInstantiatedCollider.LookAt(currLinePos);

                    lastInstantiatedCollider.localScale = new Vector3(lastInstantiatedCollider.localScale.x, lastInstantiatedCollider.localScale.y, Vector3.Distance(lastInstantiatedCollider.position, currLinePos) * 0.5f);
                }
                lastInstantiatedCollider = Instantiate(colliderPrefab, lr.GetPosition(currentIndex),Quaternion.identity, lineGo.transform);

                mousePos = Input.mousePosition;

                currentIndex++;

                lr.positionCount = currentIndex +1;

                lr.SetPosition(currentIndex, cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y, Input.mousePosition.z + 10f)));

                if(currentIndex>69)
                {
                    startDrawing = false;
                }
            }
        }

    }
}
