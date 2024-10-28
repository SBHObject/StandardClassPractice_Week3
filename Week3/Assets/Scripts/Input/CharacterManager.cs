using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//역할 : 싱글톤 패턴을 사용하여 현재 플레이어가 조종하는 캐릭터의 정보에 전역 접근 지점을 마련함
public class CharacterManager : MonoBehaviour
{
    private static CharacterManager instance;
    public static CharacterManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new GameObject("CharacterManager").AddComponent<CharacterManager>();
            }
            return instance;
        }
    }

    public Player Player
    {
        get { return _player; }
        set { _player = value; }
    }

    private Player _player;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if(instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
