using UnityEngine;
using UnityEngine.UI;

public class RadialButton : MonoBehaviour
{
    public RadialMenu Parent
    {
        get { return _parent; }
        set
        {
            if (_parent == null)
                _parent = value;
        }
    }

    public ActionType type;
    public Move move;
    public Rotate rotate;
    public Act act;
    
    private Button _assignedButton;
    private RadialMenu _parent;

    private void Start()
    {
        if (_assignedButton == null)
            _assignedButton = GetComponent<Button>();

        _assignedButton.onClick.AddListener(SendChosenAction);
    }

    private void SendChosenAction()
    {
        _parent.SendActionToController(type, move, rotate, act);
    }
}
