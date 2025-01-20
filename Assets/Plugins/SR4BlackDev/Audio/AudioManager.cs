using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;

namespace SR4BlackDev
{
    public class AudioManager : MonoBehaviour
    {
        private static AudioManager _instance;
        [SerializeField]
        private AudioMixer _audioMixer;
        [SerializeField]
        private AudioMixerGroup _mixerGroupMusic;
        [SerializeField]
        private AudioMixerGroup _mixerGroupEffect;

        private List<AudioSource> _audioSourcesEffect = new List<AudioSource>();
        private List<AudioSource> _audioSourcesMusic = new List<AudioSource>();
        private List<AudioSource> _audioSourcesEffectById = new List<AudioSource>();
        private string _volumeMusic = "VolumeMusic";
        private string _volumeEffect = "VolumeEffect";
        private string _allLowPass = "AllLowPass";
        private string _musicLowPass = "MusicLowPass";
        private string _effectLowPass = "EffectLowPass";
        private Coroutine _lowPassAllCoroutine;
        private Coroutine _lowPassMusicCoroutine;
        private Coroutine _lowPassEffectCoroutine;
        private int _sizePoolAudioSourceEffect = 20;
        private int _sizePoolAudioSourceMusic = 2;
        
        
        private Dictionary<int ,AudioSource> _audioById = new Dictionary<int, AudioSource>();

        private bool isMuteEffect, isMuteMusic;
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(this);
                Init();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            CheckMute();
        }

        private void CheckMute()
        {
//            isMuteEffect = DataManager.GamePlayData.IsMuteEffect;
//            isMuteMusic = DataManager.GamePlayData.IsMuteMusic;
//            
            MuteEffect(isMuteEffect);
            MuteMusic(isMuteMusic);
        }

        private void Init()
        {
            int i = 0;
            while (i < _sizePoolAudioSourceEffect)
            {
                AudioSource temp = gameObject.AddComponent<AudioSource>();
                _audioSourcesEffect.Add(temp);
                i++;
            }

            int j = 0;
            while (j< _sizePoolAudioSourceMusic)
            {
                AudioSource temp = gameObject.AddComponent<AudioSource>();
                _audioSourcesMusic.Add(temp);
                j++;
            }
            int k = 0;
            while (k< _sizePoolAudioSourceEffect)
            {
                AudioSource temp = gameObject.AddComponent<AudioSource>();
                _audioSourcesEffectById.Add(temp);
                k++;
            }
            
        }
        
        private AudioSource GetSourceEffectFromPool(AudioClip clip = null)
        {
            if (clip != null)
            {
                for (int i = 0; i < _audioSourcesEffect.Count; i++)
                {
                    if (_audioSourcesEffect[i] != null 
                        && _audioSourcesEffect[i].clip != null 
                        && _audioSourcesEffect[i].isPlaying == false 
                        && _audioSourcesEffect[i].clip.name == clip.name )
                    {
                        return _audioSourcesEffect[i];
                    }
                }
            }

            foreach (AudioSource adSource in _audioSourcesEffect)
            {
                if (adSource != null && !adSource.isPlaying)
                    return adSource;
            }
        
            return _audioSourcesEffect[0];
        }

        private AudioSource GetSourceMusicFromPool(AudioClip clip = null)
        {
            if (clip != null)
            {
                for (int i = 0; i < _audioSourcesMusic.Count; i++)
                {
                    if (_audioSourcesMusic[i] != null && _audioSourcesMusic[i].clip != null &&
                        _audioSourcesMusic[i].clip.name == clip.name)
                    {
                        return _audioSourcesMusic[i];
                    }
                }
            }

            foreach (AudioSource adSource in _audioSourcesMusic)
            {
                if (adSource != null && !adSource.isPlaying)
                    return adSource;
            }
        
            return _audioSourcesMusic[0];
        }
        private AudioSource GetSourceEffectByIdFromPool(AudioClip clip = null)
        {
            if (clip != null)
            {
                for (int i = 0; i < _audioSourcesEffectById.Count; i++)
                {
                    if (_audioSourcesEffectById[i] != null 
                        && _audioSourcesEffectById[i].clip != null 
                        && _audioSourcesEffectById[i].isPlaying == false 
                        && _audioSourcesEffectById[i].clip.name == clip.name )
                    {
                        return _audioSourcesEffectById[i];
                    }
                }
            }

            foreach (AudioSource adSource in _audioSourcesEffectById)
            {
                if (adSource != null && !adSource.isPlaying)
                    return adSource;
            }
        
            return _audioSourcesEffectById[0];
        }
        private void Stop(AudioSource source)
        {
            if(source)
                source.Stop();
        }

