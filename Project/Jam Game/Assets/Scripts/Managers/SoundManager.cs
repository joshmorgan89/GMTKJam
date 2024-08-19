using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviourSingleton<SoundManager>
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;

    [Header("Sound Clips")]
    public AudioClip asteroidBreaks;
    public AudioClip death1;
    public AudioClip death2;
    public AudioClip death3;
    public AudioClip death4;
    public AudioClip electionHasHappened;
    public AudioClip notice;
    public AudioClip pickUpRoom;
    public AudioClip pirateShipShoots;
    public AudioClip pirateShipShootsAlternate;
    public AudioClip placeRoom;
    public AudioClip playerShipThrusters;
    public AudioClip turretShots;
    public AudioClip uiMouseOver;
    public AudioClip warning;

    [Header("Background Music")]
    public AudioClip battleBGM;
    public AudioClip shopBGM;
    public AudioClip normalBGM;

    private float _sfxVolume = 1f;
    private float _musicVolume = 1f;
    public void PlaySound(string soundName)
    {
        switch (soundName)
        {
            case "AsteroidBreaks":
                PlaySFX(asteroidBreaks);
                break;
            case "Death1":
                PlaySFX(death1);
                break;
            case "Death2":
                PlaySFX(death2);
                break;
            case "Death3":
                PlaySFX(death3);
                break;
            case "Death4":
                PlaySFX(death4);
                break;
            case "ElectionHasHappened":
                PlaySFX(electionHasHappened);
                break;
            case "Notice":
                PlaySFX(notice);
                break;
            case "PickUpRoom":
                PlaySFX(pickUpRoom);
                break;
            case "PirateShipShoots":
                PlaySFX(pirateShipShoots);
                break;
            case "PirateShipShootsAlternate":
                PlaySFX(pirateShipShootsAlternate);
                break;
            case "PlaceRoom":
                PlaySFX(placeRoom);
                break;
            case "PlayerShipThrusters":
                PlaySFX(playerShipThrusters);
                break;
            case "TurretShots":
                PlaySFX(turretShots);
                break;
            case "UIMouseOver":
                PlaySFX(uiMouseOver);
                break;
            case "Warning":
                PlaySFX(warning);
                break;
            default:
                Debug.LogWarning("Sound not found: " + soundName);
                break;
        }
    }

    private void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip, _sfxVolume);
    }

    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.volume = _musicVolume;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlayBattleBGM()
    {
        PlayMusic(battleBGM);
    }

    public void PlayShopBGM()
    {
        PlayMusic(shopBGM);
    }

    public void PlayNormalBGM()
    {
        PlayMusic(normalBGM);
    }

    public void SetSFXVolume(float volume)
    {
        _sfxVolume = Mathf.Clamp01(volume);
        sfxSource.volume = _musicVolume;
    }

    public void SetMusicVolume(float volume)
    {
        _musicVolume = Mathf.Clamp01(volume);
        musicSource.volume = _musicVolume;
    }

    public float GetSFXVolume()
    {
        return _sfxVolume;
    }

    public float GetMusicVolume()
    {
        return _musicVolume;
    }
}
