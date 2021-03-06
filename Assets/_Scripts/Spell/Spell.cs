﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Spell : ScriptableObject {

	public enum DetailMode { SIMPLE, LEVELUP, DESC, FULL }
	public enum SpellType { SINGLE, HEAL, FAMILIAR, AREA, PASSIVE, DOT }

	[Header("Basic values")]
	public string spellName;
	public Sprite icon;
	public string description;

	[Header("Magic type")]
	public bool useable;
	public bool ultimate;
	[SerializeField] public SpellType spellType;
	[SerializeField] public ItemModifier[] buffModifiers;

	[Header("Base values")]
	[SerializeField] int damage;
	[SerializeField] int heal;
	[SerializeField] int buffValue;
	[SerializeField] int cooldown;
	[SerializeField] int manaCost;
	[SerializeField] int stuns;

	[Header("Scaling")]
	[SerializeField] int s_damage;
	[SerializeField] int s_heal;
	[SerializeField] int s_buffValue;
	[SerializeField] int s_cooldown;
	[SerializeField] int s_manaCost;
	[SerializeField] int s_stuns;


	public string generateSpellDescription(DetailMode mode, int level) {
		
		string fullDesc = description;
		if (ultimate)
			fullDesc = "[Ultimate]  " + fullDesc;

		switch (mode) {
			case DetailMode.SIMPLE:
				fullDesc = SetupDescriptionSimple(fullDesc, level);
				break;
			case DetailMode.LEVELUP:
				fullDesc = SetupDescriptionLevelup(fullDesc, level);
				break;
			case DetailMode.DESC:
				fullDesc = SetupDescriptionDesc(fullDesc);
				break;
			case DetailMode.FULL:
				fullDesc = SetupDescriptionFull(fullDesc, level);
				break;
		}

		return fullDesc;
	}

	string SetupDescriptionSimple(string fullDesc, int level) {
		level = Mathf.Max(1, level);
		int t_damage = damage + level * s_damage;
		int t_heal = heal + level * s_heal;
		int t_buffValue = buffValue + level * s_buffValue;
		int t_cooldown = cooldown + level * s_cooldown;
		int t_manaCost = manaCost + level * s_manaCost;
		int t_stun = stuns + level * s_stuns;

		fullDesc = fullDesc.Replace("<dmg>", SimpleValueString(t_damage));
		fullDesc = fullDesc.Replace("<heal>", SimpleValueString(t_heal));
		fullDesc = fullDesc.Replace("<buff>", SimpleValueString(t_buffValue));
		fullDesc = fullDesc.Replace("<cd>", SimpleValueString(t_cooldown));
		fullDesc = fullDesc.Replace("<cost>", SimpleValueString(t_manaCost));
		fullDesc = fullDesc.Replace("<stun>", SimpleValueString(t_stun));
		return fullDesc;
	}

	string SetupDescriptionLevelup(string fullDesc, int level) {
		if (level < 1) {
			return SetupDescriptionSimple(fullDesc, 1);
		}

		int t_damage = damage + level * s_damage;
		int t_heal = heal + level * s_heal;
		int t_buffValue = buffValue + level * s_buffValue;
		int t_cooldown = cooldown + level * s_cooldown;
		int t_manaCost = manaCost + level * s_manaCost;
		int t_stun = stuns + level * s_stuns;

		fullDesc = fullDesc.Replace("<dmg>", LevelupValueString(t_damage, s_damage));
		fullDesc = fullDesc.Replace("<heal>", LevelupValueString(t_heal, s_heal));
		fullDesc = fullDesc.Replace("<buff>", LevelupValueString(t_buffValue, s_buffValue));
		fullDesc = fullDesc.Replace("<cd>", LevelupValueString(t_cooldown, s_cooldown));
		fullDesc = fullDesc.Replace("<cost>", LevelupValueString(t_manaCost, s_manaCost));
		fullDesc = fullDesc.Replace("<stun>", LevelupValueString(t_stun, s_stuns));
		return fullDesc;
	}

	string SetupDescriptionDesc(string fullDesc) {
		fullDesc = fullDesc.Replace("<dmg>", DescValueString(damage, s_damage));
		fullDesc = fullDesc.Replace("<heal>", DescValueString(heal, s_heal));
		fullDesc = fullDesc.Replace("<buff>", DescValueString(buffValue, s_buffValue));
		fullDesc = fullDesc.Replace("<cd>", DescValueString(cooldown, s_cooldown));
		fullDesc = fullDesc.Replace("<cost>", DescValueString(manaCost, s_manaCost));
		fullDesc = fullDesc.Replace("<stun>", DescValueString(stuns, s_stuns));
		return fullDesc;
	}

	string SetupDescriptionFull(string fullDesc, int level) {
		level = Mathf.Max(1, level);

		int t_damage = damage + level * s_damage;
		int t_heal = heal + level * s_heal;
		int t_buffValue = buffValue + level * s_buffValue;
		int t_cooldown = cooldown + level * s_cooldown;
		int t_manaCost = manaCost + level * s_manaCost;
		int t_stuns = stuns + level * s_stuns;

		fullDesc = fullDesc.Replace("<dmg>", FullValueString(t_damage, damage, s_damage));
		fullDesc = fullDesc.Replace("<heal>", FullValueString(t_heal, heal, s_heal));
		fullDesc = fullDesc.Replace("<buff>", FullValueString(t_buffValue, buffValue, s_buffValue));
		fullDesc = fullDesc.Replace("<cd>", FullValueString(t_cooldown, cooldown, s_cooldown));
		fullDesc = fullDesc.Replace("<cost>", FullValueString(t_manaCost, manaCost, s_manaCost));
		fullDesc = fullDesc.Replace("<stun>", FullValueString(t_stuns, stuns, s_stuns));
		return fullDesc;
	}

	string SimpleValueString(int totalValue) {
		return string.Format("{0}", totalValue);
	}

	string LevelupValueString(int totalValue, int scaleValue) {
		return string.Format("{0} <color=#49BC53FF>(+{1})</color>", totalValue, scaleValue);
	}

	string DescValueString(int baseValue, int scaleValue) {
		return string.Format("<color=#A0C7D4FF>({0} + L*{1})</color>", baseValue, scaleValue);
	}

	string FullValueString(int totalValue, int baseValue, int scaleValue) {
		return string.Format("{0} <color=#A0C7D4FF>({1} + L*{2})</color>", totalValue, baseValue, scaleValue);
	}

	public bool CanLevelup(int playerLevel, int spellLevel) {
		if (ultimate) {
			return (6 + 3 * spellLevel <= playerLevel);
		}
		else {
			return (1 + 2 * spellLevel <= playerLevel);
		}
	}

	public int Damage(int level) { return damage + level * s_damage; }
	public int Heal(int level) { return heal + level * s_heal; }
	public int BuffValue(int level) { return buffValue + level * s_buffValue; }
	public int Cooldown(int level) { return cooldown + level * s_cooldown; }
	public int ManaCost(int level) { return manaCost + level * s_manaCost; }
	public int Stuns(int level) { return stuns + level * s_stuns; }

}
