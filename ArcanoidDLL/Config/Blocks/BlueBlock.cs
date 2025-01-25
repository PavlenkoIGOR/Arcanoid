using SFML.Graphics;

namespace ArcanoidDLL.Config.Blocks;

internal class BlueBlock : BlockData
{
    internal BlueBlock()
    {
        this._texture = new Texture(Path.Combine(Environment.CurrentDirectory, "GameResources", "BlueBlock.png"));
        this.sprite = new Sprite(_texture);
        this.hitTimes = 1;
        this.isDestroyable = true;
    }
}
