module Affogato.Color

/// 0.0f ~ 1.0f
let inline HSV2RGBFloat32 (hsv : float32 Vector3) : float32 Vector3 =
  let { Vector3.x=h; y=s; z=v } = hsv
  let r, g, b = v, v, v

  let h = h * 6.0f
  let i = int h
  let f = h - float32 i
  let r, g, b =
    i |> function
    | 1 ->
      ( 1.0f - s * f,
        g,
        1.0f - s
      )
    | 2 ->
      ( 1.0f - s,
        g,
        1.0f - s * (1.0f - f)
      )
    | 3 ->
      ( 1.0f - s,
        1.0f - s * f,
        b
      )
    | 4 ->
      ( 1.0f - s * (1.0f - f),
        1.0f - s,
        b
      )
    | 5 ->
      ( r,
        1.0f - s,
        1.0f - s * f
      )
    | _ ->
      ( r,
        1.0f - s * (1.0f - f),
        1.0f - s
      )

  Vector3.init r g b

/// 0 ~ 255
let inline HSV2RGBByte (hsv : byte Vector3) : byte Vector3 =
  hsv
  |>> (float32 >> devidedBy 255.0f)
  |> HSV2RGBFloat32
  |>> ((*) 255.0f >> byte)
