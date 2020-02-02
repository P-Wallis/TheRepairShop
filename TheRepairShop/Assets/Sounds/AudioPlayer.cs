using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// List of One-Shot Clips
/// ItemPick
/// ItemDrop
/// PointGained
/// 
/// 
/// 
/// 
/// 
/// 
/// List of Looped Clips
/// Ambient
/// Soundtrack
/// Footstep
/// Electric
/// Hammering
/// Carpenting
/// Painting
/// 
/// 
/// 
/// </summary>
public class AudioPlayer : MonoBehaviour
{


    public static AudioPlayer Instance => GameObject.Find("AudioPlayer").GetComponent<AudioPlayer>();
    [SerializeField] List<AudioClip> clips;
    AudioSource player;
    List<AudioSource> loopPlayers = new List<AudioSource>();
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        player = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Play a clip once. Continued effects are played through PlayAudioLoop().
    /// </summary>
    /// <param name="clipName"></param>
    public void PlayAudioOnce(string clipName) {
        player.PlayOneShot(clips.Find((x) => x.name == clipName));
        
    }

    /// <summary>
    /// Volume is between 0 and 1.
    /// </summary>
    /// <param name="clipName"></param>
    /// <param name="volume"></param>
    public void PlayAudioOnce(string clipName, float volume)
    {
        player.PlayOneShot(clips.Find((x) => x.name == clipName), volume);

    }
    /// <summary>
    /// Play the clip until StopAudioLoop() called. Clip Player ID is returned, which is required to stop playing this clip.
    /// </summary>
    /// <param name="clipName"></param>
    public int PlayAudioLoop(string clipName) {
        return PlayAudioLoop(clipName, 1f);
    }

    /// <summary>
    /// Volume is between 0 and 1.
    /// </summary>
    /// <param name="clipName"></param>
    /// <param name="volume"></param>
    /// <returns></returns>
    public int PlayAudioLoop(string clipName, float volume)
    {
        var source = gameObject.AddComponent<AudioSource>();
        source.loop = true;
        source.volume = volume;
        source.clip = clips.Find((x) => x.name == clipName);
        source.Play();
        loopPlayers.Add(source);
        return source.GetInstanceID();
    }

    public void StopAudioLoop(int ID) {
        var source  =loopPlayers.Find((x) => x.GetInstanceID() == ID);
        StartCoroutine(FadeOut(source, 0.5f));
        loopPlayers.Remove(source);
    }
    public void StopAudioLoop(string clipName) {
        foreach (var x in loopPlayers.FindAll((x) => x.clip.name == clipName)) {
            StartCoroutine(FadeOut(x, 0.5f));
            loopPlayers.Remove(x);
        }
    }
    private IEnumerator FadeOut(AudioSource source, float FadeTime) {

        float startVolume = source.volume;
        while (source.volume > 0)
        {
            source.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }
        source.Stop();
    }

}
