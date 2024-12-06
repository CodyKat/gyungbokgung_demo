using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class WalkSound : MonoBehaviour
{
    public InputDeviceCharacteristics controllerCharacteristics;
    public List<GameObject> controllerPrefabs;
    private InputDevice targetDevice;
    private GameObject spawnedController;
    private AudioSource audioSource;

    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);
        audioSource = gameObject.GetComponent<AudioSource>();


        //input devices üũ debug
        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
            //targetDevice�� �̸��� ������ controller�� ã�´�. 
            GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);

            if (prefab) //prefab�� ������ �� 
            {
                spawnedController = Instantiate(prefab, transform);
                Debug.Log(prefab);
            }
            else //prefab�� controller�� ã�� ���� ��
            {
                Debug.LogError("Did not find corresponding controller model.");
                spawnedController = Instantiate(controllerPrefabs[0], transform);
            }
        }
    }

    void Update()
    {
        //input�� debug
        // if (targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) &&
        //     primaryButtonValue)
        //     Debug.Log("Pressing Primary Button");

        // if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) &&
        //     triggerValue > 0.1f)
        //     Debug.Log("Triggerr pressed" + triggerValue);

        if (targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxisValue) && primary2DAxisValue != Vector2.zero){
            audioSource.Play();
        }
        else {
            audioSource.Stop();
        }
    
    }
}
