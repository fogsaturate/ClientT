using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Godot;


public partial class NoteRenderer : Renderer<Note>
{
    private MultiMeshInstance3D noteMesh { get; set; }

    private Color transparent = new Color(0x00000000);

    private Color white = new Color(0xffffffff);

    public override void _Ready()
    {
        noteMesh = new()
        {
            Multimesh = new()
            {
                UseColors = true,
                Mesh = new QuadMesh()
            }
        };
        AddChild(noteMesh);
    }

    private bool doProcess(Note note, float time, float approachTime)
    {
        return note.Millisecond - time >= 0 && note.Millisecond - time <= approachTime * 1000;
    }

    //public override void Dispose(Note note, int time, int songDelta)
    //{
    //    throw new System.NotImplementedException();
    //}

    public override void Process(double delta, Attempt attempt)
    {
        // TODO: Change settings to be have non-static fields
        //var notes = attempt.Notes;

        //float ar = attempt.Settings.ApproachRate;
        //float ad = attempt.Settings.ApproachDistance;
        //float at = attempt.Settings.ApproachTime;
        //float noteSize = attempt.Settings.NoteSize;
        //float fadeIn = attempt.Settings.FadeIn;
        //bool pushback = attempt.Settings.Pushback;
        //var transform = new Transform3D(new Vector3(noteSize / 2, 0, 0), new Vector3(0, noteSize / 2, 0), new Vector3(0, 0, noteSize / 2), Vector3.Zero);

        //if (notes.Count > noteMesh.Multimesh.InstanceCount)
        //{
        //    noteMesh.Multimesh.InstanceCount = notes.Count;
        //}
        //else
        //{
        //    // Saves rendering time by not clearing the buffer on the multimesh
        //    noteMesh.Multimesh.VisibleInstanceCount = notes.Count;
        //}



        //for (int i = 0; i < notes.Count; i++)
        //{
        //    var note = notes[i];

        //    if (!doProcess(note, (float)attempt.Progress, at) || note.Hit)
        //    {
        //        noteMesh.Multimesh.SetInstanceColor(i, transparent);
        //        continue;
        //    }

        //    float depth = (note.Millisecond - (float)attempt.Progress) / (1000 * at) / ad / attempt.Speed;
        //    float alpha = Math.Clamp((1 - depth / ad) / (fadeIn / 2), 0, 1);

        //    if (attempt.Mods.Contains("Ghost"))
        //    {
        //        alpha -= Math.Min(1, (ad - depth) / (ad / 2));
        //    }
        //    else if (attempt.Settings.FadeOut)
        //    {
        //        alpha -= (ad - depth) / (ad + (float)Constants.HIT_WINDOW * ar / 1000);
        //    }

        //    if (!pushback && note.Millisecond - attempt.Progress <= 0)
        //    {
        //        alpha = 0;
        //    }

        //    // TODO: Change this to user note colors
        //    var color = white;

        //    transform.Origin = new Vector3(note.X, note.Y, -depth);
        //    color.A = alpha;

        //    noteMesh.Multimesh.SetInstanceTransform(i, transform);
        //    noteMesh.Multimesh.SetInstanceColor(i, color);
        //}
    }
}
