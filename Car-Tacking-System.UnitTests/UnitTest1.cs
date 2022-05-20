using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Car_Tracking_System;

namespace Car_Tacking_System.UnitTests {
    [TestClass]
    public class UnitTest1 {
        [TestMethod]
        public void AddVehicleThrowsIndexOutOfRangeException() {
            // Arrange
            VehicleTracker vehicleTracker = new VehicleTracker(1, "123 Fake St.");
            Vehicle vehicle1 = new Vehicle("A01 T22", true);
            Vehicle vehicle2 = new Vehicle("A01 T21", true);

            // Act
            vehicleTracker.AddVehicle(vehicle1);

            // Assert
            Assert.ThrowsException<IndexOutOfRangeException>(() => vehicleTracker.AddVehicle(vehicle2));
        }

        [TestMethod]
        public void AddVehicleTests() {
            // Arrange
            VehicleTracker vehicleTracker = new VehicleTracker(1, "123 Fake St.");
            Vehicle vehicle1 = new Vehicle("A01 T22", true);
            var slotsAvailable = vehicleTracker.SlotsAvailable;

            // Act
            vehicleTracker.AddVehicle(vehicle1);

            // Assert
            Assert.AreEqual(slotsAvailable, vehicleTracker.SlotsAvailable + 1);
            Assert.IsTrue(vehicleTracker.VehicleList.ContainsValue(vehicle1));
        }

        [TestMethod]
        public void RemoveVehicleThrowsNullReferenceException() {
            // Arrange
            VehicleTracker vehicleTracker = new VehicleTracker(1, "123 Fake St.");
            Vehicle vehicle1 = new Vehicle("A01 T22", true);
            vehicleTracker.AddVehicle(vehicle1);

            // Act
            

            // Assert
            Assert.ThrowsException<NullReferenceException>(() => vehicleTracker.RemoveVehicle("AJJ"));
        }

        [TestMethod]
        public void RemoveVehicleTests() {
            // Arrange
            VehicleTracker vehicleTracker = new VehicleTracker(1, "123 Fake St.");
            Vehicle vehicle1 = new Vehicle("A01 T22", true);
            vehicleTracker.AddVehicle(vehicle1);

            // Act
            var slotsAvailable = vehicleTracker.SlotsAvailable;
            vehicleTracker.RemoveVehicle(vehicle1.Licence);

            // Assert
            Assert.AreEqual(slotsAvailable, vehicleTracker.SlotsAvailable-1);
            Assert.IsTrue(!vehicleTracker.VehicleList.ContainsValue(vehicle1));
        }

        [TestMethod]
        public void RemoveVehicleSlotNumberGreaterReturnsFalse() {
            // Arrange
            VehicleTracker vehicleTracker = new VehicleTracker(1, "123 Fake St.");
            Vehicle vehicle1 = new Vehicle("A01 T22", true);
            vehicleTracker.AddVehicle(vehicle1);

            // Act
            var result = vehicleTracker.RemoveVehicle(2);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RemoveVehicleSlotNumberSmallerReturnsFalse() {
            // Arrange
            VehicleTracker vehicleTracker = new VehicleTracker(1, "123 Fake St.");
            Vehicle vehicle1 = new Vehicle("A01 T22", true);
            vehicleTracker.AddVehicle(vehicle1);

            // Act
            var result = vehicleTracker.RemoveVehicle(2);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RemoveVehicleSlotNumberReturnsTrue() {
            // Arrange
            VehicleTracker vehicleTracker = new VehicleTracker(1, "123 Fake St.");
            Vehicle vehicle1 = new Vehicle("A01 T22", true);
            vehicleTracker.AddVehicle(vehicle1);

            // Act
            int slot = vehicleTracker.VehicleList.First(v => v.Value.Licence == vehicle1.Licence).Key;
            var result = vehicleTracker.RemoveVehicle(slot);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ParkedPassholdersReturnsParkedCarsWithPass() {
            // Arrange
            VehicleTracker vehicleTracker = new VehicleTracker(2, "123 Fake St.");
            Vehicle vehicle1 = new Vehicle("A01 T22", true);
            Vehicle vehicle2 = new Vehicle("A01 T21", false);

            // Act
            vehicleTracker.AddVehicle(vehicle1);
            vehicleTracker.AddVehicle(vehicle2);
            List<Vehicle> parkedCars = vehicleTracker
                .VehicleList
                .Where(v => v.Value.Pass)
                .Select(v=>v.Value)
                .ToList();

            var list = vehicleTracker.ParkedPassholders();

            // Assert
            Assert.IsTrue(Enumerable.SequenceEqual(parkedCars, list));
        }

        [TestMethod]
        public void PassholderPercentageTests() {
            // Arrange
            VehicleTracker vehicleTracker = new VehicleTracker(3, "123 Fake St.");
            Vehicle vehicle1 = new Vehicle("A01 T22", true);
            Vehicle vehicle2 = new Vehicle("A01 T21", false);
            Vehicle vehicle3 = new Vehicle("A01 T20", true);
            
            // Act
            vehicleTracker.AddVehicle(vehicle1);
            vehicleTracker.AddVehicle(vehicle2);
            vehicleTracker.AddVehicle(vehicle3);

            List<Vehicle> parkedCars = vehicleTracker
                .VehicleList
                .Where(v => v.Value.Pass)
                .Select(v=>v.Value)
                .ToList();
            decimal percentage = parkedCars.Count * 100 / vehicleTracker.Capacity; 
            decimal passholderPercentage = vehicleTracker.PassholderPercentage();
            
            // Assert
            Assert.AreEqual(percentage, passholderPercentage);
        }
    }
}
