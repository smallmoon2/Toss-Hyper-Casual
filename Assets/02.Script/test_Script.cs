using UnityEngine;

public class test_Script : MonoBehaviour
{

    public GameObject prefab;

    private void OnEnable()
    {
        Debug.Log("�����");
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space)){

            Instantiate(prefab);

            Debug.Log("�Է�");
        }
    }
}
