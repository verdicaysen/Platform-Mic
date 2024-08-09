using Godot;
using System;

public partial class JumpPad : Area2D
{
    private AnimatedSprite2D _animatedSprite;
	//So basically you turned the parameter that is originally passed as boost in the player class into a local public float property called BoostAmount, get and set it to the default, and then expose it with export and call it in the OnBodyEntered method.
	[Export] public float BoostAmount { get; set; } = 1.5f;
	
	
    public override void _Ready()
    {
        // Connect the BodyEntered signal to the OnBodyEntered method.
        BodyEntered += OnBodyEntered;

        // Get the AnimatedSprite2D node.
        _animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }

    private void OnBodyEntered(Node2D body)
    {
        // Play the jump animation only if the class that touches it is the player.
        if (_animatedSprite != null && body is player Player)
        {
            _animatedSprite.Play("jump");
			//We have to do it this way because calling Player.Jump() directly won't work because the JumpPad isn't considered as being "OnFloor". Boost doesn't have such a check.
			Player.Boost(BoostAmount); 
        }
    }

    public override void _Process(double delta)
    {
    }
}
