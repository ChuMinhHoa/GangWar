using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : Singleton<ObjectPooling>
{
    [SerializeField] List<GameObject> objTemps = new List<GameObject>();
    [SerializeField] GameObject objTempPref;
    [SerializeField] Transform trsObjParents;

    public GameObject GetTemp()
    {
        for (int i = 0; i < objTemps.Count; i++)
        {
            if (!objTemps[i].activeSelf)
            {
                objTemps[i].SetActive(true);
                objTemps[i].transform.position = Vector3.zero;
                return objTemps[i];
            }
        }
        GameObject newObject = Instantiate(objTempPref, trsObjParents);
        objTemps.Add(newObject);
        return newObject;
    }

    [SerializeField] List<CannabisStaff> cannabisStaffs = new List<CannabisStaff>();
    [SerializeField] CannabisStaff cannabisStaff;
    [SerializeField] Transform trsCanabisStaff;

    public CannabisStaff GetCannabisStaff()
    {
        for (int i = 0; i < cannabisStaffs.Count; i++)
        {
            if (!cannabisStaffs[i].gameObject.activeSelf)
            {
                cannabisStaffs[i].gameObject.SetActive(true);
                return cannabisStaffs[i];
            }
        }
        CannabisStaff newObject = Instantiate(cannabisStaff, trsCanabisStaff);
        cannabisStaffs.Add(newObject);
        return newObject;
    }
}
