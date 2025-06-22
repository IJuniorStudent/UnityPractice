using UnityEngine;

public class ShuffleImageMover : BaseImageMover
{
    private const int MinSpritesCountToShuffle = 2;
    
    [SerializeField] private SpriteRenderer[] _renderers;
    
    private Sprite[] _sprites;
    private Sprite[] _spriteBuffer;
    
    protected override void Awake()
    {
        base.Awake();
        
        if (_renderers.Length < MinSpritesCountToShuffle)
            return;
        
        _sprites = new Sprite[_renderers.Length];
        _spriteBuffer = new Sprite[_renderers.Length - 1];
        
        for (int i = 0; i < _renderers.Length; i++)
            _sprites[i] = _renderers[i].sprite;
    }
    
    public override void ResetState()
    {
        ResetPosition();
        
        if (_renderers.Length < MinSpritesCountToShuffle)
            return;
        
        CollectSpritesWithoutLast();
        
        int spritesCount = _renderers.Length;
        int lastSpriteIndex = spritesCount - 1;
        
        for (int i = 0; i < lastSpriteIndex; i++)
            _renderers[i].sprite = _renderers[i + 1].sprite;
        
        _renderers[lastSpriteIndex].sprite = _spriteBuffer[Random.Range(0, lastSpriteIndex)];
    }
    
    private void CollectSpritesWithoutLast()
    {
        Sprite lastSprite =  _renderers[_renderers.Length - 1].sprite;
        int index = 0;
        
        foreach (var sprite in _sprites)
        {
            if (sprite == lastSprite)
                continue;
            
            _spriteBuffer[index] = sprite;
            index++;
        }
    }
}
