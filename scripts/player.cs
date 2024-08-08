using Godot;
using System;

public partial class player : CharacterBody2D
{
	// Exported variables
	[Export] public float gravity = 400;
	[Export] public float jumpPower = 250;
	[Export] public float moveSpeed = 150;

	// Link to AnimatedSprite2D node
	private AnimatedSprite2D _animatedSprite;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//Build the reference and the nget the node for the AnimatedSprite2D node to call the builtin functions and fields.
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	// Called every frame.
	public override void _PhysicsProcess(double delta)
	{
		// Apply gravity if not on the floor.
		if (!IsOnFloor())
		{
			Velocity = new Vector2(Velocity.X, Velocity.Y + (float)(gravity * delta));
			if (Velocity.Y > gravity)
			{
				Velocity = new Vector2(Velocity.X, gravity);
			}
		}

		// Jump if not already in the air.
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			Velocity = new Vector2(Velocity.X, -jumpPower);
		}

		// Get movement direction.
		float direction = Input.GetAxis("move_left", "move_right");

		// Flip animation direction if necessary.
		if (direction != 0)
		{
			_animatedSprite.FlipH = direction == -1;
		}

		// Apply movement direction and speed.
		Velocity = new Vector2(direction * moveSpeed, Velocity.Y);
		// Enable movement.
		MoveAndSlide();
		// Update animations.
		UpdateAnimations(direction);
	}

	// Method to update animations based on character state.
	private void UpdateAnimations(float direction)
{
	// Determine the state using a ternary operator and switch expression.
	string state = IsOnFloor()
		? direction == 0 ? "idle" : "run"
		: Velocity.Y < 0 ? "jump" : "fall";

	// Use a switch expression to determine the animation to play.
	_animatedSprite.Play(state switch
	{
		"idle" => "idle",
		"run" => "run",
		"jump" => "jump",
		"fall" => "fall",
		_ => "idle" // Default case if none of the above matches.
		});
	}
}
