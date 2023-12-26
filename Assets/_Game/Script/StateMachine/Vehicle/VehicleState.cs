
public class VGlobleState : State<Vehicle>
{
    protected static VGlobleState m_instance;
    public static VGlobleState Instance
    {
        get
        {
            if (m_instance == null) m_instance = new VGlobleState();
            return m_instance;
        }
    }

    public override void Enter(Vehicle go)
    {

    }

    public override void Execute(Vehicle go)
    {

    }

    public override void Exit(Vehicle go)
    {

    }
}

public class VIdleState : State<Vehicle>
{
    protected static VIdleState m_instance;
    public static VIdleState Instance
    {
        get
        {
            if (m_instance == null) m_instance = new VIdleState();
            return m_instance;
        }
    }

    public override void Enter(Vehicle go)
    {
        go.IdleEnter();
    }

    public override void Execute(Vehicle go)
    {
        go.IdleExecute();
    }

    public override void Exit(Vehicle go)
    {
        go.IdleExit();
    }
}

public class VMoveState : State<Vehicle>
{
    protected static VMoveState m_instance;
    public static VMoveState Instance
    {
        get
        {
            if (m_instance == null) m_instance = new VMoveState();
            return m_instance;
        }
    }

    public override void Enter(Vehicle go)
    {
        go.MoveEnter();
    }

    public override void Execute(Vehicle go)
    {
        go.MoveExecute();
    }

    public override void Exit(Vehicle go)
    {
        go.MoveExit();
    }
}
