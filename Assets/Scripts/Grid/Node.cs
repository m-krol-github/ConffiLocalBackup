using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Conffi.Grid
{

    public class Node : MonoBehaviour
    {
        public int gCost;
        public int hCost;

        public int fCost
        {
            get { return gCost + hCost; }
        }

        public Node parentNode;

        public int posX;
        public int posY;
        public int state;

        public bool walkable;

        public Node(int _posX, int _posZ, int _state, bool walkable)
        {
            posX = _posX;
            posY = _posZ;
            state = _state;
            this.walkable = walkable;
        }
    }
}