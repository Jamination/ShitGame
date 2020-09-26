using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShitGame.GUI
{
    public class UI_Window
    {
        public VertexPositionColor[] Verts = new VertexPositionColor[4]
        {
            new VertexPositionColor(new Vector3(0, 0, 1), Color.White),
            new VertexPositionColor(new Vector3(10, 0, 1), Color.White),
            new VertexPositionColor(new Vector3(10, 10, 1), Color.White),
            new VertexPositionColor(new Vector3(0, 10, 1), Color.White)
        };
    }
}