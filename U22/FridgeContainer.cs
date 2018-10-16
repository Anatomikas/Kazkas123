using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U22
{
    class FridgeContainer
    {
        private Fridges[] fridges;
        public int Count { get; private set; }
        public FridgeContainer(int size)
        {
            fridges = new Fridges[size];
            Count = 0;
        }
        public void AddFridge(Fridges fridge)
        {
            fridges[Count++] = fridge;
        }
        public void AddFridges(Fridges fridge, int index)
        {
            fridges[index] = fridge;
        }
        public Fridges GetFridge(int index)
        {
            return fridges[index];
        }
        public bool Contains(Fridges fridge)
        {
            return fridges.Contains(fridge);
        }
    }
}
