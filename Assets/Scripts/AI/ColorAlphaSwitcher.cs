using UnityEngine;

public class ColorAlphaSwitcher : MonoBehaviour
{
    [SerializeField] float alphaSpeed;
    public float interpTC;

    bool isAlphaIncreasing;
    SpriteRenderer sprRen;

    bool spritesSetupDone;
    float speedDamper;

    public void Start()
    {
        interpTC = 0.1f;
        speedDamper = 1f;
        isAlphaIncreasing = true;
        sprRen = new SpriteRenderer();
        spritesSetupDone = false;
    }

    public void Update()
    {
        if (!spritesSetupDone)
        {
            SetupOfSprites();
        }

        //foreach (SpriteRenderer sprRen in sprRens)
        //{
            sprRen.color = new Color(sprRen.color.r, sprRen.color.g, sprRen.color.b, Mathf.Lerp(0.05f, 0.85f, interpTC));
            if (interpTC < 0.75f && isAlphaIncreasing)
            {
                interpTC += alphaSpeed * speedDamper * Time.deltaTime;
            }

            else if (isAlphaIncreasing)
            {
                isAlphaIncreasing = false;
            }

            if (interpTC > 0.05f && !isAlphaIncreasing)
            {
                interpTC -= alphaSpeed * speedDamper * Time.deltaTime;
            }
            else if (!isAlphaIncreasing)
            {
                isAlphaIncreasing = true;
            }
        //}
    }

    public void SetupOfSprites()
    {
        sprRen = this.gameObject.GetComponent<SpriteRenderer>();
        //sprRens.Add(this.gameObject.GetComponent<SpriteRenderer>());

        //List<GameObject> childGameObjectsOfSpriteObject = new List<GameObject>();
        //this.gameObject.transform.GatherAllChildren(ref childGameObjectsOfSpriteObject, 1);

        //foreach (GameObject sprObj in childGameObjectsOfSpriteObject)
        //{
        //    sprRens.Add(sprObj.GetComponent<SpriteRenderer>());
        //}

        spritesSetupDone = true;
    }
}
