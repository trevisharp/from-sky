/* Author:  Leonardo Trevisan Silio
 * Date:    17/02/2024
 */
using System.Collections;
using System.Collections.Generic;

namespace FromSky;

public class Scene : Layer, IList<Mesh>
{
    List<Mesh> meshes = [];
    
    public override void Draw()
    {
        foreach (var mesh in meshes)
            mesh.Draw();
    }

    public int Count => meshes.Count;
    public bool IsReadOnly => false;

    public Mesh this[int index]
    {
        get => meshes[index];
        set => meshes[index] = value;
    }

    public int IndexOf(Mesh item)
        => meshes.IndexOf(item);

    public void Insert(int index, Mesh item)
        => meshes.Insert(index, item);

    public void RemoveAt(int index)
        => meshes.RemoveAt(index);

    public void Add(Mesh item)
        => meshes.Add(item);

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