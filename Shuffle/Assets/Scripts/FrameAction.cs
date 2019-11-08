public class FrameAction
{
    public FrameAction()
    {
        Move = Move.None;
        Rotate = Rotate.None;
        Act = Act.None;
    }
    public FrameAction(Move move, Rotate rotate, Act act)
    {
        Move = move;
        Rotate = rotate;
        Act = act;
    }

    public Move Move { get; protected set; }
    public Rotate Rotate { get; protected set; }
    public Act Act { get; protected set; }

    public void SetMove(Move move)
    {
        Move = move;
    }

    public void SetRotate(Rotate rotate)
    {
        Rotate = rotate;
    }

    public void SetAct(Act act)
    {
        Act = act;
    }

    public override string ToString()
    {
        return $"Move: {Move.ToString()}, Rotate: {Rotate.ToString()}, Act: {Act.ToString()}";
    }
}

public enum Move
{
    None,
    Forward,
    Backward,
    Left,
    Right
}
public enum Rotate
{
    None,
    Left,
    Right
}
public enum Act
{
    None,
    Jump,
    Attack
}
