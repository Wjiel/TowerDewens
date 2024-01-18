using UnityEngine;

public class BulletScripts : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    [HideInInspector] public Enemu target;

    [SerializeField] private int speed;
    public int damage;

    public AudioClip _audio;

    [SerializeField] private float value;
    private void Start()
    {
        AudioSource.PlayClipAtPoint(_audio,transform.position, value);
    }
    void Update()
    {
        if (target == null)
            Destroy(gameObject);
        else
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemu")
        {
            collision.gameObject.GetComponent<Enemu>().TakeDamage(damage);
        }
        _particleSystem = GameObject.Find("popal").GetComponent<ParticleSystem>();
        _particleSystem.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        _particleSystem.Play();
        Destroy(gameObject);
    }
}
