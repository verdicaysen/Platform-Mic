extends CharacterBody2D

#Establish our downward force.
@export var gravity = 400
#Establish our jump power.
@export var jump_power = 250
#Establish our movement speed.
@export var move_speed = 150
#Link to our Animated Sprite2D node.
@onready var animated_sprite = $AnimatedSprite2D

func _physics_process(delta):
	#Check if we're on the floor, and if not we apply the next line.
	if is_on_floor()==false:
		velocity.y += gravity * delta
		#Let's cap our falling velocity. Because crazy numbers bad.
		if velocity.y > gravity:
			velocity.y = gravity 
	#Jump as long as you are not already in the air.
	if Input.is_action_just_pressed("jump"):
		velocity.y = -jump_power
	
	#Get inputs and let's go somewhere.
	var direction = Input.get_axis("move_left", "move_right")
	#Flip animation direction if direction is -1 on the x axis.
	if direction != 0:
		animated_sprite.flip_h = direction == -1
	#Apply velocity direction with some speed declared above.
	velocity.x = direction * move_speed
	#Enable movement.
	move_and_slide()
	#Run to update animations as needed.
	update_animations(direction)
#Create that method/function to do all the animation things. "Animation Tree."
func update_animations(direction):
	if is_on_floor():
		if direction == 0:
			animated_sprite.play("idle")
		else:
			animated_sprite.play("run")
	else:
		if velocity.y < 0:
			animated_sprite.play("jump")
		else:
			animated_sprite.play("fall")
