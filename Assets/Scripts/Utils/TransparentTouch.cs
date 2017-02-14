using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Da aggiungere una sola volta ad un solo oggetto (es.: main camera).
/// Invoca automaticamente le funzioni
/// <c>OnTouchDown(Touchinfo t)</c> all'inizio di un touch <br/>
/// <c>OnTouchUp(Touchinfo t)</c> quando il dito si alza <br/>
/// <c>OnTouchStay(Touchinfo t)</c> ogni frame in cui il touch è continuativo e fermo su un oggetto <br/>
/// <c>OnTouchMoved(Touchinfo t)</c> ogni frame che il touch si muove su un oggetto <br/>
/// <c>OnTouchEnded()</c> quando un touch esce da un oggetto (senza sollevare il dito)
/// </summary>
public class TransparentTouch : MonoBehaviour
{
    public bool simulateWithMouse = false;
    private Dictionary<GameObject,bool> touched = new Dictionary<GameObject, bool>();
    private RaycastHit hitinfo;

    void Update()
    {
       
        foreach (Touch touch in Input.touches)
        {
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            if (!Physics.Raycast(ray, out hitinfo))
                continue;
            touched[hitinfo.transform.gameObject]=true;
            TouchInfo touchinfo = new TouchInfo(touch, hitinfo.point);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    hitinfo.transform.SendMessage("OnTouchDown", touchinfo, SendMessageOptions.DontRequireReceiver);
                    break;
                case TouchPhase.Ended:
                    hitinfo.transform.SendMessage("OnTouchUp", touchinfo, SendMessageOptions.DontRequireReceiver);
                    break;
                case TouchPhase.Moved:
                    hitinfo.transform.SendMessage("OnTouchMoved", touchinfo, SendMessageOptions.DontRequireReceiver);
                    break;
                case TouchPhase.Stationary:
                    hitinfo.transform.SendMessage("OnTouchStay", touchinfo, SendMessageOptions.DontRequireReceiver);
                    break;
                case TouchPhase.Canceled:
                    hitinfo.transform.SendMessage("OnTouchEnded", null, SendMessageOptions.DontRequireReceiver);
                    break;
                default: break;
            }
        }

        foreach (GameObject g in touched.Keys)
        {
            if (!touched[g])
            {
                g.SendMessage("OnTouchEnded", null, SendMessageOptions.DontRequireReceiver);
                touched.Remove(g);
            }
            else
                touched[g] = false;
        }

#if UNITY_EDITOR
        if (!simulateWithMouse)
            return;
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0) || Input.GetMouseButton(0))
        {
           Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
           if (!Physics.Raycast(ray, out hitinfo))
                return;
            Touch simulated = new Touch();
            simulated.position = Input.mousePosition;
            if (Input.GetMouseButtonDown(0))
            {
                hitinfo.transform.SendMessage("OnTouchDown", simulated, SendMessageOptions.DontRequireReceiver);
                return;
            }
            if (Input.GetMouseButtonUp(0))
            {
                hitinfo.transform.SendMessage("OnTouchUp", simulated, SendMessageOptions.DontRequireReceiver);
                return;
            }
        }
#endif
    }
}

/// <summary>
/// Stuttura per non perdere le informazioni già calcolate sul punto di contatto.
/// </summary>
public struct TouchInfo
{
    public Touch touch;
    public Vector3 worldPosition;

    public TouchInfo(Touch t, Vector3 p)
    {
        touch = t;
        worldPosition = p;
    }

    public static implicit operator Touch(TouchInfo i)
    {
        return i.touch;
    }
}