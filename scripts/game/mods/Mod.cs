using Godot;
using System;

public abstract class Mod : IMod
{
    public abstract string Name { get; }

    public virtual Type[] IncomptabileMods => [];
}
