//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using TMPro;
//using Vuforia;
//using System.Collections;

//public class CloudReco : MonoBehaviour
//{

//    public class ObjectTargetInfo
//    {
//        public string name;
//        public string points;

//        public static ObjectTargetInfo CreateFromJSON(string jsonString)
//        {
//            return JsonUtility.FromJson<ObjectTargetInfo>(jsonString);
//        }
//    }

//    CloudRecoBehaviour mCloudRecoBehaviour;
//    string mTargetMetadata = "";
//    ObjectTargetInfo datosTarget;
//    bool mIsScanning = false;
//    public TextMeshProUGUI PlanetText;
//    public ImageTargetBehaviour ImageTargetTemplate;

//    void Awake()
//    {
//        mCloudRecoBehaviour = GetComponent<CloudRecoBehaviour>();
//        mCloudRecoBehaviour.RegisterOnStateChangedEventHandler(OnStateChanged);
//        mCloudRecoBehaviour.RegisterOnNewSearchResultEventHandler(OnNewSearchResult);
//    }
//    //Unregister cloud reco callbacks when the handler is destroyed
//    void OnDestroy()
//    {
//        mCloudRecoBehaviour.RegisterOnStateChangedEventHandler(OnStateChanged);
//        mCloudRecoBehaviour.UnregisterOnNewSearchResultEventHandler(OnNewSearchResult);
//    }

//    public void OnNewSearchResult(CloudRecoBehaviour.CloudRecoSearchResult cloudRecoSearchResult)
//    {
//        PlanetText.text = "Funciona";

//        datosTarget = ObjectTargetInfo.CreateFromJSON(cloudRecoSearchResult.MetaData);
//        PlanetText.text = datosTarget.name;
//        PlanetText.text = "Funciona";

//        //if (mTargetMetadata == PlanetText.text)
//        //    PlanetText.text = "CORRECTO!";

//        // Stop the scanning by disabling the behaviour
//        mCloudRecoBehaviour.enabled = false;
//    }

//    public void OnStateChanged(bool scanning)
//    {
//        mIsScanning = scanning;

//        if (scanning)
//        {
//            // Clear all known targets
//        }
//    }


//    void OnGUI()
//    {
//        // Display current 'scanning' status
//        GUI.Box(new Rect(100, 100, 200, 50), mIsScanning ? "Scanning" : "Not scanning");
//        // Display metadata of latest detected cloud-target
//        GUI.Box(new Rect(100, 200, 200, 50), "Metadata: " + datosTarget.name);
//        // If not scanning, show button
//        // so that user can restart cloud scanning
//        if (!mIsScanning)
//        {
//            if (GUI.Button(new Rect(100, 300, 200, 50), "Restart Scanning"))
//            {
//                // Reset Behaviour
//                mCloudRecoBehaviour.enabled = true;
//                mTargetMetadata = "";
//            }
//        }
//    }
//}

using UnityEngine;
using Vuforia;
using System.Collections;
using TMPro;

public class CloudReco : MonoBehaviour
{
    CloudRecoBehaviour mCloudRecoBehaviour;
    bool mIsScanning = false;
    string mTargetMetadata = "";
    public TextMeshProUGUI PlanetText;

    public ImageTargetBehaviour ImageTargetTemplate;

    // Register cloud reco callbacks
    void Awake()
    {
        mCloudRecoBehaviour = GetComponent<CloudRecoBehaviour>();
        mCloudRecoBehaviour.RegisterOnInitializedEventHandler(OnInitialized);
        mCloudRecoBehaviour.RegisterOnInitErrorEventHandler(OnInitError);
        mCloudRecoBehaviour.RegisterOnUpdateErrorEventHandler(OnUpdateError);
        mCloudRecoBehaviour.RegisterOnStateChangedEventHandler(OnStateChanged);
        mCloudRecoBehaviour.RegisterOnNewSearchResultEventHandler(OnNewSearchResult);
    }
    //Unregister cloud reco callbacks when the handler is destroyed
    void OnDestroy()
    {
        mCloudRecoBehaviour.UnregisterOnInitializedEventHandler(OnInitialized);
        mCloudRecoBehaviour.UnregisterOnInitErrorEventHandler(OnInitError);
        mCloudRecoBehaviour.UnregisterOnUpdateErrorEventHandler(OnUpdateError);
        mCloudRecoBehaviour.UnregisterOnStateChangedEventHandler(OnStateChanged);
        mCloudRecoBehaviour.UnregisterOnNewSearchResultEventHandler(OnNewSearchResult);
    }
    public void OnInitialized(CloudRecoBehaviour cloudRecoBehaviour)
    {
        Debug.Log("Cloud Reco initialized");
    }

    public void OnInitError(CloudRecoBehaviour.InitError initError)
    {
        Debug.Log("Cloud Reco init error " + initError.ToString());
    }

    public void OnUpdateError(CloudRecoBehaviour.QueryError updateError)
    {
        Debug.Log("Cloud Reco update error " + updateError.ToString());

    }

    public void OnStateChanged(bool scanning)
    {
        mIsScanning = scanning;

        if (scanning)
        {
            // Clear all known targets
        }
    }

    // Here we handle a cloud target recognition event
    public void OnNewSearchResult(CloudRecoBehaviour.CloudRecoSearchResult cloudRecoSearchResult)
    {

        if (ImageTargetTemplate)
        {
            /* Enable the new result with the same ImageTargetBehaviour: */
            mCloudRecoBehaviour.EnableObservers(cloudRecoSearchResult, ImageTargetTemplate.gameObject);
        }

        mTargetMetadata = cloudRecoSearchResult.MetaData;
        PlanetText.text = mTargetMetadata;

        mCloudRecoBehaviour.enabled = false;
    }

    void OnGUI()
    {
        // Display current 'scanning' status
        GUI.Box(new Rect(100, 100, 200, 50), mIsScanning ? "Scanning" : "Not scanning");
        // Display metadata of latest detected cloud-target
        GUI.Box(new Rect(100, 200, 200, 50), "Metadata: " + mTargetMetadata);
        // If not scanning, show button
        // so that user can restart cloud scanning
        if (!mIsScanning)
        {
            if (GUI.Button(new Rect(100, 300, 200, 50), "Restart Scanning"))
            {
                // Reset Behaviour
                mCloudRecoBehaviour.enabled = true;
                mTargetMetadata = "";
            }
        }
    }
}