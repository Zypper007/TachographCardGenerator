using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator_pliku_ddd
{
    class DriversList : List<Driver>
    {
        public delegate void DriversEventHendler(DriversList sender, DriverEventArgs args);
        public event DriversEventHendler OnCountChange;


        public DriversList() : base()
        {

        }

        private void RisenEventOnCountChange(Driver d = null) => OnCountChange?.Invoke(this, new DriverEventArgs(d));

        public new void Add(Driver driver)
        {
            base.Add(driver);
            RisenEventOnCountChange(driver);
        }

        public new void AddRange(IEnumerable<Driver> collection)
        {
            base.AddRange(collection);
            RisenEventOnCountChange(collection.First());
        }

        public new void Clear()
        {
            base.Clear();
            RisenEventOnCountChange();
        }


        public new void Insert(int index, Driver item)
        {
            base.Insert(index, item);
            RisenEventOnCountChange(item);
        }
        
        public new void InsertRange(int index, IEnumerable<Driver> collection)
        {
            base.InsertRange(index, collection);
            RisenEventOnCountChange(collection.First());
        }

        public new bool Remove(Driver item)
        {
            var r = base.Remove(item);
            RisenEventOnCountChange(item);
            return r;
        }

        public new int RemoveAll(Predicate<Driver> match)
        {
            var r = base.RemoveAll(match);
            RisenEventOnCountChange();
            return r;
        }
       

        public new void RemoveAt(int index)
        {
            base.RemoveAt(index);
            RisenEventOnCountChange();
        }
        
        public new void RemoveRange(int index, int count)
        {
            base.RemoveRange(index, count);
            RisenEventOnCountChange();
        }

    }
}
