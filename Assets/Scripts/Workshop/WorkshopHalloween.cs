using UnityEngine;
using Unity.Services.RemoteConfig;
using System.Collections;

public class Halloween : MonoBehaviour
{
    public struct userAttributes { }
    public struct appAttributes { }

    public bool isHalloween;

    public GameObject halloweenEquipment;
    public GameObject normalLight;
    public GameObject halloweenLight;

    IEnumerator Start()
    {
        while (!RemoteConfigSettings.Instance)
        {
            yield return null;
        }

        RemoteConfigService.Instance.FetchCompleted += ApplyHalloweenRemote;

        RemoteConfigService.Instance.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());
    }

    private void ApplyHalloweenRemote(ConfigResponse response)
    {
        isHalloween = RemoteConfigService.Instance.appConfig.GetBool("isHalloween");

        if (isHalloween)
        {
            SetupHalloween();
        }
        else
        {
            SetupDefualt();
        }
    }

    private void SetupHalloween()
    {
        halloweenEquipment?.SetActive(true);
        normalLight?.SetActive(false);
        halloweenLight?.SetActive(true);
    }

    private void SetupDefualt()
    {
        halloweenLight.SetActive(false);
        normalLight?.SetActive(true);
        halloweenEquipment?.SetActive(false);
    }

    private void OnDestroy()
    {
        RemoteConfigService.Instance.FetchCompleted -= ApplyHalloweenRemote;
    }
}
