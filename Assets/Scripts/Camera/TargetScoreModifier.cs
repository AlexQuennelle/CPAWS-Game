using UnityEngine;

/// <summary>
///   Base class for components that act as modifiers on the score value a
///   <see cref="CameraTarget"/> contributes to a picture.
/// </summary>
public abstract class TargetScoreModifier : MonoBehaviour
{
	/// <summary>
	///   Whether the value returned by
	///   <see cref="GetValue(Camera, Transform, Rect)"/> should be interpreted
	///   as a multiplier. If false, value should be interpreted as additive.
	/// </summary>
	[field: SerializeField]
	public bool IsMultiplier { get; private set; }

	/// <summary>
	///   Calculates the modifier's value.
	/// </summary>
	/// <param name="cam">
	///   Camera whose data should be used when calculating the modifier value.
	///   This should be the same camera whose output is used as the final
	///   picture.
	/// </param> 
	/// <param name="camTransform">
	///   The transform component of the object <paramref name="cam"/> is
	///   attached to. Used for calculating things related to positioning.
	/// </param>
	/// <param name="bounds">
	///   The 2D bounding box in screenspace of the target the modifier is
	///   attached to.
	/// </param>
	/// <returns>
	///   The calculated modifier value. How this value should be interpreted
	///   is determined by the value of <see cref="IsMultiplier"/>.
	/// </returns>
	public abstract float GetValue(Camera cam, Transform camTransform, Rect bounds);
}