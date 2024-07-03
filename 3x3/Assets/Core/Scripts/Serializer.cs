using UnityEngine;

public static class Serializer
{
    public static string DataModelToJson(DataModel model)
    {
        return JsonUtility.ToJson(model);
    }
    public static DataModel DataModelFromJson(string data)
    {
        return JsonUtility.FromJson<DataModel>(data);
    }
}
