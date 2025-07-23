using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceContainer : EntityContainer<CollectableResource>
{
    private Dictionary<ResourceBase, HashSet<CollectableResource>> _resourcesMap = new();
    private Dictionary<CollectableResource, ResourceBase> _resourceOwnerMap = new();
    
    public void TryRegister(List<CollectableResource> resources, ResourceBase owner)
    {
        foreach (CollectableResource resource in resources)
        {
            if (Has(resource) == false)
                Register(resource);
            
            if (IsFree(resource) == false)
                continue;
            
            SetOwner(resource, owner);
        }
    }
    
    public override bool TryFree(CollectableResource resource)
    {
        bool isFreed = base.TryFree(resource);
        
        if (_resourceOwnerMap.Remove(resource, out ResourceBase owner) == false)
            return isFreed;
        
        if (_resourcesMap.TryGetValue(owner, out HashSet<CollectableResource> resources))
            resources.Remove(resource);
        
        return isFreed;
    }
    
    public bool TryGetSorted(ResourceBase owner, int maxSelectCount, out List<CollectableResource> targetResources)
    {
        targetResources = null;
        
        if (_resourcesMap.TryGetValue(owner, out HashSet<CollectableResource> ownedResources) == false)
            return false;
        
        if (ownedResources.Count == 0 || maxSelectCount <= 0)
            return false;

        List<CollectableResource> sortedResources = GetFreeSorted(owner.gameObject.transform.position, ownedResources);
        
        int selectCount = Math.Min(sortedResources.Count, maxSelectCount);
        targetResources = new List<CollectableResource>(selectCount);
        
        for (int i = 0; i < selectCount; i++)
            targetResources.Add(sortedResources[i]);
        
        return true;
    }
    
    private void SetOwner(CollectableResource resource, ResourceBase owner)
    {
        if (_resourceOwnerMap.ContainsKey(resource))
            return;
        
        if (_resourcesMap.TryGetValue(owner, out HashSet<CollectableResource> resources) == false)
        {
            resources = new HashSet<CollectableResource>();
            _resourcesMap[owner] = resources;
        }
        
        resources.Add(resource);
        _resourceOwnerMap.Add(resource, owner);
    }
    
    private List<CollectableResource> GetFreeSorted(Vector3 center, HashSet<CollectableResource> ownedResources)
    {
        var sortedResources = new List<CollectableResource>();
        
        foreach (CollectableResource resource in ownedResources)
            if (IsFree(resource))
                sortedResources.Add(resource);
        
        sortedResources.Sort((resourceA, resourceB) =>
            (center - resourceA.transform.position).sqrMagnitude.CompareTo(
                (center - resourceB.transform.position).sqrMagnitude)
        );
        
        return sortedResources;
    }
}
