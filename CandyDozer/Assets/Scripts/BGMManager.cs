using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public CandyManager candymanager;
    public AudioSource[] BGMS;
    public int i;
    public bool flg = false;

    // Start is called before the first frame update
    void Start()
    {
        BGMS = GetComponents<AudioSource>();
        BGMS[i].Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(candymanager.score != 0 &&
            candymanager.score % 500 == 0 &&
            flg == false &&
            BGMS.Length > i)
        {
            candymanager.score += 10;
            BGMS[i].Stop();
            BGMS[0].Play();
            flg = true;
            StartCoroutine(MusicBGM());
        }

    }

    IEnumerator MusicBGM()
    {
        yield return new WaitForSeconds(11.2f);
        flg = false;
        i++;
        BGMS[i].Play();
    }
}
