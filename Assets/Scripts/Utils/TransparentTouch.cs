using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Da aggiungere una sola volta ad un solo oggetto (es.: main camera). <br/>
/// Invoca automaticamente le funzioni:
/// <c>OnTouchDown(Touchinfo t)</c> all'inizio di un touch <br/>
/// <c>OnTouchUp(Touchinfo t)</c> quando il dito si alza <br/>
/// <c>OnTouchStay(Touchinfo t)</c> ogni frame in cui il touch è continuativo e fermo su un oggetto <br/>
/// <c>OnTouchMoved(Touchinfo t)</c> ogni frame che il touch si muove su un oggetto <br/>
/// <c>OnTouchExit()</c> quando un touch esce da un oggetto (anche senza sollevare il dito)<br/>
/// <c>OnTouchCancel()</c> quando il touch non è più tracciato. Così dice unity, non so cosa significhi.<br/>
/// </summary>
public class TransparentTouch : MonoBehaviour
{
    /// <summary>
    /// Se <c>true</c> il mouse sarà usato nell'editor per simulare eventi OnTouchDown e OnTouchUp.
    /// Non ho onestamente lo sbatto di implementare e lanciare tutti gli eventi corrispondenti
    /// </summary>
    public bool simulateWithMouse = true;
    private Dictionary<GameObject, bool> touched = new Dictionary<GameObject, bool>();
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
                    hitinfo.transform.SendMessage("OnTouchCancel", null, SendMessageOptions.DontRequireReceiver);
                    break;
                default: break;
            }
        }

#if !UNITY_EDITOR
       
        foreach (GameObject g in touched.Keys.ToArray())
        {
            if (!touched[g])
            {
                g.SendMessage("OnTouchExit", null, SendMessageOptions.DontRequireReceiver);
                touched.Remove(g);
            }
            else
                touched[g] = false;
        }

#else 
        if (!simulateWithMouse)
            return;
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0) || Input.GetMouseButton(0))
        {
           Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
           if (!Physics.Raycast(ray, out hitinfo))
                return;
            Touch t = new Touch();
            t.position = Input.mousePosition;
            TouchInfo simulated = new TouchInfo(t, hitinfo.point);
            
            if (Input.GetMouseButtonDown(0))
            {
                hitinfo.transform.SendMessage("OnTouchDown", simulated, SendMessageOptions.DontRequireReceiver);
                touched[hitinfo.transform.gameObject] = true;
                return;
            }
            if (Input.GetMouseButtonUp(0))
            {
                hitinfo.transform.SendMessage("OnTouchUp", simulated, SendMessageOptions.DontRequireReceiver);
                touched.Clear();
                return;
            }

            if (Input.GetMouseButton(0) && touched.Keys.Count>0)
            {
                hitinfo.transform.SendMessage("OnTouchMoved", simulated, SendMessageOptions.DontRequireReceiver);
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

    public TouchInfo(Touch t)
    {
        RaycastHit hitinfo;
        Ray ray = Camera.main.ScreenPointToRay(t.position);
        if (!Physics.Raycast(ray, out hitinfo))
            throw new Exception("Touch is not on any collider.");

        this.touch = t;
        this.worldPosition = hitinfo.point;
    }
}