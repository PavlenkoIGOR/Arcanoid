using SFML.Graphics;

namespace ArcanoidDLL.Config.Blocks;

internal class YellowBlock : BlockData
{
    internal YellowBlock()
    {
        this._texture = new Texture(Path.Combine(Environment.CurrentDirectory, "GameResources", "YellowBlock.png"));
        this.sprite = new Sprite(_texture);
        this.hitTimes = 2;
        this.isDestroyable = true;
    }
}
