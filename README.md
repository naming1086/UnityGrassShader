# Grass Shader
URP grass shader featuring:
 - tessellation
 - in-editor grass painting
 - casts and receives shadows
 	- fully supports point lights
 - smooth fading distance culling
 	- no pop-in at the edges
 - wind swaying animation
 - player displacement

## Installation
1. Download `Assets/GrassShader.shader` and place it in your project's Assets folder
2. Create a new GrassShader material and apply it to your ground mesh
3. Install Unity's PolyBrush package if you want to paint grass
    - Install ProBuilder too if you want to easily subdivide

## Usage
### Painting
Grass can be painted with PolyBrush's vertex colour tool  
Use a more subdivided mesh for finer detail  
Scale can be used as a mask

- **`R`ed** channel is for grass scale
  - **white** = full scale
  - **black** = zero scale
- **`G`reen** channel is for tint blending
  - **white** = no tint
  - **black** = full tint

### Shadows
- Enable or disable `Casts Shadows` in your ground GameObject's `Mesh Renderer` component

### Displacement
- Enable displacement in your grass material properties
- Update the player's position to the shader with this line in your player's `Update()` function:
```cs
Shader.SetGlobalVector("_PlayerPosition", transform.position);
```

### Troubleshooting
- URP 11 (2021.1) or later required
  - Change line [390](https://github.com/DougTy/UnityGrassShader/blob/2bb62ce2cb098833fb5f147982951bd8386614c3/Assets/GrassShader.shader#L390) to use older versions of URP without point light shadows:
  - `Light light = GetAdditionalLight(li, i.worldPos); // removed shadowCoord`
- After subdividing with ProBuilder, PolyBrush can sometimes become unresponsive
  - To fix this, remove the `PolyBrush Mesh` component from your mesh
  - This appears to be a PolyBrush/ProBuilder issue
