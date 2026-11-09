using UnityEngine;
using Unity.Services.RemoteConfig;
using System.Collections;

public class AssignmentChristmas : MonoBehaviour
{
    public struct userAttributes { }
    public struct appAttributes { }

    public bool isChristmas;

    public GameObject christmasEquipment;
    public GameObject christmasProps;
    public GameObject normalProps;

    IEnumerator Start()
    {
        while (!AssignmentRemoteConfigSettings.Instance)
        {
            yield return null;
        }

        RemoteConfigService.Instance.FetchCompleted += ApplyChristmasRemote;

        RemoteConfigService.Instance.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());
    }

    private void ApplyChristmasRemote(ConfigResponse response)
    {
        isChristmas = RemoteConfigService.Instance.appConfig.GetBool("isChristmas");

        if (isChristmas)
        {
            SetupChristmas();
        }
        else
        {
            SetupDefault();
        }
    }

    private void SetupChristmas()
    {
        christmasEquipment?.SetActive(true);
        normalProps?.SetActive(false);
        christmasProps?.SetActive(true);
    }

    private void SetupDefault()
    {
        christmasEquipment?.SetActive(false);
        normalProps?.SetActive(true);
        christmasProps?.SetActive(false);
    }

    private void OnDestroy()
    {
        RemoteConfigService.Instance.FetchCompleted -= ApplyChristmasRemote;
    }
}
