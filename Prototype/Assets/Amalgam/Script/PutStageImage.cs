using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PutStageImage : MonoBehaviour
{
    [SerializeField]
    private Sprite[] StagePhotos;

    [SerializeField]
    private Image StageImage;

    [SerializeField]
    public int Index { get; set; }

    private int OldIndex;

    // Start is called before the first frame update
    void Start()
    {
        StageImage = GetComponent<Image>();
        Index = OldIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(OldIndex != Index)
        {
            if(Index > StagePhotos.Length)
            {
                Index = OldIndex;
            }

            StageImage.sprite = StagePhotos[Index];
            OldIndex = Index;
        }
    }
}
