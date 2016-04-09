﻿#pragma warning disable 0168
#pragma warning disable 0219
#pragma warning disable 0414

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using Eric; 

namespace Dylan
{
    public class TurnManager : MonoBehaviour
    {
        [SerializeField]
        private UnityEngine.UI.Text TurnLabel;
        private List<Player> Players; //All players in the current game
        private int m_CurrentPlayerIndex; //index of the current player
        [SerializeField]
        private Player ActivePlayer; //Current player taking his/her turn
        
        /// <Testing>
        //public Text cPlayer;
        //public Text cPhase;
        /// </Testing>

        private enum TurnPhases
        {
            firstPhase,
            secondPhase,
            combatPhase,
            endPhase
        }
        [SerializeField]
        private TurnPhases currentPhase = TurnPhases.firstPhase; //Current turnPhase the player is in
		void Awake()
		{
			for(int i = 0; i < 8; i++)
			{
//				GameObject t = Resources.Load("TreasureCardTemplate") as GameObject;
//				GameObject m = Resources.Load("MysteryCardTemplate") as GameObject;
//				TreasureCard tc = t.GetComponent<TreasureCardMono>().theCard;
//				TreasureStack.
//				MysteryCard mc = m.GetComponent<MysteryCardMono>().theCard;
//				MysteryStack.Push(mc);

			}

		}
        void Start()
        {


	
            Players = new List<Player>();
            Players.AddRange(FindObjectsOfType<Player>());
            PlayerCycle();
		}

         /// <summary>
         /// Cycles from one player to the next
         /// </summary>
        void PlayerCycle()
        {            
            ActivePlayer = Players[m_CurrentPlayerIndex];
           // float x = ActivePlayer.transform.position.x;
//            float z = ActivePlayer.transform.position.x;
           // Camera.main.transform.position = new Vector3(x, 33, z);
            //Camera.main.transform.LookAt(Vector3.zero);
            //cPlayer.text = ActivePlayer.name;
            if (m_CurrentPlayerIndex >= 3)
                m_CurrentPlayerIndex = 0;
            else
                m_CurrentPlayerIndex++;
        }

        void Update()
        {
            ///<Testing>
            if (Input.GetKeyDown(KeyCode.D))
            {
                PhaseTransition();
                TurnLabel.text = currentPhase.ToString();
                //cPhase.text = currentPhase.ToString();
            }
            /// </Testing>
        }

        /// <summary>
        /// Handles the transitions from one phase to another
        /// as the Active player takes his/her turn
        /// </summary>
        void PhaseTransition()
        {
            switch(currentPhase)
            {
                case TurnPhases.firstPhase:
                    currentPhase = TurnPhases.secondPhase;
                    break;
                case TurnPhases.secondPhase:
                    currentPhase = TurnPhases.combatPhase;
                    break;
                case TurnPhases.combatPhase:
                    currentPhase = TurnPhases.endPhase;
                    break;
                case TurnPhases.endPhase:
                    currentPhase = TurnPhases.firstPhase;
                    PlayerCycle();
                    break;
            }
        }
    }
}

