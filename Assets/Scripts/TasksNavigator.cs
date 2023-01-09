using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TasksNavigator : MonoBehaviour
{

    public GameObject MessageBox;
    public GameObject NextButton;

    private UnityEngine.UI.Text _text;
    private int _counter;

    // Start is called before the first frame update
    void Start()
    {
        _text = MessageBox.GetComponent<UnityEngine.UI.Text>();
        _text.text = Messages.Intro;
        _counter = 0;
    }


    public void SwitchIntroText() 
    {
        switch (_counter)
        {
            case 0:
                _text.text = Messages.Intro;
                break;
            case 1:
                _text.text = Messages.Instructions1;
                break;
            case 2:
                _text.text = Messages.Instructions2;
                break;
            case 3:
                _text.text = Messages.TasksAboutToStart;
                // TODO: Spawn Tasks next
                break;
            default:
                _counter = 0;
                break;
        }
        _counter++;
    }

}
