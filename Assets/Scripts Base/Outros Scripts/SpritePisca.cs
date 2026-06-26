using UnityEngine;

public class SpritePisca : MonoBehaviour
{
    [SerializeField] private float velocidadeDecaimento = 5f;

    private SpriteRenderer sp;
    private MaterialPropertyBlock propertyBlock;

    private float blinkFactor;

    private static readonly int BlinkFactorID =
        Shader.PropertyToID("BlinkFactor");

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        propertyBlock = new MaterialPropertyBlock();

        Debug.Log(propertyBlock);
        Debug.Log(BlinkFactorID);

    }

    private void Update()
    {
        if (blinkFactor <= 0f)
            return;

        blinkFactor = Mathf.Lerp(
            blinkFactor,
            0f,
            Time.deltaTime * velocidadeDecaimento
        );

        if (blinkFactor < 0.01f)
            blinkFactor = 0f;

        AplicarBlinkFactor();
    }

    public void Piscar()
    {
        blinkFactor = 1f;
        AplicarBlinkFactor();
        Debug.Log("Piscou");
    }

    private void AplicarBlinkFactor()
    {
        sp.GetPropertyBlock(propertyBlock);
        propertyBlock.SetFloat(BlinkFactorID, blinkFactor);
        sp.SetPropertyBlock(propertyBlock);

        Debug.Log("Blink aplicado: " + blinkFactor);
        Debug.Log(sp.sharedMaterial.name);
        
    }
}