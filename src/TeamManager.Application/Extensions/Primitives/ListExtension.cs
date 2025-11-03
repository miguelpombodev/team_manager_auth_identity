namespace TeamManager.Application.Extensions.Primitives;

public static class ListExtension
{
    
    public static bool Empty<T>(this List<T> list, T item)
    {
        return list.Count != 0;
    }
}