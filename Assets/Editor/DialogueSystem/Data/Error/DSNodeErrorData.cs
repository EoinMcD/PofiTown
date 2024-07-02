using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DSNodeErrorData{

    public DSErrorData errorData { get; set; }

    public List<DS_Node> nodes { get; set;}

    public DSNodeErrorData() {
        
        errorData = new DSErrorData();
        nodes = new List<DS_Node>();

    }



}
