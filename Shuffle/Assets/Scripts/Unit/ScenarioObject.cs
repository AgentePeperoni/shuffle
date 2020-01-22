using UnityEngine;

public class ScenarioObject : TimeObject
{
    [SerializeField]
    protected bool _loop;
    [SerializeField]
    protected Action[] _actions;
    
    public virtual void ChangeBehaviour(Action[] newActions, bool isLooped)
    {
        _actions = newActions;
        _loop = isLooped;

        for (int i = 0; i < _actions.Length; ++i)
            if (_actions[i].actionType != Actions.None)
                InsertAction(i, _actions[i]);
    }

    public override void SetCurrentFrame(int frame)
    {
        if (TimeObjectActions.Count <= frame)
        {
            if (_loop)
            {
                int index = TimeObjectActions.Count;
                for (int i = 0; i < _actions.Length; ++i)
                    InsertAction(index++, _actions[i]);
            }
            else
            {
                while (TimeObjectActions.Count <= frame)
                    TimeObjectActions.Add(new ObjectAction());
            }
        }

        ResetPosition();
        for (int i = 0; i < frame; ++i)
            ResolveAction(TimeObjectActions[i]);
    }

    protected virtual void Start()
    {
        ChangeBehaviour(_actions, _loop);
    }
}
