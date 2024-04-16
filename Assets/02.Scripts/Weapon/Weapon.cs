using UnityEngine;

public enum WeaponType
{
    Wind,
    Fire,
    Arrow
}

public class Weapon : MonoBehaviour
{
    public WeaponType Wtype;

    private float _timer = 0;
    public float ActiveTime = 1f;
    //public float Speed = 3;

    private void Update()
    {
        if (Wtype == WeaponType.Wind)
        {
            transform.Translate(Vector3.right * 0.2f * Time.deltaTime);
        }

        else if (Wtype == WeaponType.Fire)
        {
            transform.Translate(Vector3.right * 0.5f * Time.deltaTime);
        }

        else if (Wtype == WeaponType.Arrow)
        {
            transform.Translate(Vector3.right * 2 * Time.deltaTime);
        }
        
        _timer += Time.deltaTime;
        if (_timer > ActiveTime)
        {
            this.gameObject.SetActive(false);
            _timer = 0;
        }
    }
}
