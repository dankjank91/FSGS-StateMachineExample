using Godot;
using System;
using PLAYER;
using STATEMACHINE;
using System.Collections.Generic;


namespace STATEMACHINE
{
    public class PlayerStateMachine : StateMachine
    {

        public override void _Ready()
        {
            player = (Player)this.Owner;
            GD.Print("Ready!");
            CallDeferred("set_state","idle");
        }
        public override void _state_logic(float delta)
        {
            player._inputs();
            player._jump();
            player._gravity();
        }
        public override string _transition(float delta,string transition){

            switch (state)
            {
                case "idle": 
                    if(player.onfloor == false && player.dead == false)
                        {transition = States.jump.ToString();} 
                    else if(player.dead)
                        {transition = States.die.ToString();}

                return transition;
                case "jump": 
                    if(player.onfloor == false && player.shooting == true)
                        {transition = States.shoot.ToString();}    
                    else if(player.onfloor){transition = States.idle.ToString();}
                    else if(player.dead){transition = States.die.ToString();}

                return transition;
                case "shoot": 
                    if(player.onfloor == false && player.shooting == true )
                        {GD.Print("PEW PEW");} 
                    if(player.onfloor == true)
                        {transition = States.idle.ToString();}
                    else if(player.dead){transition = States.die.ToString();}

                return transition;
                case "die": GetTree().ReloadCurrentScene();
                break;
            }
            return transition;
        }
    }
}
