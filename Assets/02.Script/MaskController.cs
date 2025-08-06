using UnityEngine;

public class MaskController : MonoBehaviour
{
    private void Start()
    {
        //ActivateMaskChild(1);
    }
    public void ActivateMaskChild(int num)
    {
        Transform mask = FindDeepChild(transform, "Mask");  // �ڽ� �������� Ž��

        if (mask == null)
        {
            Debug.LogWarning($"{name}�� Mask ������Ʈ�� ã�� �� �����ϴ�.");
            return;
        }

        if (num < 0 || num >= mask.childCount)
        {
            Debug.LogWarning($"{name}�� Mask�� {num}�� �ڽ��� �������� �ʽ��ϴ�.");
            return;
        }

        for (int i = 0; i < mask.childCount; i++)
        {
            mask.GetChild(i).gameObject.SetActive(i == num);
        }

        Debug.Log($"{name}�� Mask �ڽ� �� {num}���� Ȱ��ȭ �Ϸ�");
    }

    private Transform FindDeepChild(Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
                return child;

            Transform result = FindDeepChild(child, name);
            if (result != null)
                return result;
        }
        return null;
    }
}
