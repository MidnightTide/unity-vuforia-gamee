using UnityEngine;

public class ClickTester : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit))
                Debug.Log("ClickTester hit: " + hit.collider.gameObject.name);
            else
                Debug.Log("ClickTester hit nothing");
        }
    }
}
