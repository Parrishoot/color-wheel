using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioSourceExtensions
{
    public static void PlayIfNotPlaying(this AudioSource source, Vector2? pitchBounds = null) {
        
        if (source.isPlaying) {
            return;
        }

        float pitch = 1f;

        if(pitchBounds != null) {
            pitch = Random.Range(pitchBounds.Value.x, pitchBounds.Value.y);
        }
        
        source.pitch = pitch;
        source.Play();
    }
}