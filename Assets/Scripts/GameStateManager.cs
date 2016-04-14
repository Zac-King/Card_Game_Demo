using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameStateManager : MonoBehaviour
{
    private static GameStateManager instance;
    private GameStateManager() { }
    public static GameStateManager Instance
    {
        get
        {
            if (instance == null)
                instance = new GameStateManager();
            return instance;
        }
    }

    void Awake()
    {
        foreach (GameStateManager gsm in FindObjectsOfType<GameStateManager>())
        {
            if (gsm != this)
                Destroy(gsm.gameObject);
        }

        DontDestroyOnLoad(gameObject);
        currentState = GAMESTATES.INIT;
        currentState = GAMESTATES.RUNNING;
    }

    static private GAMESTATES InitToRunning()
    {
        print("New Current State -> Running");
        
        return GAMESTATES.RUNNING;
    }
    static private GAMESTATES RunningToStart()
    {
        print("New Current State -> Start");

        return GAMESTATES.START;
    }
    static private GAMESTATES StartToCombat()
    {
        print("New Current State -> Combat");
        SceneManager.LoadScene("Combat");

        return GAMESTATES.COMBAT;
    }
    static private GAMESTATES CombatToPause()
    {
        print("New Current State -> Pause");

        return GAMESTATES.PAUSE;
    }
    static private GAMESTATES PauseToCombat()
    {
        print("New Current State -> Combat");
        SceneManager.LoadScene("Combat");

        return GAMESTATES.COMBAT;
    }
    static private GAMESTATES PauseToStart()
    {
        print("New Current State -> Start");
        SceneManager.LoadScene("Intro");

        return GAMESTATES.START;
    }
    static private GAMESTATES CombatToCredits()
    {
        print("New Current State -> Credits");
        SceneManager.LoadScene("Credits");

        return GAMESTATES.CREDITS;
    }
    static private GAMESTATES ToQuit()
    {
        print("New Current State -> Quit");
        Application.Quit();

        return GAMESTATES.QUIT;
    }
    static private GAMESTATES NoTransition()
    {
        print("No Change From " + currentState.ToString());
        return m_currentState;
    }

    public void ChangeStateTo(int a_stateIndex)
    {
        currentState = (GAMESTATES) a_stateIndex;
    }

    static public void ChangeStateTo(GAMESTATES a_state)
    {
        currentState = a_state;
    }

    public enum GAMESTATES
    {
        INIT    = 0,
        RUNNING = 1,
        START   = 2,
        COMBAT  = 3,
        PAUSE   = 4,
        CREDITS = 5,
        QUIT    = 6,
    }

    [SerializeField] static private GAMESTATES m_currentState;

    static private GAMESTATES currentState
    {
        get { return m_currentState; }
        set
        {
            switch (value)
            {
                case GAMESTATES.INIT:
                    break;

                case GAMESTATES.RUNNING:
                    switch(currentState)
                    {
                        case GAMESTATES.INIT:   m_currentState = InitToRunning();   break;
                        default:                m_currentState = NoTransition();    break;
                    }
                    break;

                case GAMESTATES.START:
                    switch(currentState)
                    {
                        case GAMESTATES.RUNNING:    m_currentState = RunningToStart();  break;
                        case GAMESTATES.PAUSE:      m_currentState = PauseToStart();    break;
                        default:                    m_currentState = NoTransition();    break;
                    }
                    break;

                case GAMESTATES.COMBAT:
                    switch(currentState)
                    {
                        case GAMESTATES.START:  m_currentState = StartToCombat();   break;
                        case GAMESTATES.PAUSE:  m_currentState = PauseToCombat();   break;
                        default:                m_currentState = NoTransition();    break;
                    }
                    break;

                case GAMESTATES.PAUSE:
                    switch(currentState)
                    {
                        case GAMESTATES.COMBAT: m_currentState = CombatToPause();   break;
                        default:                m_currentState = NoTransition();    break;
                    }
                    break;

                case GAMESTATES.CREDITS:
                    switch(currentState)
                    {
                        case GAMESTATES.COMBAT: m_currentState = CombatToCredits(); break;
                        default:                m_currentState = NoTransition();    break;
                    }
                    break;

                case GAMESTATES.QUIT:
                    switch(currentState)
                    {
                        case GAMESTATES.START:      m_currentState = ToQuit();          break;
                        case GAMESTATES.COMBAT:     m_currentState = ToQuit();          break;
                        case GAMESTATES.CREDITS:    m_currentState = ToQuit();          break;
                        default:                    m_currentState = NoTransition();    break;
                    }
                    break;

                default:
                    m_currentState = NoTransition();
                    break;
            }
        }
    }
}
