using UnityEngine;

[CreateAssetMenu(fileName = "TargetInfo", menuName = "New Target")]
public class TargetInfo : ScriptableObject
{
	[field: SerializeField]
	public string Name { get; private set; }
	[field: SerializeField]
	public int Score { get; private set; }
	[field: SerializeField, Multiline]
	public string Description { get; private set; }
}