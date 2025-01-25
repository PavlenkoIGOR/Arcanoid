using SFML.Graphics;

namespace ArcanoidDLL.Config.Blocks;

public abstract class BlockData
{
    private protected Texture? _texture;

    public Sprite? sprite;
    public int hitTimes;
    public bool isDestroyable = false;
    public BlockData()
    {
        
    }
}
