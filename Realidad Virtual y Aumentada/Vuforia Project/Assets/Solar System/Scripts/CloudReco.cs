using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Vuforia;
using System.Collections;

public class CloudReco : MonoBehaviour
{

    public class ObjectTargetInfo
    {
        public string name;
        public string points;

        public static ObjectTargetInfo CreateFromJSON(string jsonString)
        {
            return JsonUtility.FromJson<ObjectTargetInfo>(jsonString);
        }
    }

    CloudRecoBehaviour mCloudRecoBehaviour;
    string mTargetMetadata = "";
    ObjectTargetInfo datosTarget;
    bool mIsScanning = false;
    public TextMeshProUGUI PlanetText;

    void Awake()
    {
        mCloudRecoBehaviour = GetComponent<CloudRecoBehaviour>();
        mCloudRecoBehaviour.RegisterOnNewSearchResultEventHandler(OnNewSearchResult);
    }
    //Unregister cloud reco callbacks when the handler is destroyed
    void OnDestroy()
    {
        mCloudRecoBehaviour.UnregisterOnNewSearchResultEventHandler(OnNewSearchResult);
    }

    public void OnNewSearchResult(CloudRecoBehaviour.CloudRecoSearchResult cloudRecoSearchResult)
    {
        datosTarget = ObjectTargetInfo.CreateFromJSON(cloudRecoSearchResult.MetaData);
        PlanetText.text = datosTarget.name;

        //if (mTargetMetadata == PlanetText.text)
        //    PlanetText.text = "CORRECTO!";

        // Stop the scanning by disabling the behaviour
        mCloudRecoBehaviour.enabled = false;
    }




    void OnGUI()
    {
        // Display current 'scanning' status
        GUI.Box(new Rect(100, 100, 200, 50), mIsScanning ? "Scanning" : "Not scanning");
        // Display metadata of latest detected cloud-target
        GUI.Box(new Rect(100, 200, 200, 50), "Metadata: " + datosTarget.name);
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
