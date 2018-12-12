using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CRYSTAL;

namespace IceFalls {

    public class script_IcicleSpawnerControl : CRYSTAL_Script {

        // Public

        //public GameObject PrefabIcicleSpawner;
       
        //public float IceSpawnRegionXAxisMin = -7;

        //public float IceSpawnRegionXAxisMax = 7;

        //// Private
        
        //[SerializeField]
        //private List<script_IcicleSpawner> p_GeneratedIcicleSpawners;

        //private int p_TotalSegments = 5;

        //private float p_SegmentWidth;

        // Properties

        //public int TotalSegments {
        //    get {
        //        return this.p_TotalSegments;
        //    }
        //}

        // Public Methods

        //public override void SetDefaultValues() {
        //    base.SetDefaultValues();
            
        //    this.p_GeneratedIcicleSpawners = new List<script_IcicleSpawner>();
            
        //    this.GenerateIceSpawners();
        //}

        //public void GenerateIceSpawners() {

        //    this.p_SegmentWidth = (this.IceSpawnRegionXAxisMax - this.IceSpawnRegionXAxisMin) / (this.TotalSegments + 1);

        //    // Cleanup
        //    RemoveAllIceSpawners();

        //    // Create new spawners
        //    int i, n = this.TotalSegments;
        //    for (i = 0; i < n; ++i) {
        //        this.SpawnIceSpawner(i);
        //    }
        //}

        //// Private Methods

        //private void RemoveAllIceSpawners() {
        //    while (this.transform.childCount > 0) {
        //        Destroy(this.transform.GetChild(0));
        //    }
        //}

        //private void SpawnIceSpawner(int _Index) {
        //    Vector3 spawnerPosition = new Vector3(this.XPositionAtIndex(_Index), 6, 0);
        //    GameObject spawnerClone = GO.Clone(this.PrefabIcicleSpawner, spawnerPosition);
        //    spawnerClone.transform.parent = this.transform;
        //    this.p_GeneratedIcicleSpawners.Add(spawnerClone.GetComponent<script_IcicleSpawner>());
        //}

        //private float XPositionAtIndex(int _Index) {
        //    return this.IceSpawnRegionXAxisMin + this.p_SegmentWidth * _Index;
        //}
    }
}