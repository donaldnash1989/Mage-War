using UnityEngine;

public class BasicSpellCast : MonoBehaviour, ISpellCast
{
    [SerializeField] private GameObject spellPrefab;
    [SerializeField] private float rateOfFire = 10.0f;
    private float timer = 1.0f;

    public void Cast()
    {
        timer += Time.deltaTime;
        if(timer >= 1.0f / rateOfFire)
        {

            if (Input.GetMouseButton(0))
            {
                Instantiate(spellPrefab, transform.position, transform.rotation);
                timer = 0.0f;
            }
        }
    }
}
