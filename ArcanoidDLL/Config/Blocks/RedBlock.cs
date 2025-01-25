using SFML.Graphics;

namespace ArcanoidDLL.Config.Blocks;

internal class RedBlock : BlockData
{
    internal RedBlock()
    {
        this._texture = new Texture(Path.Combine(Environment.CurrentDirectory, "GameResources", "RedBlock.png"));
        this.sprite = new Sprite(_texture);
        this.hitTimes = 3;
        this.isDestroyable = true;
    }
}
