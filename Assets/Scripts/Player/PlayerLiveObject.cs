using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerLiveObject : MonoBehaviour
{

    [SerializeField]
    private float _maxLife;
    [SerializeField]
    private float _life;
    const int _constZero = 0;
    const int _constOne = 1;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    public AudioClip _damagedSound;
    [SerializeField]
    public AudioClip _deadSound;

    public bool _takingDamage;
    public bool _isDead;
    private bool _isInFire;
    private float _inFireTimer;
    private float _fireDamage;
    private float _fireTime;
    private int _tickCount;
    public bool _isInvul;
    private float _timeToBackVulnerable;
    [SerializeField]
    private float _invulnerableTime;
    public int _hitsRecived;
    [SerializeField]
    private int _hitsBlockInInvulnerableState = 2;
    [SerializeField]
    private AudioClip _fireSound;
    [SerializeField]
    private AudioClip _invulnerableHitSound;
    [SerializeField]
    private AudioClip _maxLifeAdd;
    [SerializeField]
    private Image _lifeBar;
    [SerializeField]
    private Image _bloodMark;
    [SerializeField]
    private float _timeOfFadeOfMark;
    [SerializeField]
    private float _actualTimeOfFadeOfMark;
    [SerializeField]
    private Image _fire;
    private bool _markIsActive;
    [SerializeField]
    private Text _lifeBarValue;
    [SerializeField]
    private ParticleSystem _maxLifeUpParticle;
    [SerializeField]
    private Transform _maxLifeUpParticlePosition;
    [SerializeField]
    private GameObject[] _particlesLife;
    [SerializeField]
    private string _faderTrigger;
    [SerializeField]
    private FaderManager _fader;
    [SerializeField]
    private PlayerFunctions _playerFunctions;
    [SerializeField]
    private AudioClip _invulnerabilityAudioClip;
    [SerializeField]
    private AudioClip _lifeAddAudioClip;
    [SerializeField]
    private AudioClip _buffAttack;
    void Start()
    {
        if (_audioSource == null)
        {
            _audioSource = GetComponent<AudioSource>();
        }
        if (_fader == null)
        {
            _fader = FindObjectOfType<FaderManager>();
            if (_fader == null)
            {
                Debug.LogError("No se pudo encontrar al objeto FaderManager");
                return;
            }
        }
        if (_playerFunctions == null)
        {
            _playerFunctions = GetComponent<PlayerFunctions>();
            if (_playerFunctions == null)
            {
                Debug.LogError("No se pudo encontrar al componente PlayerFunctions");
                return;
            }
        }
        _isDead = false;
        _isInFire = false;
        _fireDamage = _constZero;
        _fireTime = _constZero;
        _life = _maxLife;
    }

    void Update()
    {
        InFire(_fireDamage, _fireTime, _isInFire);
        MarkApearAndFade();
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    AddMaxLife(2);
        //}
    }
    public void TakeDamage(int _damage)
    {
        if (!_isInvul)
        {
            _life -= _damage;
            if (_life <= _constZero)
            {
                _life = _constZero;
            }
            _lifeBarValue.text = (_life + "/" + _maxLife);
            _lifeBar.fillAmount = _life / _maxLife;
            _markIsActive = true;
            _actualTimeOfFadeOfMark = _constZero;
        }
        else
        {
            SoundInvulnerable();
        }
        if (_life <= _constZero)
        {
            _audioSource.Stop();
            _audioSource.clip = _deadSound;
            _audioSource.Play();
            _isDead = true;
            _life = _constZero;
            _lifeBarValue.text = (_life + "/" + _maxLife);
            Death();
        }
        else
        {
            if (_audioSource.clip != _damagedSound)
            {
                _audioSource.clip = _damagedSound;
            }
            if (_audioSource.isPlaying)
            {
                _audioSource.Stop();
            }
            _audioSource.clip = _damagedSound;
            _audioSource.Play();
        }
    }
    public void InFire(float _firedamage, float _firetime, bool _isinfire)
    {
        if (_isinfire)
        {
            _isInFire = _isinfire;
        }
        else
        {
            return;
        }
        _fireDamage = _firedamage;
        _fireTime = _firetime;
        _inFireTimer += Time.deltaTime;

        if (_inFireTimer >= _constOne)
        {
            _tickCount++;
            _life -= _firedamage;
            _lifeBarValue.text = (_life + "/" + _maxLife);
            if (_life <= _constZero)
            {
                _lifeBarValue.text = (_life + "/" + _maxLife);
                Death();
            }
            _lifeBar.fillAmount = _life / _maxLife;
            _inFireTimer = _constZero;
        }
        if (_tickCount >= _firetime)
        {
            if (_audioSource.clip == _fireSound && _audioSource.isPlaying)
            {
                _audioSource.Stop();
            }
            _isInFire = false;
            _fire.enabled = false;
            _inFireTimer = _constZero;
            _fireDamage = _constZero;
            _fireTime = _constZero;
            _tickCount = _constZero;
        }
    }
    public void StartFireDamageSound()
    {
        if (_fireSound != null)
        {
            _audioSource.clip = _fireSound;
            _audioSource.Play();
            _fire.enabled = true;
        }
    }
    public void SoundInvulnerable()
    {
        _audioSource.clip = _invulnerableHitSound;
        _audioSource.Play();
    }
    private void MarkApearAndFade()
    {
        if (!_markIsActive)
        {
            return;
        }
        _actualTimeOfFadeOfMark += Time.deltaTime;
        _bloodMark.color = new Color(_bloodMark.color.r, _bloodMark.color.g, _bloodMark.color.b, _constOne - _constOne / (_timeOfFadeOfMark - _actualTimeOfFadeOfMark));
        if (_actualTimeOfFadeOfMark >= _timeOfFadeOfMark)
        {
            _bloodMark.color = new Color(_bloodMark.color.r, _bloodMark.color.g, _bloodMark.color.b, _constZero);
            _actualTimeOfFadeOfMark = _constZero;
            _markIsActive = false;
        }
    }
    private void Death()
    {
        Cursor.lockState = CursorLockMode.None;
        _fader.SetTarget(_faderTrigger);
        _fader.SceneExit();
        _playerFunctions.enabled = false;
        this.enabled = false;
    }
    public void AddMaxLife(int amount)
    {
        _maxLife += amount;
        _life += amount;
        _lifeBarValue.text = (_life + "/" + _maxLife);
        _audioSource.Stop();
        _audioSource.clip = _maxLifeAdd;
        _audioSource.Play();
        Instantiate(_maxLifeUpParticle, _maxLifeUpParticlePosition.position, _maxLifeUpParticlePosition.rotation).transform.parent = _maxLifeUpParticlePosition;
    }
    public void AddLife(int lifeToAdd)
    {
        _audioSource.clip = _lifeAddAudioClip;
        _audioSource.Play();
        _life += lifeToAdd;
        if (_life >= _maxLife)
        {
            _life = _maxLife;
        }
        _lifeBar.fillAmount = _life / _maxLife;
        _lifeBarValue.text = (_life + "/" + _maxLife);
        StartCoroutine("lifeAdd");
    }
    public IEnumerator lifeAdd()
    {
        _particlesLife[0].GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(3);
        _particlesLife[0].GetComponent<ParticleSystem>().Stop();
    }
    public void BufferAttack(int wait)
    {
        _audioSource.Stop();
        _audioSource.clip = _buffAttack;
        _audioSource.Play();
        _playerFunctions._isBuff = true;
        StartCoroutine(buffAdd(wait));
    }
    public IEnumerator buffAdd(int num)
    {
        _particlesLife[2].GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(num);
        _particlesLife[2].GetComponent<ParticleSystem>().Stop();
        _playerFunctions._isBuff = false;
    }
    public void Invulnerability(int wait)
    {
        _isInvul = true;
        _audioSource.clip = _invulnerabilityAudioClip;
        _audioSource.Play();
        StartCoroutine(invulAction(wait));
    }
    public IEnumerator invulAction(int num)
    {
        _particlesLife[1].GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(num);
        _particlesLife[1].GetComponent<ParticleSystem>().Stop();
        _isInvul = false;
    }
}
