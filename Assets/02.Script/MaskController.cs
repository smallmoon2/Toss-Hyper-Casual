using UnityEngine;

public class MaskController : MonoBehaviour
{
    private void Start()
    {
        //ActivateMaskChild(1);
    }
    public void ActivateMaskChild(int num)
    {
        Transform mask = FindDeepChild(transform, "Mask");  // 자신 기준으로 탐색

        if (mask == null)
        {
            Debug.LogWarning($"{name}의 Mask 오브젝트를 찾을 수 없습니다.");
            return;
        }

        if (num < 0 || num >= mask.childCount)
        {
            Debug.LogWarning($"{name}의 Mask에 {num}번 자식이 존재하지 않습니다.");
            return;
        }

        for (int i = 0; i < mask.childCount; i++)
        {
            mask.GetChild(i).gameObject.SetActive(i == num);
        }

        Debug.Log($"{name}의 Mask 자식 중 {num}번만 활성화 완료");
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
