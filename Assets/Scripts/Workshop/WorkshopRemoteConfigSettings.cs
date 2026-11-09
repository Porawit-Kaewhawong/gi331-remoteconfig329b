using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Services.RemoteConfig;
using Unity.Services.Core;
using Unity.Services.Authentication;

using System.Threading.Tasks;

public class RemoteConfigSettings : MonoBehaviour
{
    public static RemoteConfigSettings Instance { get; private set; }
    public string environmentId;

    public struct userAttributes
    {
        // Optionally declare variables for any custom user attributes:
        // These variables can be updated as the game progresses and then used in Campaign Audience Targeting!
        //public int score;

        // items inventory[]
        // levelsCompleted
    }

    public struct appAttributes
    {
        // Optionally declare variables for any custom app attributes:

        // i.e
        // public int level;
        // public string appVersion;
    }


    public async Task InitializeRemoteConfigAsync()
    {
        // initialize handlers for Unity game services

        await UnityServices.InitializeAsync();


        // As of Remote Config 3.0.0 SDK,
        // * Remote Config now requires Authentication for managing environment information and retrieving Setting *
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
        // Set the environment ID:
        RemoteConfigService.Instance.SetEnvironmentID(environmentId);

    }

    async void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (Utilities.CheckForInternetConnection())
        {
            await InitializeRemoteConfigAsync();
        }
    }


    IEnumerator Start()
    {
        //For Testing only Update every 3 second [Best Pratice : Use when load new scene or reload]
        while (true)
        {
            yield return new WaitForSeconds(3);
            RemoteConfigService.Instance.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());
        }

    }
}
