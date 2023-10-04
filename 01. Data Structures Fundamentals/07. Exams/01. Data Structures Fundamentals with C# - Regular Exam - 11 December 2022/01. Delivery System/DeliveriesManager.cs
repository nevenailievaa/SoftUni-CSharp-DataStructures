using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.DeliveriesManager
{
    public class DeliveriesManager : IDeliveriesManager
    {
        //Fields
        private Dictionary<string, Deliverer> deliverersById = new Dictionary<string, Deliverer>();
        private Dictionary<string, Package> packagesById = new Dictionary<string, Package>();

        private Dictionary<string, string> packagesByDeliverer = new Dictionary<string, string>();

        //Methods
        public void AddDeliverer(Deliverer deliverer)
        {
            deliverersById.Add(deliverer.Id, deliverer);
        }

        public void AddPackage(Package package)
        {
            packagesById.Add(package.Id, package);
        }

        public void AssignPackage(Deliverer deliverer, Package package)
        {
            if (!deliverersById.ContainsKey(deliverer.Id) || !packagesById.ContainsKey(package.Id))
            {
                throw new ArgumentException();
            }
            packagesByDeliverer.Add(package.Id, deliverer.Id);
        }

        public bool Contains(Deliverer deliverer) => deliverersById.ContainsKey(deliverer.Id);

        public bool Contains(Package package) => packagesById.ContainsKey(package.Id);

        public IEnumerable<Deliverer> GetDeliverers() => deliverersById.Values;

        public IEnumerable<Deliverer> GetDeliverersOrderedByCountOfPackagesThenByName()
        {
            var deliverersAndPackageIds = new Dictionary<string, int>();

            foreach (var package in packagesByDeliverer)
            {
                if (!deliverersAndPackageIds.ContainsKey(package.Value))
                {
                    deliverersAndPackageIds.Add(package.Value, 0);
                }

                deliverersAndPackageIds[package.Value]++;
            }

            return deliverersAndPackageIds
                .OrderByDescending(kvp => kvp.Value)
                .ThenBy(kvp => deliverersById[kvp.Key].Name)
                .Select(kvp => deliverersById[kvp.Key]);
        }

        public IEnumerable<Package> GetPackages() => packagesById.Values;

        public IEnumerable<Package> GetPackagesOrderedByWeightThenByReceiver()
        {
            return packagesById.Values
                .OrderByDescending(package => package.Weight)
                .ThenBy(package => package.Receiver);
        }

        public IEnumerable<Package> GetUnassignedPackages() =>
            packagesById.Where(package => !packagesByDeliverer.ContainsKey(package.Key))
            .Select(package => package.Value);
    }
}
