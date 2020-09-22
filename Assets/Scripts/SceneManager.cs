using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
	private Dictionary<string,BuffParams> buffs = new Dictionary<string,BuffParams>();

	public void Apply(
		PlayerController player,
		float speed,
		float jump,
		float duration)
	{
		buffs.Add(Guid.NewGuid().ToString(), new BuffParams
		{
			Duration = TimeSpan.FromSeconds((double)duration),
			Jump = jump,
			Player = player,
			Speed = speed
		});

		player.ApplyBuff(speed, jump);
	}

	private void Update()
	{
		var ended = new List<string>();
		foreach(var pair in buffs)
		{
			var buff = pair.Value;
			if (DateTime.Compare(DateTime.UtcNow, buff.Start.Add(buff.Duration)) < 0)
			{
				buff.Player.ApplyBuff(-buff.Speed, -buff.Jump);
				ended.Add(pair.Key);
			}
		}
		foreach(var key in ended)
		{
			buffs.Remove(key);
		}
	}
}

public class BuffParams
{
	public PlayerController Player { get; set; }
	public float Speed { get; set; }
	public float Jump { get; set; }
	public TimeSpan Duration { get; set; }
	public DateTime Start { get; set; } = DateTime.UtcNow;
	public bool Stopped { get; set; } = false;
}