        private void StopForFade(AudioSource source)
        {
            float currentVolume = source.volume;
            float targetVolume = 0;
            source.DOFade(targetVolume, .5f);
        }

        private AudioSource PlaySoundEffect(AudioClip audioClip, float volume =1f)
        {
            AudioSource source = GetSourceEffectFromPool(audioClip);
            if (audioClip == null) return source;
            
            source.outputAudioMixerGroup = _mixerGroupEffect;
            source.loop = false;
            source.clip = audioClip;
            source.volume = volume;
            source.Play();
            
            return source;
        }

        private AudioSource PlaySoundMusic(AudioClip audioClip, float volume =1f)
        {
            AudioSource source = GetSourceMusicFromPool(audioClip);
            if (audioClip == null) return source;

            source.outputAudioMixerGroup = _mixerGroupMusic;
            source.clip = audioClip;
            source.loop = true;
            source.volume = volume;
            source.Play();
            return source;
        }

        private AudioSource GetAudioSSourceWithPlaying(AudioClip audioClip)
        {
            for (int i = 0; i < _audioSourcesEffect.Count; i++)
            {
                if (_audioSourcesEffect[i].clip == audioClip && _audioSourcesEffect[i].isPlaying)
                {
                    return _audioSourcesEffect[i];
                }
            }

            return null;
        }
        
        private AudioSource PlaySoundEffect(AudioClip audioClip, bool loop, float volume =1f)
        {
            AudioSource source = GetSourceEffectFromPool(audioClip);
            if (audioClip == null) return source;
            
            source.outputAudioMixerGroup = _mixerGroupEffect;
            source.loop = loop;
            source.clip = audioClip;
            source.volume = volume;
            source.Play();
            
            return source;
        }

        private AudioSource PlaySoundEffectById(int id, AudioClip audioClip, float volume = 1f)
        {
            if (_audioById.ContainsKey(id))
            {
                AudioSource source = _audioById[id];
                source.outputAudioMixerGroup = _mixerGroupEffect;
                source.loop = false;
                source.clip = audioClip;
                source.volume = volume;
                source.Play();
                return source;
            }
            else
            {
                AudioSource source = GetSourceEffectByIdFromPool(audioClip);
                source.outputAudioMixerGroup = _mixerGroupEffect;
                source.loop = false;
                source.clip = audioClip;
                source.volume = volume;
                source.Play();
                _audioById.Add(id, source);
                return source;
            }
        }


        private void SetLowPassAll(float duration)
        {
            if (_lowPassAllCoroutine != null)
            {
                StopCoroutine(_lowPassAllCoroutine);
            }
            _lowPassAllCoroutine = StartCoroutine(IELowPassAll(duration));
        }
        private void SetLowPassEffect(float duration)
        {
            if (_lowPassEffectCoroutine != null)
            {
                StopCoroutine(_lowPassEffectCoroutine);
            }
            _lowPassEffectCoroutine = StartCoroutine(IELowPassEffect(duration));
        }
        private void SetLowPassMusic(float duration)
        {
            if (_lowPassMusicCoroutine != null)
            {
                StopCoroutine(_lowPassMusicCoroutine);
            }
            _lowPassMusicCoroutine = StartCoroutine(IELowPassMusic(duration));
        }

        IEnumerator IELowPassAll(float duration)
        {
            float timer = 0;
            WaitForFixedUpdate wait = new WaitForFixedUpdate();
            float valueLowPass = 15000f;
            float minLowPass = 1000f;
            float maxLowPass = 22000f;
            while (timer < duration)
            {
                timer += Time.deltaTime;

                valueLowPass = Mathf.Clamp(valueLowPass - 1000f, minLowPass, maxLowPass);
                
                _audioMixer.SetFloat(_allLowPass, valueLowPass);
                yield return wait;
            }

            float timerRestone = 0;
            while (timerRestone < 0.2f)
            {
                timerRestone += Time.deltaTime;
                valueLowPass = Mathf.Clamp(valueLowPass + 3000f, minLowPass, maxLowPass);
                _audioMixer.SetFloat(_allLowPass, valueLowPass);
                yield return wait;
            }
            _audioMixer.SetFloat(_allLowPass, maxLowPass);
        }
        
