﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HexGrid : MonoBehaviour {

    public int width = 6;
    public int height = 6;

    public HexCell cellPrefab;
    public Text cellLabelPrefab;

    Canvas gridCanvas;

    HexMesh hexMesh;
    HexCell[] cells;

    void Awake()
    {
        gridCanvas = GetComponentInChildren<Canvas>();
        hexMesh = GetComponentInChildren<HexMesh>();

        cells = new HexCell[height * width];

        for (int z = 0, i = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                CreateCell(x, z, i++);
            }
        }
    }

    void CreateCell(int x, int z, int i)
    {
        Vector3 position;
        //the distance between adjacent hexagon cells is twice inner radius,
        //Each row is offset along the X axis by the inner radius
        //position.x = x * 10f;
        position.x = (x + z *0.5f- z/2) * (HexMetrics.innerRadius * 2f);
        position.y = 0f;
        //the distance to the next row of cells should be 1.5 times the outer radius.
        //position.z = z * 10f;
        position.z = z * (HexMetrics.outerRadius * 1.5f);

        HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;

        Text label = Instantiate<Text>(cellLabelPrefab);
        label.rectTransform.SetParent(gridCanvas.transform, false);
        label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
        label.text = x.ToString() + "\n" + z.ToString();
    }

    void Start()
    {
        hexMesh.Triangulate(cells);
    }

    public void  Triangulate(HexCell[] cells)
    {

    }
}