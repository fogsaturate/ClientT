extends Node3D

@onready var tileMaterial: StandardMaterial3D = $"Top".mesh.material
@onready var environment := $"WorldEnvironment"

var color := Color(1, 1, 1)

func _process(delta: float) -> void:
	# lerp to last note hit color after runner rewrite
	color = color.lerp(Color(1, 1, 1), delta * 8)
	
	tileMaterial.albedo_color = color
	tileMaterial.uv1_offset += Vector3.UP * delta * 3