        IEnumerator IELowPassEffect(float duration)
        {
            float timer = 0;
            WaitForFixedUpdate wait = new WaitForFixedUpdate();
            float valueLowPass = 15000f;
            float minLowPass = 1000f;
            float maxLowPass = 22000f;
            while (timer < duration)
            {
                timer += Time.deltaTime;

                valueLowPass = Mathf.Clamp(valueLowPass - 1000f, minLowPass, maxLowPass);
                
                _audioMixer.SetFloat(_effectLowPass, valueLowPass);
                yield return wait;
            }

            float timerRestone = 0;
            while (timerRestone < 0.2f)
            {
                timerRestone += Time.deltaTime;
                valueLowPass = Mathf.Clamp(valueLowPass + 3000f, minLowPass, maxLowPass);
                _audioMixer.SetFloat(_effectLowPass, valueLowPass);
                yield return wait;
            }
            _audioMixer.SetFloat(_effectLowPass, maxLowPass);
        }
        IEnumerator IELowPassMusic(float duration)
        {
            float timer = 0;
            WaitForFixedUpdate wait = new WaitForFixedUpdate();
            float valueLowPass = 15000f;
            float minLowPass = 1000f;
            float maxLowPass = 22000f;
            while (timer < duration)
            {
                timer +=  Time.deltaTime;

                valueLowPass = Mathf.Clamp(valueLowPass - 1000f, minLowPass, maxLowPass);
                
                _audioMixer.SetFloat(_musicLowPass, valueLowPass);
                yield return wait;
            }

            float timerRestone = 0;
            while (timerRestone < 0.2f)
            {
                timerRestone +=  Time.deltaTime;
                valueLowPass = Mathf.Clamp(valueLowPass + 3000f, minLowPass, maxLowPass);
                _audioMixer.SetFloat(_musicLowPass, valueLowPass);
                yield return wait;
            }
            _audioMixer.SetFloat(_musicLowPass, maxLowPass);
        }
        private void MuteEffect(bool isMute)
        {
            if (isMute) 
                _audioMixer.SetFloat(_volumeEffect, -80f);
            else 
                _audioMixer.SetFloat(_volumeEffect, 0);
        }

        private void MuteMusic(bool isMute)
        {
            if (isMute) 
                _audioMixer.SetFloat(_volumeMusic, -80f);
            else 
                _audioMixer.SetFloat(_volumeMusic, 0);
        }

        //TODO Play Sound Effect
        public static AudioSource PlayEffect(AudioClip audioClip)
        {
            return _instance?.PlaySoundEffect(audioClip);
        }

        public static AudioSource PlayEffect(AudioClip audioClip, float volume)
        {
            return _instance?.PlaySoundEffect(audioClip, volume);
        }
        
        public static AudioSource PlayEffect(AudioClip audioClip, bool loop)
        {
            return _instance?.PlaySoundEffect(audioClip, loop);
        }

        public static AudioSource GetAudioSourcePlaying(AudioClip audioClip)
        {
            return _instance?.GetAudioSSourceWithPlaying(audioClip);
        }
        //TODO Play Music
        public static AudioSource PlayMusic(AudioClip audioClip)
        {
            return _instance?.PlaySoundMusic(audioClip);
        }

        public static AudioSource PlayMusic(AudioClip audioClip, float volume)
        {
            return _instance?.PlaySoundMusic(audioClip, volume);
        }

        public static void StopSound(AudioSource source)
        {
            _instance?.Stop(source);
        }

        public static void StopSoundForFade(AudioSource source)
        {
            _instance?.StopForFade(source);
        }
        
        public static void SetMuteEffect(bool isMute)
        {
            _instance?.MuteEffect(isMute);
        }

        public static void SetMuteMusic(bool isMute)
        {
            _instance?.MuteMusic(isMute);
        }


        public static void SetAllLowPass(float duration)
        {
            _instance?.SetLowPassAll(duration);
        }
        public static void SetEffectLowPass(float duration)
        {
            _instance?.SetLowPassEffect(duration);
        }
        public static void SetMusicLowPass(float duration)
        {
            _instance?.SetLowPassMusic(duration);
        }

        public static void PlayAudioById(int id, AudioClip clip)
        {
            _instance?.PlaySoundEffectById(id, clip);
        }
        
        
    }

}
