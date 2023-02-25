using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
          ____
         /    \
    ____/      \____
   /    \  0   /    \
  /      \____/      \
  \  5   /    \  1   /
   \____/      \____/
   /    \  x   /    \
  /      \____/      \
  \  4   /    \  2   /
   \____/      \____/
        \   3  /
         \____/


*/
public static class HexCellOffset
{
    public enum Offset {
        offset0 = 0,
        offset1 = 1,
        offset2 = 2,
        offset3 = 3,
        offset4 = 4,
        offset5 = 5,
    }

    public static readonly Vector3 offset0 = new Vector3(-1.0525f, 0, 0);
    public static readonly Vector3 offset1 = new Vector3(-0.525f, 0, 0.9f);
    public static readonly Vector3 offset2 = new Vector3(0.525f, 0, 0.9f);
    public static readonly Vector3 offset3 = new Vector3(1.0525f, 0, 0);
    public static readonly Vector3 offset4 = new Vector3(0.525f, 0, -0.9f);
    public static readonly Vector3 offset5 = new Vector3(-0.525f, 0, -0.9f);

    public static readonly float zOffset = 0.2f;

    public static Vector3 GetTransform(Vector3 previousCellPosition, Offset whichOffset) {
        switch (whichOffset) {
            case Offset.offset0:
                return previousCellPosition + offset0;
            case Offset.offset1:
                return previousCellPosition + offset1;
            case Offset.offset2:
                return previousCellPosition + offset2;
            case Offset.offset3:
                return previousCellPosition + offset3;
            case Offset.offset4:
                return previousCellPosition + offset4;
            case Offset.offset5:
                return previousCellPosition + offset5;
        }

        return new Vector3();
    }

}
