/* Author:  Leonardo Trevisan Silio
 * Date:    17/02/2024
 */
using System;
using System.Collections;
using System.Collections.Generic;

namespace FromSky;

using static Radiance.Utils;

public class Scene : Layer, IList<Mesh>
{
    List<Mesh> meshes = [];
    dynamic mainRender;

    public Scene()
    {
        updateScene();

        mainRender = render(() => {
            clear((0, 0, .4f, 1f));
        });
    }

    public override void Draw()
    {
        mainRender(Empty);
        foreach (var mesh in meshes)
            mesh.Draw();
    }

    const float a0 = 35.26f * MathF.PI / 180f;
    const float b0 = 45f * MathF.PI / 180f;

    private float a = a0;
    private float b = b0;
    private float cosa = float.NaN;
    private float cosb = float.NaN;
    private float sina = float.NaN;
    private float sinb = float.NaN;
    private float sinbsina = float.NaN;
    private float cosbsina = float.NaN;

    private void updateScene()
    {
        cosa = MathF.Cos(a);
        cosb = MathF.Cos(b);
        sina = MathF.Sin(a);
        sinb = MathF.Sin(b);
        sinbsina = sinb * sina;
        cosbsina = cosb * sina;
        foreach (var mesh in meshes)
            mesh.SetCamTransform(camTransform);
    }

    private (float x, float y) camTransform(float x, float y, float z)
        => (
            x * cosb - z * sinb,
            y * cosa + x * sinbsina + z * cosbsina
        );

    public int Count => meshes.Count;

    public bool IsReadOnly => false;

    public Mesh this[int index]
    {
        get => meshes[index];
        set
        {
            meshes[index] = value;
            value.SetCamTransform(camTransform);
        }
    }

    public int IndexOf(Mesh item)
        => meshes.IndexOf(item);

    public void Insert(int index, Mesh item)
    {
        meshes.Insert(index, item);
        item.SetCamTransform(camTransform);
    }

    public void RemoveAt(int index)
        => meshes.RemoveAt(index);

    public void Add(Mesh item)
    {
        meshes.Add(item);
        item.SetCamTransform(camTransform);
    }

    public void Clear()
        => meshes.Clear();

    public bool Contains(Mesh item)
        => meshes.Contains(item);

    public void CopyTo(Mesh[] array, int arrayIndex)
        => meshes.CopyTo(array, arrayIndex);

    public bool Remove(Mesh item)
        => meshes.Remove(item);

    public IEnumerator<Mesh> GetEnumerator()
        => meshes.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => meshes.GetEnumerator();
}