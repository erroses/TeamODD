using UnityEngine;

public class JarState : MonoBehaviour
{
    public Material blueMaterial;
    public Material orangeMaterial;
    public Material redMaterial;

    public int maxHealth = 2;
    public int currentHealth;
    void Start()
    {
        currentHealth = 2;
    }

    public void ChangeColor()
    {
        // Renderer 컴포넌트를 가져옵니다.
        Renderer renderer = GetComponent<Renderer>();

        switch(currentHealth)
        {
            case 0: renderer.material = redMaterial; break;
            case 1: renderer.material = orangeMaterial; break;
            case 2: renderer.material = blueMaterial; break;
        }
    }
}
