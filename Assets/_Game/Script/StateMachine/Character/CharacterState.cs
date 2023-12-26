
public class CGlobalState : State<Character>
{
    private static CGlobalState m_instance;
    public static CGlobalState Instance
    {
        get
        {
            if (m_instance == null) m_instance = new CGlobalState();
            return m_instance;
        }
    }

    public override void Enter(Character go)
    {

    }

    public override void Execute(Character go)
    {

    }

    public override void Exit(Character go)
    {

    }
}

public class CIdleState : State<Character>
{
    private static CIdleState m_instance;
    public static CIdleState Instance {
        get { 
            if (m_instance == null) m_instance= new CIdleState(); 
            return m_instance;
        }
    }

    public override void Enter(Character go)
    {
        go.IdleEnter();
    }

    public override void Execute(Character go)
    {
        go.IdleExecute();
    }

    public override void Exit(Character go)
    {
        go.IdleEnd();
    }
}

public class CMoveState : State<Character>
{
    private static CMoveState m_instance;
    public static CMoveState Instance
    {
        get
        {
            if (m_instance == null) m_instance = new CMoveState();
            return m_instance;
        }
    }

    public override void Enter(Character go)
    {
        go.MoveEnter();
    }

    public override void Execute(Character go)
    {
        go.MoveExecute();
    }

    public override void Exit(Character go)
    {
        go.MoveEnd();
    }
}
