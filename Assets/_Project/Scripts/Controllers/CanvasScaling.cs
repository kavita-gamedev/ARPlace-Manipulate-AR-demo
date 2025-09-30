using UnityEngine;
using UnityEngine.UI;

public class CanvasScaling : MonoBehaviour
{
    public bool keepScaleOne = false;
    public enum ENUM_Device_Type
    {
        Tablet,
        Phone
    }


    private void Awake()
    {
        setcanvas();
    }
    

    void setcanvas()
    {
      
        Debug.Log("Device Type___________________________" + isDeviceTablet());
        CanvasScaler c = GetComponent<UnityEngine.UI.CanvasScaler>();
        tablet = isDeviceTablet();
        if (tablet)
        {
            if (keepScaleOne)
            {
                c.matchWidthOrHeight = 1;
            }
            else
            {
                c.matchWidthOrHeight = .8f;
            }
            
        }
        else
        {
            c.matchWidthOrHeight = 0;
            
        }
    }



    public float GetDeviceAspectRatio()//Call this before Set Resolution
    {
        float toReturn = ((float)Screen.height / (float)Screen.width);
        return toReturn;
    }


    public bool isDeviceTablet()
    {

#if UNITY_IOS
        Debug.Log("GetDeviceType" + GetDeviceType());
        if (IOS_GetDeviceType() == ENUM_Device_Type.Tablet)
        {
            return true;
        }
        else
        {
            return false;
        }
#else

        Debug.Log("GetDeviceType" + GetDeviceType());
        if (GetDeviceType() == ENUM_Device_Type.Tablet)
        {
            return true;
        }
        else
        {
            return false;
        }
#endif
    }



    public static bool isTablet;
    public bool tablet;

    private static float DeviceDiagonalSizeInInches()
    {
        float screenWidth = Screen.width / Screen.dpi;
        float screenHeight = Screen.height / Screen.dpi;
        float diagonalInches = Mathf.Sqrt(Mathf.Pow(screenWidth, 2) + Mathf.Pow(screenHeight, 2));

        return diagonalInches;
    }

#if UNITY_IOS
    public static ENUM_Device_Type IOS_GetDeviceType()
    {
        ENUM_Device_Type type = ENUM_Device_Type.Tablet;
        bool deviceIsIpad = UnityEngine.iOS.Device.generation.ToString().Contains("iPad");
        if (deviceIsIpad)
        {
            type = ENUM_Device_Type.Tablet;

        }
        bool deviceIsIphone = UnityEngine.iOS.Device.generation.ToString().Contains("iPhone");
        if (deviceIsIphone)
        {
            type = ENUM_Device_Type.Phone;
            
        }
        
        return type;
    }
#endif


    public static ENUM_Device_Type GetDeviceType()
    {
        //Debug.Log("Screen.width = " + Screen.width + "Screen.height" + Screen.height);

        if (Screen.width == 1080 && Screen.height == 1920 || Screen.width == 1920 && Screen.height == 1080)
        {
            //Debug.Log("exception screen case 1080x1920");
            isTablet = false;
        }
        else
        {
            float aspectRatio = Mathf.Max(Screen.width, Screen.height) / Mathf.Min(Screen.width, Screen.height);
            isTablet = (DeviceDiagonalSizeInInches() > 6.5f && aspectRatio < 2f);
            Debug.Log("isTablet" + isTablet + "DeviceDiagonalSizeInInches" + DeviceDiagonalSizeInInches() + "aspectRatio=" + aspectRatio);

        }
        
        if (isTablet)
        {
            Debug.Log("tablet selected");
            return ENUM_Device_Type.Tablet;
        }
        else
        {
            return ENUM_Device_Type.Phone;
        }
        
    }
}


