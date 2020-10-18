using UnityEngine;

public class SelfRotation : MonoBehaviour
{

    [SerializeField] private Vector3 _selfRotationAngles;

	void Update ()
    {
        transform.Rotate(_selfRotationAngles * Time.deltaTime);
	}

}