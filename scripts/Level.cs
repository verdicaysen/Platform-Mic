using Godot;
using System;

public partial class Level : Node2D

{
    // Instance a version of the Marker2D StartPosition.
    private Marker2D myStartPosition;

    public override void _Ready()
    {
        //Welcome to how to connect Signals in C# 101. You're not stupid anymore!
        TrashCan myTrashCan = GetNode<TrashCan>("TrashCan");
        //Yes, the += is necessary. We're saying hey node we just referenced, use the signal we connected and then add it to the method OnTrashCanBodyEntered.
        myTrashCan.BodyEntered += OnTrashCanBodyEntered;
        myStartPosition= GetNode<Marker2D>("StartPosition");
    }

    //Here we're using the signal with the method we already previously declared to the signal handler.
     private void OnTrashCanBodyEntered(Node2D body)
    {
        // If the body is a CharacterBody2D, reset its velocity and position as directed.
        if (body is CharacterBody2D characterBody)
        {
            // Reset the velocity to zero.
            characterBody.Velocity = Vector2.Zero;

            // Reset the characters position to these coordinates.
            characterBody.GlobalPosition = myStartPosition.GlobalPosition;
        }
    }

}


 