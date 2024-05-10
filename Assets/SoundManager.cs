using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField]
    private AudioSource pieceMovementAudioSource;

    [SerializeField]
    private AudioSource rowSpawnedAudioSource;

    [SerializeField]
    private AudioSource clusterPoppedAudioSource;

    [SerializeField]
    private AudioSource cashInAudioSource;

    [SerializeField]
    private AudioSource gameOverAudioSource;


    private PiecesManager piecesManager;

    void Start() {
        
        piecesManager = PiecesManager.Instance;
        
        piecesManager.PieceSpawned += (pieceManager) => { rowSpawnedAudioSource?.PlayIfNotPlaying(new Vector2(1.2f, 1.8f)); };
        piecesManager.PieceMoved += () => { pieceMovementAudioSource?.PlayWithPitchVariance(new Vector2(.9f, 1.1f)); };

        GameManager.Instance.GameOver += () => gameOverAudioSource.Play();
    }

    public void PlayClusterPoppped(int multiplier) {

        if(clusterPoppedAudioSource.isPlaying) {
            clusterPoppedAudioSource.Stop();
        }

        clusterPoppedAudioSource.pitch = 1f + (.2f * (multiplier - 1));
        clusterPoppedAudioSource.Play();
    }

    public void PlayCashIn() {
        cashInAudioSource.PlayIfNotPlaying(new Vector2(1f, 1.3f));
    }
}


