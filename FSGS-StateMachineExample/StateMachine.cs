using Godot;
using System;
using System.Collections.Generic;
using PLAYER;
using STATEMACHINE;

namespace STATEMACHINE
{
    public class StateMachine : Node
    {   
        public enum States{
            idle,jump,hurt,die,shoot
        }
        private string TRANSITION = null;
        public string transition{get{return TRANSITION;}set{TRANSITION = value;}}
        public string state = string.Empty;
        public string previous_state = string.Empty;
        protected Player player = new Player();
        public override void _Ready()
        {
            player = (Player)this.Owner;
            
        }
        public override void _PhysicsProcess(float delta)
        {

            if(state != null)
            {
                _state_logic(delta);
            }
            string transition = _transition(delta,TRANSITION);
    		if (transition != null){
    			set_state(transition);
            }
        }
        public virtual string _transition(float delta,string transition){return transition;}
        public void set_state(string new_state){
            previous_state = state;
            state = new_state;

            if (previous_state != null){
    		    _exit_state(previous_state, new_state);
            }
	        if( new_state != null){
	    	    _enter_state(new_state, previous_state);
            };
        }
        public virtual void _state_logic(float delta){}
        public void _enter_state(string new_state,string old_state){}
        public void _exit_state(string old_state,string new_state){}

    }
}
