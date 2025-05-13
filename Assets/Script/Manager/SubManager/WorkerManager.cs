using UnityEngine;
using System.Collections.Generic;

public class WorkerManager : Singleton<WorkerManager>   
{
    private List<InvadeObject> _invadeObjectList 
        = new List<InvadeObject>();

    public List<InvadeObject> InvadeObjectList => _invadeObjectList;

    public void RegisterInvadeObject(InvadeObject invadeObject)
    {
        if (!_invadeObjectList.Contains(invadeObject))
            _invadeObjectList.Add(invadeObject);
    }

    public void UnRegisterInvadeObject()
    {
        _invadeObjectList.Clear();
    }

    public void StartTracking(GameObject player)
    {
        foreach(var item in _invadeObjectList)
        {
            if (item == null)
                continue;

            item.OnSystemTracking(player.transform);
        }
    }

    public void CatchPlayer(InvadeObject invadeObject)
    {
        foreach(var item in _invadeObjectList)
        {
            if (!invadeObject.Equals(item))
            {
                item.SystemTracking = false;

                item.StopTarcking();
            }
        }

        invadeObject.SystemTracking = false;
    }
}
