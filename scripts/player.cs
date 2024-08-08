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
		string state = "";
		if (IsOnFloor())
		{
			state = direction == 0 ? "idle" : "run";
		}
		else
		{
			state = Velocity.Y < 0 ? "jump" : "fall";
		}

		switch (state)
		{
			case "idle":
				_animatedSprite.Play("idle");
				break;
			case "run":
				_animatedSprite.Play("run");
				break;
			case "jump":
				_animatedSprite.Play("jump");
				break;
			case "fall":
				_animatedSprite.Play("fall");
				break;
			default:
				_animatedSprite.Play("idle");
				break;
		}
	}
}
